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
            public SortedList tags;
        }

        // Storage of max Tag Key string length
        public class TagKeyLength
        {
            public int max;
            public string k;
        }

        private Thread m_myThread;
        static int m_maxWays;
        static string m_status;
        static Form1 m_myForm;
        const int m_cFieldSize = 64;

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
            labelCurrentWay.Text = m_status;
        }

        // http://www.openstreetmap.org/api/0.6/map?bbox=-1.080351,50.895238,-1.054516,50.907688
        private void workerThread()
        {
            // Load Theme data
            ArrayList keys = new ArrayList();
            XmlDocument themeDoc = new XmlDocument();
            themeDoc.Load(themeTextBox.Text);
            // Load key list
            XmlNodeList keyNodeList = themeDoc.SelectNodes("osm2tab/keys/key");
            foreach (XmlNode keyNode in keyNodeList)
            {
                keys.Add(keyNode.SelectSingleNode("@name").Value);
            }
            
            double[] pointsX = new double[2000]; // 2000 is 0.6 OSM spec
            double[] pointsY = new double[2000];

            // Load OSM data
            XmlTextReader reader = new XmlTextReader(inputTextBox.Text);

            SortedList nodeList = new SortedList();

            SortedList wayList = new SortedList();

            SortedList tagKeyLengthList = new SortedList();

            WayInfo currentWay = null;

           
            m_status = "Starting";
            m_myForm.Invoke(m_myForm.m_myCurrentWayDelegate);

            // Used to calculate TAB database field format
            //needs to Be an Array of longest strings per field otherwise all fields will Get same width. even simple ones
            //but field widths should be consistent so what do we do
            int longestString = 0;

            int loadedNodeCount = 0;
            // Cache XML file as fast-readable database in ram
            while (reader.Read())
            {
                // Log progress
                if (loadedNodeCount++ % 100000 == 0)
                {
                    m_status = "Loading OSM: " + Convert.ToString(loadedNodeCount/100000);
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
                            way.tags = new SortedList();

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
                                currentWay.tags.Add(ki.k, ki);

                                // Update max key name size for MI tab field width
                                // Each Key
                                if (!tagKeyLengthList.Contains(ki.k))
                                {
                                    // If there is not a 
                                    TagKeyLength tkl = new TagKeyLength();
                                    tkl.k = ki.k;
                                    tkl.max = ki.v.Length;
                                    tagKeyLengthList.Add(ki.k, tkl);
                                }
                                else
                                {
                                    TagKeyLength tkl = (TagKeyLength)tagKeyLengthList[ki.k];
                                    if (tkl.max < ki.v.Length)
                                        tkl.max = ki.v.Length;
                                }   
                                
                                // log longest string
                                if (ki.v.Length > longestString)
                                    longestString = ki.v.Length;
                            }
                            //else if (currentRelation blah blah != null)
                            //{
                            //}
                        }
                        else if (reader.Name.Equals("relation"))
                        {
                            currentWay = null;
                            int a = 0;
                            int b = a + 1;
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
                // Get max field width
                TagKeyLength tkl = (TagKeyLength)tagKeyLengthList[key];

                if (tkl != null)
                {
                    MiApi.mitab_c_add_field(regionTabFile, key, 1, tkl.max, 0, 0, 0);
                    MiApi.mitab_c_add_field(lineTabFile, key, 1, tkl.max, 0, 0, 0);
                }
                else
                {
                    // No key exists in the dataset so there is no max length - make it 32
                    MiApi.mitab_c_add_field(regionTabFile, key, 1, 32, 0, 0, 0);
                    MiApi.mitab_c_add_field(lineTabFile, key, 1, 32, 0, 0, 0);
                }

                index++;
            }

            // Create MapInfo tabs from cached database
            m_maxWays = wayList.Count;
            m_myForm.Invoke(m_myForm.m_myMaxWaysDelegate);
  
            // Lines and Regions
            for (int i = 0; i < wayList.Count; i++)
            {
                // Log progress
                if (i % 10000 == 0)
                {
                    m_status = "Translating : " + Convert.ToString((100* i) / wayList.Count) + "%";
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
                    // Region
                    IntPtr feat = MiApi.mitab_c_create_feature(regionTabFile, 7); // 7 = region
                    // 'part' param is -1 for single part regions. Need to use relation nodes to add 'holes' to -1 part polys which have poly numbers 1+ 
                    MiApi.mitab_c_set_points(feat, -1, nodes.Count - 1, pointsX, pointsY); // nodes.Count -1 as last and first nodes are the same
                    int gti = 0; // get tag info
                    foreach (string key in keys)
                    {
                        TagInfo tag = (TagInfo)way.tags[key];
                        if (tag != null) // Not every field is used
                            MiApi.mitab_c_set_field(feat, gti, tag.v);

                        gti++;
                    }

                    // Set Region Style
                    XmlNodeList styleNodeList = themeDoc.SelectNodes("osm2tab/regionStyles/style");
                    foreach (XmlNode styleNode in styleNodeList)
                    {
                        string styleKey = styleNode.SelectSingleNode("@key").Value;
                        TagInfo tag = (TagInfo)way.tags[styleKey];
                        if (tag != null) // Not every field is used
                        {
                            string styleKeyValue = styleNode.SelectSingleNode("@value").Value;
                            if (tag.v == styleKeyValue)
                            {
                                int pattern = Convert.ToInt32(styleNode.SelectSingleNode("@pattern").Value);
                                int foreground = Convert.ToInt32(styleNode.SelectSingleNode("@foreground").Value);
                                int background = Convert.ToInt32(styleNode.SelectSingleNode("@background").Value);
                                int transparent = Convert.ToInt32(styleNode.SelectSingleNode("@transparent").Value);

                                int penPattern = Convert.ToInt32(styleNode.SelectSingleNode("@penPattern").Value);
                                int penColour = Convert.ToInt32(styleNode.SelectSingleNode("@penColour").Value);
                                int penWidth = Convert.ToInt32(styleNode.SelectSingleNode("@penWidth").Value);

                                MiApi.mitab_c_set_brush(feat, foreground, background, pattern, transparent);
                                MiApi.mitab_c_set_pen(feat, penWidth, penPattern, penColour);
                            }
                        }
                    }

                    MiApi.mitab_c_write_feature(regionTabFile, feat);
                    MiApi.mitab_c_destroy_feature(feat);
                }
                else if (nodes.Count >= 2)
                {
                    // Line
                    IntPtr feat = MiApi.mitab_c_create_feature(regionTabFile, 5); // 5 = region
                    MiApi.mitab_c_set_points(feat, 0, nodes.Count, pointsX, pointsY); // part is 0 - can we use relation nodes to associate further (>0) parts?
                    int gti = 0; // get tag info
                    foreach (string key in keys)
                    {
                        TagInfo tag = (TagInfo)way.tags[key];
                        if(tag != null) // Not every field is used
                            MiApi.mitab_c_set_field(feat, gti, tag.v);

                        gti++;
                    }

                    // Set Line Style
                    XmlNodeList styleNodeList = themeDoc.SelectNodes("osm2tab/lineStyles/style");
                    foreach (XmlNode styleNode in styleNodeList)
                    {
                        string styleKey = styleNode.SelectSingleNode("@key").Value;
                        TagInfo tag = (TagInfo)way.tags[styleKey];
                        if (tag != null) // Not every field is used
                        {
                            string styleKeyValue = styleNode.SelectSingleNode("@value").Value;
                            if (tag.v == styleKeyValue)
                            {
                                int penPattern = Convert.ToInt32(styleNode.SelectSingleNode("@penPattern").Value);
                                int penColour = Convert.ToInt32(styleNode.SelectSingleNode("@penColour").Value);
                                int penWidth = Convert.ToInt32(styleNode.SelectSingleNode("@penWidth").Value);

                                MiApi.mitab_c_set_pen(feat, penWidth, penPattern, penColour);
                            }
                        }
                    }

                    MiApi.mitab_c_write_feature(lineTabFile, feat);
                    MiApi.mitab_c_destroy_feature(feat);
                }
                
            }

            MiApi.mitab_c_close(regionTabFile);
            MiApi.mitab_c_close(lineTabFile);

            m_status = "Done!";
            m_status = longestString.ToString();
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
            bool okToProcess = true;

            if (outputTextBox.Text == "")
            {
                MessageBox.Show("Please enter a folder in the 'Output' box above.\r\rYou can use the button at the right side of this box to select a folder.", "There is a problem");
                okToProcess = false;
            }

            if (inputTextBox.Text == "")
            {
                MessageBox.Show("Please enter a file or URL in the 'Input' box above.\r\rYou can use the button at the right side of this box to select an OSM file.", "There is a problem");
                okToProcess = false;
            }

            if(okToProcess)
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
            outputFolderBrowserDialog.Description = "Output folder for TAB files";
            outputFolderBrowserDialog.ShowDialog();
            outputTextBox.Text = outputFolderBrowserDialog.SelectedPath;
        }

        private void buttonSelectInFile_Click(object sender, EventArgs e)
        {
            openOSMFileDialog.FileName = "";
            openOSMFileDialog.Multiselect = false;
            openOSMFileDialog.Title = "Select OSM file to translate";
            openOSMFileDialog.DefaultExt = "osm";
            openOSMFileDialog.CheckPathExists = true;
            openOSMFileDialog.CheckFileExists = true;
            openOSMFileDialog.Filter = "OpenStreetMap files (*.osm)|*.osm|All files (*.*)|*.*";
            openOSMFileDialog.ShowDialog();

            inputTextBox.Text = openOSMFileDialog.FileName;
        }
    }
}
