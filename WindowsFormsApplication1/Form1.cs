using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SldWorks;
using SwConst;
using SwCommands;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private SldWorks.SldWorks swApp;
        private int traverseLevel;
        List<StudentInfo> listOfStudents;
        private int numSubmissions = 0;
        private string rootDirectory = "";
        ModelDoc2 currentModel = default(ModelDoc2);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }



        // Shows the FolderBrowser and fills in the "directory" text box with browser-selected folder path
        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = folderBrowserDialog1.SelectedPath;
            }
        }


        // Runs the "Normal-To view" command for a feature's sketch (this is the Ctrl+8 user shortcut in Solidworks)
        public int RunNormalToSketchCommand()
        {

            Console.WriteLine("  Running 'Normal-To' command...");
            ModelDoc2 thisModel = default(ModelDoc2);
            try
            {
                thisModel = (ModelDoc2)swApp.ActiveDoc;  // Get the part 
                if (thisModel == null)
                {
                    return -1;
                }
                int currentCommandID = 169;  // // enum int for the NormalTo command (defined in 'swCommands_e')
                string currentCommandTitle = "Normal-To";  // custom name for the command to be run, arbitrary

                swApp.RunCommand(currentCommandID, currentCommandTitle);  // Run the Normal-To command
                thisModel.ViewZoomtofit2();
            }
            catch (Exception e)
            {
                ShowFatalErrorMessage(e.ToString());
                throw;
            }

            return 0;
        }


        // Gets details of a given sketch (as a Feature) and prints to the Console, returns int contrained status
        public int GetConstrainedStatus(Feature featureAsSketch)
        {
            Sketch currentSketch = default(Sketch);
            currentSketch = featureAsSketch.GetSpecificFeature2();

            int constrainedStatus = -1;

            if (currentSketch != null) // Is a valid Sketch object
            {
                ////Console.WriteLine("Iterating through sketch #" + numSketches + " in this model...");
                //Console.WriteLine("  Name: " + featureAsSketch.Name);
                //Console.WriteLine("  Created by: " + featureAsSketch.CreatedBy);
                //Console.WriteLine("  Date created: " + featureAsSketch.DateCreated);
                //Console.WriteLine("  Date modified: " + featureAsSketch.DateModified);
                //Console.WriteLine("  Is rolled back?: " + featureAsSketch.IsRolledBack());
                //Console.WriteLine("  Is suppressed?: " + featureAsSketch.IsSuppressed());
                //Console.WriteLine("  Constrained status for this sketch is: " + currentSketch.GetConstrainedStatus());
            }
            else
            {
                Console.WriteLine("** Error: currentSketch is NULL! Something's wrong...");
            }

            return constrainedStatus;
        }



        // (UNUSED, for future reference) Loops through all sketches in a given ModelDoc2 model
        public void iterate_each_sketch_in_model(ModelDoc2 thisModel)
        {
            Console.WriteLine("Iterating through all sketches in this model...");

            if (thisModel == null)
            {
                Console.WriteLine("** Error: thisModel is NULL! Something's wrong...");
            }
            Feature currentFeature = default(Feature);
            int numParentFeatures = 0;
            int numSketches = 0;

            currentFeature = thisModel.IFirstFeature();
            List<Feature> listOfSketches = new List<Feature>();

            // Loop through all features in 'thisModel'
            while (currentFeature != null)
            {
                numParentFeatures++;
                if (currentFeature.GetTypeName2() == "ProfileFeature")  // If child is type Sketch
                {

                    // Gather properties of sketch
                    Console.WriteLine("Iterating through parentFeature" + numParentFeatures + " ('" + currentFeature.Name + "') in this model...");
                    GetConstrainedStatus(currentFeature);

                    // Clear all previous selections
                    thisModel.ClearSelection2(true);

                    // Select the current sketch (we verified this feature is a sketch)
                    if (currentFeature.Select2(false, 0))
                    {
                        Console.WriteLine("Successfully selected Sketch ('" + currentFeature.Name + "')");
                        listOfSketches.Add(currentFeature); 
                    }
                    else
                    {
                        Console.WriteLine(" ** ERROR: Failed to select Sketch ('" + currentFeature.Name + "')");
                        break;
                    }

                    // Edit the (previously selected) sketch
                    Console.WriteLine("  Editing sketch......");
                    thisModel.EditSketchOrSingleSketchFeature();

                    // Run the 'Normal-To' view command
                    RunNormalToSketchCommand();

                    // Exit out of "Edit Sketch" and rebuild
                    thisModel.InsertSketch2(true);
                }

                currentFeature = currentFeature.GetNextFeature();
                if (currentFeature == null)
                {
                    Console.WriteLine("Failed to get next feature! (GetNextFeature() is NULL)");
                }
            }

            Console.WriteLine("Got " + numSketches + " sketches from this model");
        }




        // Opens and returns a model, given a file path. Returns currently-active model if already open
        public ModelDoc2 GetSolidworksModelFromFile(string filePath)
        {
            // Declare variables
            ModelDoc2 currentModel = default(ModelDoc2);
            string fileName = new FileInfo(filePath).Name;
            int i_errors = 0;
            int i_warnings = 0;

            // Check if Solidworks.exe is open
            if (swApp == null)
            {
                ShowNonFatalError("Solidworks.exe is not open (var 'swApp' is null)!\n\nPlease start Solidworks and try again.");
                return null;
            }

            // If requested model == already-open model, return current model
            currentModel = swApp.ActiveDoc;
            if (currentModel != null)
            {
                Console.WriteLine("Already-open model is titled '{0}'", currentModel.GetTitle());
                if (currentModel.GetTitle() == fileName)
                {
                    return currentModel;
                }
            }

            // Open document
            try
            {
                Console.Write("Opening Solidworks file '" + fileName + "'... ");
                currentModel = swApp.OpenDoc6(filePath, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref i_errors, ref i_warnings);
                Console.Write("Opened.\n");
                Console.WriteLine("  Errors: " + i_errors);
                Console.WriteLine("  Warnings: " + i_warnings);
            }
            catch (Exception openDocExcept)
            {
                Console.WriteLine(openDocExcept.ToString());
                throw;
            }

            //// Get the current doc (should be the one we just opened)
            //currentModel = (ModelDoc2)swApp.ActiveDoc;

            // Exit if it failed
            if (currentModel == null)
            {
                ShowNonFatalError("Model failed to open (var 'currentModel' is null)!\n\nPlease try again.");
                return null;
            }

            // Set the working directory to the document directory
            //swApp.SetCurrentWorkingDirectory(currentModel.GetPathName().Substring(0, currentModel.GetPathName().LastIndexOf("\\")));
            //string workingDirectory = swApp.GetCurrentWorkingDirectory();
            //var workingFolder = Path.Combine(Directory.GetParent(workingDirectory).Name, Path.GetFileName(workingDirectory));
            //Console.WriteLine("Current working directory is now " + workingFolder);

            // Return 
            return currentModel;
        }



        // Shows a dialog box without closing Solidworks or the Form
        public void ShowNonFatalError(string message)
        {
            MessageBox.Show("Error: '" + message + "'\n\nPlease try again.", "Try again", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
            Console.WriteLine(message);
        }

        // Shows a dialog box and closes both Solidworks and the Form
        public void ShowFatalErrorMessage(string message)
        {
            Console.WriteLine(message);
            if (MessageBox.Show("ERROR: '" + message + "'\n\nExiting...", "Exiting script", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly)
                == DialogResult.OK) { }

            CloseSolidworks(false);  // Close without dialog prompt
            Application.Exit();
        }



        // Checks if file name has " ~ <fileName>" or " . <fileName>", given file path
        private bool IsTempFile(string filePath)
        {
            string fileName = new FileInfo(filePath).Name;
            if (fileName[0] == '~')  // Solidworks temp. file
                return true;
            else if (fileName[0] == '.')  // Hidden file (usually from MACOSX folder)
                return true;
            else
                return false;
        }




        // "Open solidworks" button
        private void button3_Click(object sender, EventArgs e)
        {
            // Open Solidworks
            Console.WriteLine("Opening Solidworks.exe ...");
            swApp = new SldWorks.SldWorks();
            // swApp.Visible = true;
            
            // If any parts are open, notify and exit script
            if (swApp.ActiveDoc != null)
            {
                Console.WriteLine("Solidworks already open, restarting it...");
                CloseSolidworks(false);  // Close without prompt
                button3_Click(null, null);
                return;
            }
            else
            {
                Console.WriteLine("Opened Solidworks.exe successfully.");
                startSolidworks.Enabled = false;  // Disable the "Start Solidworks form button"
            }
            

        }



        // Expands every node in the Feature Manager tree, given a Solidworks ModelDoc2
        private void ExpandAllSolidworksFeaturesInTree(ModelDoc2 model)
        {
            Console.WriteLine("Expanding all features in Solidworks part...");
            if (model == null)
            {
                ShowNonFatalError("Failed to open model. (ActiveDoc is null)");
                return;
            }

            FeatureManager featureMgr = default(FeatureManager);
            TreeControlItem rootNode = default(TreeControlItem);

            // Get the root node of the Feature Manager tree
            featureMgr = model.FeatureManager;
            rootNode = featureMgr.GetFeatureTreeRootItem2((int)swFeatMgrPane_e.swFeatMgrPaneBottom);
            if ((rootNode == null))
            {
                ShowNonFatalError("Failed to get root node of Feature Tree from part. (rootNode is null)");
                return;
            }

            //TreeControlItem childNode = default(TreeControlItem);
            int nodeObjectType = rootNode.ObjectType;
            object nodeObject = rootNode.Object;

            // Ignore Annotations and History features
            if (rootNode.Text != "Annotations" && rootNode.Text != "History")
            {
                // Expand the node
                rootNode.Expanded = true;
                traverseLevel = traverseLevel + 1;
                //childNode = rootNode.GetFirstChild();
            }

            traverseLevel = traverseLevel - 1;
            Console.WriteLine("Expanded all features in part successfully.");
        }




        // TODO: add "Next <submission|part|sketck>" button functionality
        private void nextSubmission_Click_1(object sender, EventArgs e)
        {
            

        }




        // Returns a List object containing each student folder's path found within root directory
        private List<string> EnumerateFolders(string rootFolderPath)
        {
            try
            {
                List<string> listOfSubfolderPaths = Directory.GetDirectories(rootFolderPath).ToList();

                foreach (string path in listOfSubfolderPaths)
                {
                    // Console.WriteLine(path);
                }
                listOfSubfolderPaths.Sort();
                return listOfSubfolderPaths;
            }
            catch (UnauthorizedAccessException UAEx)
            {
                ShowNonFatalError(UAEx.Message);  // Show message box
                return null;
            }
            catch (PathTooLongException PathEx)
            {
                ShowNonFatalError(PathEx.Message);  // Show message box
                
                return null;
            }
            catch (DirectoryNotFoundException MissingEx)
            {
                ShowNonFatalError(MissingEx.Message);
                return null;
            }
        }


        // Returns a List object containing each part file's path found within directory
        private List<string> EnumerateSolidworksFiles(string rootFolderPath)
        {
            try
            {
                List<string> listOfFilePaths = new List<string>();

                // Loop through all files found in folder
                foreach (string filePath in Directory.EnumerateFiles(rootFolderPath, "*.SLDPRT", SearchOption.AllDirectories))
                {
                    if (!IsTempFile(filePath))  // Ignore if '~' or '.' at beginning of file
                    {
                            listOfFilePaths.Add(filePath);
                            //Console.WriteLine(filePath);
                    } 
                }

                listOfFilePaths.Sort();
                return listOfFilePaths;
            }
            catch (UnauthorizedAccessException UAEx)
            {
                ShowNonFatalError(UAEx.Message);  // Show message box
                return null;
            }
            catch (PathTooLongException PathEx)
            {
                ShowNonFatalError(PathEx.Message);  // Show message box
                return null;
            }
        }


        // Returns a List object containing each part as a ModelDoc2, given a folder path
        private List<ModelDoc2> EnumerateSolidworksParts(string folderPath)
        {
            // Check if Solidworks.exe is open
            if (swApp == null)
            {
                ShowNonFatalError("Solidworks.exe is not open (var 'swApp' is null)!\n\nPlease start Solidworks and try again.");
                return null;
            }

            List<ModelDoc2> listOfParts = new List<ModelDoc2>();
            List<string> listOfFilePaths = new List<string>();

            // Retrieves a list of file paths from folder
            listOfFilePaths = EnumerateSolidworksFiles(folderPath);
            if (listOfFilePaths == null)
            {
                ShowNonFatalError("Did not find any files in folder: "+folderPath);
            }

            // Retrieves each part in folder as a ModelDoc2 object, adds part to listOfParts
            int numParts = listOfFilePaths.Count;
            for (int i = 0; i < numParts; i++)
            {
                ModelDoc2 currentModel = default(ModelDoc2);
                currentModel = GetSolidworksModelFromFile(listOfFilePaths[i]);
                if (currentModel == null)
                {
                    ShowNonFatalError("Failed to get model from folder: " + folderPath);
                    continue;
                }

                listOfParts.Add(currentModel);

            }

            return listOfParts;
        }


        // Returns a List object containing each sketch as a Feature, from a given Solidworks ModelDoc2
        private List<Feature> EnumerateSketchesInSolidworksPart(ModelDoc2 thisModel)
        {
            if (thisModel == null)
            {
                ShowNonFatalError("** Error: thisModel is NULL! Something's wrong...");
                return null;
            }
            Console.WriteLine("Enumerating all sketches in model '{0}'...", thisModel.GetTitle());

            List<Feature> listOfSketches = new List<Feature>();
            Feature currentFeature = default(Feature);
            int numParentFeatures = 0;

            // Loop through all features in 'thisModel'
            currentFeature = thisModel.IFirstFeature();
            while (currentFeature != null)
            {
                numParentFeatures++;
                if (currentFeature.GetTypeName2() == "ProfileFeature")  // If child is type Sketch
                {
                    // Gather properties of sketch
                    //GetSketchDetails(currentFeature);

                    thisModel.ClearSelection2(true);  // Clear all previous entity/item selections in Solidworks

                    // Select the current sketch (we verified this feature is a sketch)
                    if (currentFeature.Select2(false, 0))
                    {
                        Console.WriteLine("Successfully selected Sketch ('" + currentFeature.Name + "')");
                        listOfSketches.Add(currentFeature);
                    }
                    else
                    {
                        Console.WriteLine(" ** ERROR: Failed to select Sketch ('" + currentFeature.Name + "')");
                        break;
                    }
                }
                currentFeature = currentFeature.GetNextFeature();
            }

            Console.WriteLine("Got " + listOfSketches.Count + " sketches from this model");
            return listOfSketches;
        }




        // Fill the class variable "listOfStudents" with info from each student folder
        private void PopulateListOfStudents(List<string> listOfStudentFolders)
        {
            // Check if Solidworks.exe is open
            if (swApp == null)
            {
                ShowNonFatalError("Solidworks.exe is not open (var 'swApp' is null)!\n\nPlease start Solidworks and try again.");
                return;
            }

            numSubmissions = listOfStudentFolders.Count();
            listOfStudents = new List<StudentInfo>(numSubmissions);
            swApp.Visible = false;

            // Check each student folder for Solidworks files
            for (int i = 0; i < numSubmissions; i++)
            {
                Console.WriteLine("Populating Student {0} of {1}...", i+1, numSubmissions);
                // Get single folder from listOfFolders (a class member variable)
                string folderPath = listOfStudentFolders[i];
                string folderName = folderPath.Substring(folderPath.LastIndexOf("\\") + 1);
                StudentInfo student = new StudentInfo();

                // Fill each Student class based on their folder path
                student.Name = (folderName.Split('_')[0]);
                student.ListOfFilePaths = EnumerateSolidworksFiles(folderPath);  // Get list of student folder paths
                student.NumFiles = student.ListOfFilePaths.Count();
                student.FolderPath = folderPath;

                //List<ModelDoc2> listOfParts = new List<ModelDoc2>();
                //listOfParts = EnumerateSolidworksParts(folderPath);

                // Extract and define the file NAMES, from each file PATH
                student.ListOfFileNames = new List<string>(student.NumFiles);
                for (int j=0; j < student.NumFiles; j++)  
                {
                    string fileName = Path.GetFileName(student.ListOfFilePaths[j]).ToString();
                    student.ListOfFileNames.Add(fileName);
                }
                
                // Add this student to the list of students
                listOfStudents.Add(student);

                swApp.CloseAllDocuments(true);
                Console.WriteLine("Finished Student {0}.", i + 1);
            }
             // swApp.Visible = true;
        }


        // Loops through folder (specified in Form text box) and populates a list of Student classes
        private void ScanDirectoriesButton_Click(object sender, EventArgs e)
        {
            // Check if Solidworks.exe is open
            if (swApp == null)
            {
                ShowNonFatalError("Solidworks.exe is not open (var 'swApp' is null)!\n\nPlease start Solidworks and try again.");
                return;
            }

            rootDirectory = textBox4.Text;  // Folder path for all student folders
            List<string> listOfStudentFolders = EnumerateFolders(rootDirectory);  // Get list of student folder paths
            numSubmissions = listOfStudentFolders.Count();

            // Fill the class var "listOfStudents" with info from each student folder
            PopulateListOfStudents(listOfStudentFolders);

            // Check if population of student list failed
            if (listOfStudents == null)
            {
                ShowNonFatalError("Failed to populate list of students from root folder\n\nPlease try again.");
                return;
            }

            // Fills the treeView1 control with folder, file, etc. nodes using class variable 'listOfStudents'
            PopulateTreeViewWithStudents();  
        }




        // Fill the TreeView control with each student folder and their .SLDPRT files
        private void PopulateTreeViewWithStudents()
        {

            treeView1.Nodes.Clear();
            StudentInfo student;
            var rootNode = new TreeNode(new DirectoryInfo(rootDirectory).Name); // Get node name from rootDirectory folder name (from path)
            NodeInfo nodeInfo = new NodeInfo("Root");  // A custom class (ie. NodeInfo) to track the heirarchy of the Nodes
            nodeInfo.FolderPath = rootDirectory;
            rootNode.Tag = nodeInfo;

            // Loop through all student folders in root directory
            for (int i = 0; i < numSubmissions; i++)
            {
                student = listOfStudents[i];

                StudentNodeInfo folderNodeInfo = new StudentNodeInfo(student);  // A custom class (ie. NodeInfo) to track the heirarchy of the Nodes
                student.NodeInfo = folderNodeInfo;  // Assign each student it's position in the tree (ie. using studentIndex = i)
                var childFolderNode = new TreeNode(folderNodeInfo.Name);  // Make a new child node with text = Student's name

                childFolderNode.Tag = folderNodeInfo;  // Store NodeInfo in this Node's Tag object (retrieve later by casting Tag to NodeInfo)
                rootNode.Nodes.Add(childFolderNode);  // Add this child student folder node under the root node

                // Loop through all files in current student folder
                for (int j = 0; j < student.NumFiles; j++)
                {
                     PartNodeInfo fileNodeInfo = new PartNodeInfo(student, j);  // A custom class (ie. NodeInfo) to track the heirarchy of the Nodes
                    var childFileNode = new TreeNode(fileNodeInfo.Name);  // Make the Node title the file's name

                    childFileNode.Tag = fileNodeInfo;
                    childFolderNode.Nodes.Add(childFileNode);  // Add this child file node under the student folder node
                }
            }

            treeView1.Nodes.Add(rootNode);

            PrintAllNodeTags(rootNode);
        }


        // Print all Node Tag key:value pairs (including any child Nodes, too)
        private void PrintAllNodeTags(TreeNode parentNode)
        {
            if (parentNode == null)
            {
                return;
            }
            else if (parentNode.Nodes.Count <= 0)
            {
                return;
            }

            int i = 0;
            TreeNode currentNode = new TreeNode();
            
            // Loop through all children of parentNode
            do
            {
                currentNode = parentNode.Nodes[i];

                // Print out any/all key:value pairs found in the nodes' Tag object
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(currentNode.Tag))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(currentNode.Tag);
                    Console.WriteLine("{0}={1}", name, value);
                }

                // Call this function recursively for any children of currentNode
                if (currentNode.Nodes.Count > 0)
                {
                    PrintAllNodeTags(currentNode);
                }

                i++;
            } while (currentNode.NextNode != null);
        }



        // Handle the selected tree view Node (node tyes Root, Folder, Part, etc.)
        private void HandleTreeViewSelection()
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode == null)  // Tree View node is selected
                return;

            PrintAllNodeTags(selectedNode);

            NodeInfo nodeInfo = (NodeInfo)selectedNode.Tag;  // Retrieve this node's Tag object as a custom class
            Console.WriteLine("node level = " + nodeInfo.NodeLevel + ")");

            if (nodeInfo.NodeLevel == "Root")  // Selected node is not root
            {  
                return;
            }
            else if (nodeInfo.NodeLevel == "Student")
            {
                Console.WriteLine("This is a student folder.");
            }
            else if (nodeInfo.NodeLevel == "Part")
            {
                swApp.Visible = false;
                if (OpenPartFromNode(selectedNode))
                {
                    // Only add sketches if there aren't Sketch node children already
                    if (selectedNode.GetNodeCount(true) == 0)
                    {
                        AddSketchesToPartNode(selectedNode);
                    }
                }
                swApp.Visible = true;
            }
            else if (nodeInfo.NodeLevel == "Sketch")
            {
                OpenSketchFromNode(selectedNode);
            }
            else
            {
                Console.WriteLine("Invalid node selection (node level = " + nodeInfo.NodeLevel + ")");
            }
        }


        // Open selection from Enter key on treeView1 node
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // "Enter" key is pressed
            if (e.KeyCode == Keys.Enter)
            {
                // PrintAllNodeTags(treeView1.Nodes[0]);  // Print all key:value pairs for each node's Tag object
                HandleTreeViewSelection();
            }
        }

        // Open selection from tree view's "load" button
        private void LoadSelectionButton_Click(object sender, EventArgs e)
        {
                // PrintAllNodeTags(treeView1.Nodes[0]);  // Print all key:value pairs for each node's Tag object
                HandleTreeViewSelection();
        }




        // Adds child Sketch nodes to treeView1 node, given a TreeNode
        private void AddSketchesToPartNode(TreeNode partNode)
        {
            PartNodeInfo pNodeInfo = (PartNodeInfo) partNode.Tag;  // Retrieve the custom NodeInfo class from the node's Tag object

            ModelDoc2 currentModel = default(ModelDoc2);
            currentModel = GetSolidworksModelFromFile(pNodeInfo.FilePath);

            List<Feature> listOfSketches = new List<Feature>();
            listOfSketches = EnumerateSketchesInSolidworksPart(currentModel);

            if (listOfSketches == null)
            {
                ShowNonFatalError("Failed to get sketches from part '"+pNodeInfo.FileName+"'");
                return;
            }

            var sketchNode = new TreeNode();
            for (int i=0; i < listOfSketches.Count; i++)
            {
                
                SketchNodeInfo sNodeInfo = new SketchNodeInfo(pNodeInfo, listOfSketches[i], i);  // A custom class (ie. NodeInfo) to track the heirarchy of the Nodes
                sketchNode = new TreeNode(sNodeInfo.Name);  // Make the Node title the file's name
                sketchNode.Tag = sNodeInfo;
                partNode.Nodes.Add(sketchNode);  // Add this child file node under the student folder node
            }
        }

        // Opens a Solidworks part, given a part TreeNode 
        private bool OpenPartFromNode(TreeNode partNode)
        {


            // Gets the current part's name (if open), to break if new part is already open
            ModelDoc2 currentPart = default(ModelDoc2);
            currentPart = swApp.IActiveDoc2;
            string currentPartName;
            if (currentPart == null)
                currentPartName = "";
            else
                currentPartName = currentPart.GetTitle();

            // Get custom PartNodeInfo object from this node
            PartNodeInfo pNodeInfo = (PartNodeInfo)partNode.Tag;
            string fileName = pNodeInfo.FileName;

            // Exit if same part is already open
            if (fileName == currentPartName)
            {
                Console.WriteLine("Part is already opened. Skipping PartOpen.");
                currentModel = currentPart;

                // Exit out of any opened sketches
                if (currentModel.SketchManager.ActiveSketch != null)
                    currentModel.InsertSketch2(true);

                return true;
            }

            // Close current part and open new part
            Console.WriteLine("Opening student {0}'s part file: '{1}'.", pNodeInfo.Student.Name, fileName);
            CloseCurrentPart();

            ModelDoc2 newPart = default(ModelDoc2);
            newPart = GetSolidworksModelFromFile(pNodeInfo.FilePath);
            if (newPart == null)  // Opens up the new part
            {
                Console.WriteLine("Failed to open part file '{0}'", fileName);
                return false;
            }

            currentModel = newPart;

            Console.WriteLine("Opened part successfully.");
            ExpandAllSolidworksFeaturesInTree(newPart);
            return true;
        }


        // Opens a Solidworks part, given a part TreeNode 
        private bool OpenSketchFromNode(TreeNode sketchNode)
        {
            // Get custom SketchNodeInfo object from this node
            SketchNodeInfo sNodeInfo = (SketchNodeInfo)sketchNode.Tag;
            if (sNodeInfo == null)
            {
                ShowNonFatalError("Sketch node is missing SketchNodeInfo tag");
                return false;
            }

            // Get the sketch feature from SketchNodeInfo
            string sketchName = sNodeInfo.SketchName;
            if (sketchName == null)
            {
                ShowNonFatalError("SketchNodeInfo is missing a Sketch Feature variable");
                return false;
            }

            // Opens the part from sketch's parent node (if not already open)
            if (!OpenPartFromNode(sketchNode.Parent))
            {
                ShowNonFatalError("Failed to open part from sketch node's parent node");
                return false;
            }

            // Clear all previous selections in part
            currentModel.ClearSelection2(true);

            // Open "Edit Sketch" and make sketch Normal-To (ctrl+8 shortcut)
            OpenSketchNormalToByName(sketchName);

            return false;
        }


        // Open "Edit Sketch" and make sketch Normal-To (ctrl+8 shortcut) given the Feature
        private bool OpenSketchNormalToByName(string sketchName)
        {
            // Exit out of any opened sketches
            if (currentModel.SketchManager.ActiveSketch != null)
                currentModel.InsertSketch2(true);

            ModelDocExtension swModelDocExt = default(ModelDocExtension);
            swModelDocExt = currentModel.Extension;
            bool selected = swModelDocExt.SelectByID2(sketchName, "SKETCH", 0, 0, 0, false, 0, null, 0);

            if (!selected)
            {
                ShowNonFatalError("Failed to select sketch '"+sketchName+"'");
                return false;
            }

            // Open sketch for editing ("Edit Sketch" in Solidworks UI)
            currentModel.EditSketchOrSingleSketchFeature();

            // Run the 'Normal-To' view command
            if (RunNormalToSketchCommand() != 0)
            {
                return false;
            }

            return true;
        }


        // Closes the currently-open part, if it exists
        private void CloseCurrentPart()
        {
            if (swApp != null)
            {
                ModelDoc2 currentModel = swApp.IActiveDoc2;
                if (currentModel != null)
                {
                    swApp.CloseAllDocuments(true);
                }
            }
        }

        // Closes Solidworks with/without a messagebox prompt to confirm
        public void CloseSolidworks(bool withPrompt)
        {
            if (withPrompt)
            {
                if (MessageBox.Show("Ok to close Solidworks?", "Close Solidworks", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly)
                    == DialogResult.Yes)
                { }
            }
            Console.Write("\nExiting... ");
            if (swApp == null)
            {
                ShowNonFatalError("Solidworks isn't open, can't close it.");
                return;
            }

            swApp.ExitApp();
            swApp = null;
            Console.Write("Exited Solidworks.\n");
        }




        // Prompts to close Solidworks when form closes
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (swApp != null) {
                CloseSolidworks(true);  // Close solidworks with a prompt
            }
            
        }


    }
}

public class StudentInfo
{
    public List<string> ListOfFilePaths { get; set; }
    public List<string> ListOfFileNames { get; set; }
    public string FolderPath { get; set; }
    public int NumFiles { get; set; }
    public string Name { get; set; }
    public NodeInfo NodeInfo { get; set; }
    public int Index { get; set; }
}



public class SketchInfo
{
    public string Name { get; set; }
}



public class NodeInfo
{
    public string Name { get; set; }
    public string NodeLevel { get; set; }
    public string FolderPath { get; set; }

    public NodeInfo(string level)
    {
        NodeLevel = level;
    }
}

public class StudentNodeInfo : NodeInfo
{
    public StudentInfo Student { get; set; }
    public int StudentIndex { get; set; }
    public int NumFiles { get; set; }


    public StudentNodeInfo(StudentInfo student)
        :base ("")
    {
        NodeLevel = "Student";
        Student = student;
        StudentIndex = Student.Index;
        FolderPath = Student.FolderPath;
        NumFiles = Student.NumFiles;
        Name = Student.Name;
    }
}


public class PartNodeInfo : StudentNodeInfo
{
    public int PartIndex { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }

    public PartNodeInfo(StudentInfo student, int partIndex)
        :base(student)
    {
        NodeLevel = "Part";
        PartIndex = partIndex;
        FileName = Student.ListOfFileNames[partIndex];
        FilePath = Student.ListOfFilePaths[partIndex];
        Name = FileName;
    }
}


public class SketchNodeInfo : PartNodeInfo
{
    public string ConvertConstrainedToString(int constrainedStatus)
    {
        if (constrainedStatus == 3)  // Fully defined
            return "";
        else if (constrainedStatus == 2)  // Under constrained
            return "(-)";
        else if (constrainedStatus == 4)  // Over constrained
            return "(+)";
        else
            return "(!)";
    }

    public string ConstrainedString { get; set; }
    public int SketchIndex { get; set; }
    public string SketchName { get; set; }

    public SketchNodeInfo(PartNodeInfo partNodeInfo, Feature sketchFeature, int sketchIndex)
        : base(partNodeInfo.Student, partNodeInfo.PartIndex)
    {
        NodeLevel = "Sketch";
        SketchIndex = sketchIndex;
        ConstrainedString = "??";

        if (sketchFeature != null)
        {
            ConstrainedString = ConvertConstrainedToString(sketchFeature.GetSpecificFeature2().GetConstrainedStatus());
            SketchName = sketchFeature.Name;
        }
        Name = ConstrainedString + " " + SketchName;
    }
}