using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using EBop.MapObjects.MapInfo;
using System.Threading;
using System.Collections;

namespace OSM2TAB
{
    public partial class Form1 : Form
    {
        public class NodeInfo
        {
            public double lat;
            public double lon;
        }

        public class TagInfo
        {
            public string k;
            public string v;
        }

        public class WayInfo
        {
            public int id;
            public bool corrupt = false;
            public ArrayList nodes;
            public ArrayList tags;
        }

        private Thread m_myThread;
        static int m_maxWays;
        static int m_currentWay;
        static Form1 m_myForm;

        public delegate void delegateSetMaxWays();
        public delegate void delegateSetCurrentWay();
        
        public delegateSetMaxWays m_myMaxWaysDelegate;
        public delegateSetCurrentWay m_myCurrentWayDelegate;

        public void setMaxWays()
        {
            labelMaxWays.Text = m_maxWays.ToString();
        }
        public void setCurrentWay()
        {
            labelCurrentWay.Text = m_currentWay.ToString();
        }

        private void workerThread()
        {
            string[] keys = { "name", "highway", "building", "landuse" };

            double[] pointsX = new double[2000];
            double[] pointsY = new double[2000];

            // Load OSM data
            XmlTextReader reader = new XmlTextReader(inputTextBox.Text);

            SortedList nodeList = new SortedList();

            SortedList wayList = new SortedList();

            WayInfo currentWay = null;

            m_currentWay = 1;
            m_myForm.Invoke(m_myForm.m_myCurrentWayDelegate);

            int loadedNodeCount = 0;
            // Cache XML file as fast-readable database in ram
            while (reader.Read())
            {
                if (loadedNodeCount++ % 100000 == 0)
                {
                    m_currentWay = loadedNodeCount/100000;
                    m_myForm.Invoke(m_myForm.m_myCurrentWayDelegate);
                }

                switch (reader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                    {
                        if (reader.Name.Equals("node"))
                        {
                            //currentWay = null; // tags can be for nodes or ways
                            int id = Convert.ToInt32(reader.GetAttribute("id"));

                            NodeInfo node = new NodeInfo();
                            node.lat = Convert.ToDouble(reader.GetAttribute("lat"));
                            node.lon = Convert.ToDouble(reader.GetAttribute("lon"));
                            if(!nodeList.Contains(id))
                                 nodeList.Add(id, node);
                            else
                            {
                                // need to trace data source error
                            }


                        }
                        else if (reader.Name.Equals("way"))
                        {  
                            WayInfo way = new WayInfo();
                            way.nodes = new ArrayList();
                            way.tags = new ArrayList();

                            way.id = Convert.ToInt32(reader.GetAttribute("id"));
                            // this can be a duplicate in an error in the osm data
                            // therefore current node should be null and nd and way needs to check for null

                            if (!wayList.Contains(way.id))
                                wayList.Add(way.id, way);
                            else
                            {
                                // !! need to trace error
                                currentWay.corrupt = true;
                            }

                            currentWay = way;  
                        }
                        else if (reader.Name.Equals("nd"))
                        {
                            int ndRef = Convert.ToInt32(reader.GetAttribute("ref"));
                            NodeInfo ni = (NodeInfo)nodeList[ndRef];
                            
                            if (ni != null)
                            {
                                currentWay.nodes.Add(ni);
                            }
                            else
                            {
                                // Node ref not found - error!
                                currentWay.corrupt = true;
                            }
                            
                        }
                        else if (reader.Name.Equals("tag"))
                        {
                            // Way Tags
                            if (currentWay != null)
                            {
                                // Way Keys only
                                TagInfo ki = new TagInfo();
                                ki.k = reader.GetAttribute("k");
                                ki.v = reader.GetAttribute("v");
                                currentWay.tags.Add(ki);
                            }
                            //else if (currentRelation blah blah != null)
                            //{
                            //}
                        }
                        break;
                    }
                }
            }

            // Create MapInfo tabs

            IntPtr regionTabFile = MiApi.mitab_c_create(outputTextBox.Text + "\\" + tabPrefix.Text + "_region.tab", "tab", "Earth Projection 1, 104", 0, 0, 0, 0);
            IntPtr lineTabFile = MiApi.mitab_c_create(outputTextBox.Text + "\\" + tabPrefix.Text + "_line.tab", "tab", "Earth Projection 1, 104", 0, 0, 0, 0);

            // Create fields
            int index = 0;
            foreach (string key in keys)
            {
                MiApi.mitab_c_add_field(regionTabFile, key, 1, 64, 0, 0, 0);
                MiApi.mitab_c_add_field(lineTabFile, key, 1, 64, 0, 0, 0);

                index++;
            }

            // Create MapInfo tabs from cached database
            m_maxWays = wayList.Count;
            m_myForm.Invoke(m_myForm.m_myMaxWaysDelegate);
  
            for (int i = 0; i < wayList.Count; i++)
            {
                if (i % 10000 == 0)
                {
                    m_currentWay = (100* i) / wayList.Count;
                    m_myForm.Invoke(m_myForm.m_myCurrentWayDelegate);
                }

                WayInfo way = (WayInfo)wayList.GetByIndex(i);
                if (way.corrupt)
                    continue;

                bool iAmARegion = false;
                ArrayList nodes = ((WayInfo)wayList.GetByIndex(i)).nodes;

                // Is this a region? i.e. first and last nodes the same
                NodeInfo firstNode = (NodeInfo)nodes[0];
                NodeInfo lastNode = (NodeInfo)nodes[nodes.Count - 1];
                if ((firstNode.lat == lastNode.lat)
                    && (firstNode.lon == lastNode.lon))
                {
                    iAmARegion = true;
                }

                for (int j = 0; j < nodes.Count; j++)
                {
                    NodeInfo node = (NodeInfo)nodes[j];
                    pointsX[j] = node.lon;
                    pointsY[j] = node.lat;   
                }

                if (iAmARegion && nodes.Count >=3)
                {
                    IntPtr feat = MiApi.mitab_c_create_feature(regionTabFile, 7);
                    MiApi.mitab_c_set_points(feat, -1, nodes.Count - 1, pointsX, pointsY);
                    MiApi.mitab_c_write_feature(regionTabFile, feat);
                    MiApi.mitab_c_destroy_feature(feat);
                }
                else if (nodes.Count >= 2)
                {
                    IntPtr feat = MiApi.mitab_c_create_feature(regionTabFile, 5);
                    MiApi.mitab_c_set_points(feat, 0, nodes.Count, pointsX, pointsY);
                    MiApi.mitab_c_write_feature(lineTabFile, feat);
                    MiApi.mitab_c_destroy_feature(feat);
                }
                
            }

            MiApi.mitab_c_close(regionTabFile);
            MiApi.mitab_c_close(lineTabFile);

            m_currentWay = 42;
            m_myForm.Invoke(m_myForm.m_myCurrentWayDelegate);
        }

        public Form1()
        {
            InitializeComponent();

            m_myMaxWaysDelegate = new delegateSetMaxWays(setMaxWays);
            m_myCurrentWayDelegate = new delegateSetCurrentWay(setCurrentWay);
            m_myForm = this;
        }

        private void buttonTestThread_Click(object sender, EventArgs e)
        {
            if (outputTextBox.Text == "")
            {
                MessageBox.Show("Please enter a folder in the 'Output' box above.\r\rYou can use the button at the right side of this box to select a folder.", "There is a problem");
            }
            else
            {
                m_myThread = new Thread(new ThreadStart(workerThread));
                m_myThread.Start();
            }
        }

        private void outputFolderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void buttonSelectOutFolder_Click(object sender, EventArgs e)
        {
            outputFolderBrowserDialog.ShowDialog();
            outputTextBox.Text = outputFolderBrowserDialog.SelectedPath;
        }
    }
}
