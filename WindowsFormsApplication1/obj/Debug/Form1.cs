using Microsoft.CSharp.RuntimeBinder;
using SldWorks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public class Form1 : Form
	{
		[CompilerGenerated]
		private static class <>o__12
		{
			public static CallSite<Func<CallSite, object, ModelDoc2>> <>p__0;
		}

		[CompilerGenerated]
		private static class <>o__13
		{
			public static CallSite<Func<CallSite, object, Sketch>> <>p__0;
		}

		[CompilerGenerated]
		private static class <>o__14
		{
			public static CallSite<Func<CallSite, object, Feature>> <>p__0;
		}

		[CompilerGenerated]
		private static class <>o__15
		{
			public static CallSite<Func<CallSite, object, Feature>> <>p__0;

			public static CallSite<Func<CallSite, object, Feature>> <>p__1;
		}

		[CompilerGenerated]
		private static class <>o__17
		{
			public static CallSite<Func<CallSite, object, ModelDoc2>> <>p__0;
		}

		[CompilerGenerated]
		private static class <>o__24
		{
			public static CallSite<Func<CallSite, object, object, object>> <>p__0;

			public static CallSite<Func<CallSite, object, bool>> <>p__1;
		}

		[CompilerGenerated]
		private static class <>o__29
		{
			public static CallSite<Func<CallSite, object, ModelDoc2>> <>p__0;
		}

		[CompilerGenerated]
		private static class <>o__34
		{
			public static CallSite<Func<CallSite, object, Feature>> <>p__0;
		}

		[CompilerGenerated]
		private static class <>o__43
		{
			public static CallSite<Func<CallSite, object, ModelDoc2>> <>p__0;
		}

		private SldWorks swApp;

		private int traverseLevel;

		private bool expandThis;

		private List<StudentInfo> listOfStudents;

		private int directoryIndex = 0;

		private int currentStudentIndex = 0;

		private int currentPartIndex = 0;

		private int currentSketchIndex = 0;

		private int numSubmissions = 0;

		private string rootDirectory = "";

		private IContainer components = null;

		private FolderBrowserDialog folderBrowserDialog1;

		private Button button2;

		private Label label4;

		private TextBox textBox4;

		private Button startSolidworks;

		private Label label6;

		private Label label7;

		private Label label9;

		private Label label1;

		private Label label2;

		private Button button5;

		private Button button6;

		private Button button7;

		private Button button8;

		private Button nextSubmission;

		private Button button10;

		private Button ScanDirectoriesButton;

		private TreeView treeView1;

		private BindingSource bindingSource1;

		private Button LoadSelectionButton;

		public Form1()
		{
			this.InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			bool flag = this.folderBrowserDialog1.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				this.textBox4.Text = this.folderBrowserDialog1.SelectedPath;
			}
		}

		public int RunNormalToSketchCommand()
		{
			Console.WriteLine("  Running 'Normal-To' command...");
			try
			{
				if (Form1.<>o__12.<>p__0 == null)
				{
					Form1.<>o__12.<>p__0 = CallSite<Func<CallSite, object, ModelDoc2>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(ModelDoc2), typeof(Form1)));
				}
				ModelDoc2 thisModel = Form1.<>o__12.<>p__0.Target(Form1.<>o__12.<>p__0, this.swApp.ActiveDoc);
				int currentCommandID = 169;
				string currentCommandTitle = "Normal-To";
				this.swApp.RunCommand(currentCommandID, currentCommandTitle);
				thisModel.ViewZoomtofit2();
			}
			catch (Exception e)
			{
				this.ShowFatalErrorMessage(e.ToString());
				throw;
			}
			return 0;
		}

		public int GetSketchDetails(Feature featureAsSketch)
		{
			if (Form1.<>o__13.<>p__0 == null)
			{
				Form1.<>o__13.<>p__0 = CallSite<Func<CallSite, object, Sketch>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(Sketch), typeof(Form1)));
			}
			Sketch currentSketch = Form1.<>o__13.<>p__0.Target(Form1.<>o__13.<>p__0, featureAsSketch.GetSpecificFeature2());
			int constrainedStatus = -1;
			bool flag = currentSketch != null;
			if (!flag)
			{
				Console.WriteLine("** Error: currentSketch is NULL! Something's wrong...");
			}
			return constrainedStatus;
		}

		public void iterate_each_sketch_in_model(ModelDoc2 thisModel)
		{
			Console.WriteLine("Iterating through all sketches in this model...");
			bool flag = thisModel == null;
			if (flag)
			{
				Console.WriteLine("** Error: thisModel is NULL! Something's wrong...");
			}
			int numParentFeatures = 0;
			int numSketches = 0;
			Feature currentFeature = thisModel.IFirstFeature();
			List<Feature> listOfSketches = new List<Feature>();
			while (currentFeature != null)
			{
				numParentFeatures++;
				bool flag2 = currentFeature.GetTypeName2() == "ProfileFeature";
				if (flag2)
				{
					Console.WriteLine(string.Concat(new object[]
					{
						"Iterating through parentFeature",
						numParentFeatures,
						" ('",
						currentFeature.Name,
						"') in this model..."
					}));
					this.GetSketchDetails(currentFeature);
					thisModel.ClearSelection2(true);
					bool flag3 = currentFeature.Select2(false, 0);
					if (!flag3)
					{
						Console.WriteLine(" ** ERROR: Failed to select Sketch ('" + currentFeature.Name + "')");
						break;
					}
					Console.WriteLine("Successfully selected Sketch ('" + currentFeature.Name + "')");
					listOfSketches.Add(currentFeature);
					Console.WriteLine("  Editing sketch......");
					thisModel.EditSketchOrSingleSketchFeature();
					this.RunNormalToSketchCommand();
					thisModel.InsertSketch2(true);
				}
				if (Form1.<>o__14.<>p__0 == null)
				{
					Form1.<>o__14.<>p__0 = CallSite<Func<CallSite, object, Feature>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(Feature), typeof(Form1)));
				}
				currentFeature = Form1.<>o__14.<>p__0.Target(Form1.<>o__14.<>p__0, currentFeature.GetNextFeature());
				bool flag4 = currentFeature == null;
				if (flag4)
				{
					Console.WriteLine("Failed to get next feature! (GetNextFeature() is NULL)");
				}
			}
			Console.WriteLine("Got " + numSketches + " sketches from this model");
		}

		public int iterate_each_feature_in_Solidworks_file(string filePath)
		{
			int numFeaturesInPart = 0;
			ModelDoc2 currentModelDoc = this.GetSolidworksModelFromFile(filePath);
			bool flag = currentModelDoc == null;
			int result;
			if (flag)
			{
				Console.WriteLine("** Error: Model is NULL! Something's wrong...");
				result = -1;
			}
			else
			{
				string fileName = new FileInfo(filePath).Name;
				if (Form1.<>o__15.<>p__0 == null)
				{
					Form1.<>o__15.<>p__0 = CallSite<Func<CallSite, object, Feature>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Feature), typeof(Form1)));
				}
				Feature currentFeature = Form1.<>o__15.<>p__0.Target(Form1.<>o__15.<>p__0, currentModelDoc.FirstFeature());
				while (currentFeature != null)
				{
					Console.WriteLine("  Feature " + currentFeature.Name + " created by " + currentFeature.CreatedBy);
					if (Form1.<>o__15.<>p__1 == null)
					{
						Form1.<>o__15.<>p__1 = CallSite<Func<CallSite, object, Feature>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Feature), typeof(Form1)));
					}
					currentFeature = Form1.<>o__15.<>p__1.Target(Form1.<>o__15.<>p__1, currentFeature.GetNextFeature());
					numFeaturesInPart++;
				}
				Console.WriteLine("Closing Solidworks file '" + fileName + "'...");
				this.swApp.CloseDoc(filePath);
				Console.WriteLine("Closed successfully.");
				Console.WriteLine("Number of features iterated through in this part: " + numFeaturesInPart);
				result = numFeaturesInPart;
			}
			return result;
		}

		public void open_Solidworks_files(string folderPath)
		{
			bool flag = Directory.Exists(folderPath);
			if (flag)
			{
				foreach (string filePath in Directory.EnumerateFiles(folderPath, "*.SLDPRT"))
				{
					string fileName = new FileInfo(filePath).Name;
					bool flag2 = this.IsTempFile(filePath);
					if (flag2)
					{
						Console.WriteLine("Ignoring temp. file: '" + fileName + "'");
					}
					else
					{
						Console.WriteLine("Filename is: '" + fileName + "'");
						ModelDoc2 currentModel = this.GetSolidworksModelFromFile(filePath);
						bool flag3 = currentModel == null;
						if (flag3)
						{
							Console.WriteLine("** ERROR: Failed to open model");
						}
						this.iterate_each_sketch_in_model(currentModel);
						this.swApp.CloseDoc(currentModel.GetTitle());
					}
				}
			}
			else
			{
				this.ShowNonFatalError("** Error: Invalid folder path:\n'" + folderPath + "'");
			}
		}

		public ModelDoc2 GetSolidworksModelFromFile(string filePath)
		{
			ModelDoc2 currentModel = null;
			string fileName = new FileInfo(filePath).Name;
			int i_errors = 0;
			int i_warnings = 0;
			bool flag = this.swApp == null;
			ModelDoc2 result;
			if (flag)
			{
				this.ShowNonFatalError("Solidworks.exe is not open (var 'swApp' is null)!\n\nPlease start Solidworks and try again.");
				result = null;
			}
			else
			{
				if (Form1.<>o__17.<>p__0 == null)
				{
					Form1.<>o__17.<>p__0 = CallSite<Func<CallSite, object, ModelDoc2>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(ModelDoc2), typeof(Form1)));
				}
				currentModel = Form1.<>o__17.<>p__0.Target(Form1.<>o__17.<>p__0, this.swApp.ActiveDoc);
				bool flag2 = currentModel != null;
				if (flag2)
				{
					Console.WriteLine("Already-open model is titled '{0}'", currentModel.GetTitle());
					bool flag3 = currentModel.GetTitle() == fileName;
					if (flag3)
					{
						Console.WriteLine("Returning current model");
						result = currentModel;
						return result;
					}
				}
				try
				{
					Console.Write("Opening Solidworks file '" + fileName + "'... ");
					currentModel = this.swApp.OpenDoc6(filePath, 1, 1, "", ref i_errors, ref i_warnings);
					Console.Write("Opened.\n");
					Console.WriteLine("  Errors: " + i_errors);
					Console.WriteLine("  Warnings: " + i_warnings);
				}
				catch (Exception openDocExcept)
				{
					Console.WriteLine(openDocExcept.ToString());
					throw;
				}
				bool flag4 = currentModel == null;
				if (flag4)
				{
					this.ShowNonFatalError("Model failed to open (var 'currentModel' is null)!\n\nPlease try again.");
					result = null;
				}
				else
				{
					result = currentModel;
				}
			}
			return result;
		}

		public int get_num_Solidworks_files_in_folder(string folderPath)
		{
			int numSolidworksFilesInFolder = 0;
			bool flag = Directory.Exists(folderPath);
			int result;
			if (flag)
			{
				foreach (string file in Directory.EnumerateFiles(folderPath, "*.SLDPRT"))
				{
					numSolidworksFilesInFolder++;
				}
				string folderName = new DirectoryInfo(folderPath).Name;
				Console.WriteLine(string.Concat(new object[]
				{
					"Found ",
					numSolidworksFilesInFolder,
					" Solidworks files in folder: '",
					folderName,
					"'"
				}));
				result = numSolidworksFilesInFolder;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		public void ShowNonFatalError(string message)
		{
			MessageBox.Show("Error: '" + message + "'\n\nPlease try again.", "Try again", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
			Console.WriteLine(message);
		}

		public void ShowFatalErrorMessage(string message)
		{
			Console.WriteLine(message);
			bool flag = MessageBox.Show("ERROR: '" + message + "'\n\nExiting...", "Exiting script", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.OK;
			if (flag)
			{
			}
			this.CloseSolidworks(false);
			Application.Exit();
		}

		public void ShowCloseSolidworksMessage(string message)
		{
			Console.WriteLine(message);
			bool flag = MessageBox.Show("ERROR: '" + message + "'\n\nExiting...", "Exiting script", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.OK;
			if (flag)
			{
			}
			this.CloseSolidworks(false);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private bool IsTempFile(string filePath)
		{
			string fileName = new FileInfo(filePath).Name;
			bool flag = fileName[0] == '~';
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = fileName[0] == '.';
				result = flag2;
			}
			return result;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Console.WriteLine("Opening Solidworks.exe ...");
			this.swApp = (SldWorks)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("F16137AD-8EE8-4D2A-8CAC-DFF5D1F67522")));
			if (Form1.<>o__24.<>p__1 == null)
			{
				Form1.<>o__24.<>p__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(Form1), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Func<CallSite, object, bool> arg_CE_0 = Form1.<>o__24.<>p__1.Target;
			CallSite arg_CE_1 = Form1.<>o__24.<>p__1;
			if (Form1.<>o__24.<>p__0 == null)
			{
				Form1.<>o__24.<>p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(Form1), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			bool flag = arg_CE_0(arg_CE_1, Form1.<>o__24.<>p__0.Target(Form1.<>o__24.<>p__0, this.swApp.ActiveDoc, null));
			if (flag)
			{
				Console.WriteLine("Solidworks already open, restarting it...");
				this.CloseSolidworks(false);
				this.button3_Click(null, null);
			}
			else
			{
				Console.WriteLine("Opened Solidworks.exe successfully.");
				this.startSolidworks.Enabled = false;
			}
		}

		public string check_this_Solidworks_directory(string folderPath)
		{
			string folderName = folderPath.Substring(folderPath.LastIndexOf("\\") + 1);
			int num_Solidworks_parts_in_folder = this.get_num_Solidworks_files_in_folder(folderPath);
			bool flag = num_Solidworks_parts_in_folder == -1;
			string result;
			if (flag)
			{
				result = "Invalid folder path '" + folderName + "'.";
			}
			else
			{
				bool flag2 = num_Solidworks_parts_in_folder == 0;
				if (flag2)
				{
					result = "No Solidworks part files found in selected folder:\n" + folderName;
				}
				else
				{
					result = "Valid";
				}
			}
			return result;
		}

		public void run_main_script()
		{
			Console.WriteLine("Opened Solidworks.exe successfully.");
			this.swApp.Visible = true;
			string folderPath = this.textBox4.Text;
			string resultOfFolderCheck = this.check_this_Solidworks_directory(folderPath);
			bool flag = resultOfFolderCheck != "Valid";
			if (flag)
			{
				this.ShowCloseSolidworksMessage(resultOfFolderCheck);
			}
			else
			{
				string folderName = new DirectoryInfo(folderPath).Name;
				Console.WriteLine("Folder name is: '" + folderName + "'");
				Console.WriteLine("Opening all Solidworks files in folder '" + folderName + "'");
				Console.WriteLine("Done opening files.");
				this.CloseSolidworks(true);
			}
		}

		private void button_ExitScript_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void ExpandAllSolidworksFeaturesInTree(ModelDoc2 model)
		{
			Console.WriteLine("Expanding all features in Solidworks part...");
			bool flag = model == null;
			if (flag)
			{
				this.ShowNonFatalError("Failed to open model. (ActiveDoc is null)");
			}
			else
			{
				FeatureManager featureMgr = model.FeatureManager;
				TreeControlItem rootNode = featureMgr.GetFeatureTreeRootItem2(1);
				bool flag2 = rootNode == null;
				if (flag2)
				{
					this.ShowNonFatalError("Failed to get root node of Feature Tree from part. (rootNode is null)");
				}
				else
				{
					int nodeObjectType = rootNode.ObjectType;
					object nodeObject = rootNode.Object;
					Console.WriteLine("Node text = '" + rootNode.Text + "'");
					bool flag3 = rootNode.Text != "Annotations" && rootNode.Text != "History";
					if (flag3)
					{
						rootNode.Expanded = true;
						this.traverseLevel++;
					}
					this.traverseLevel--;
					Console.WriteLine("Expanded all features in part successfully.");
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (Form1.<>o__29.<>p__0 == null)
			{
				Form1.<>o__29.<>p__0 = CallSite<Func<CallSite, object, ModelDoc2>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(ModelDoc2), typeof(Form1)));
			}
			ModelDoc2 myModel = Form1.<>o__29.<>p__0.Target(Form1.<>o__29.<>p__0, this.swApp.ActiveDoc);
			bool flag = myModel == null;
			if (flag)
			{
				this.ShowNonFatalError("Failed to open model. (ActiveDoc is null)");
			}
			else
			{
				this.traverseLevel = 0;
				this.ExpandAllSolidworksFeaturesInTree(myModel);
			}
		}

		private void nextSubmission_Click_1(object sender, EventArgs e)
		{
		}

		private List<string> EnumerateFolders(string rootFolderPath)
		{
			List<string> result;
			try
			{
				List<string> listOfSubfolderPaths = Directory.GetDirectories(rootFolderPath).ToList<string>();
				foreach (string path in listOfSubfolderPaths)
				{
				}
				listOfSubfolderPaths.Sort();
				result = listOfSubfolderPaths;
			}
			catch (UnauthorizedAccessException UAEx)
			{
				this.ShowNonFatalError(UAEx.Message);
				Console.WriteLine(UAEx.Message);
				result = null;
			}
			catch (PathTooLongException PathEx)
			{
				this.ShowNonFatalError(PathEx.Message);
				result = null;
			}
			return result;
		}

		private List<string> EnumerateSolidworksFiles(string rootFolderPath)
		{
			List<string> result;
			try
			{
				List<string> listOfFilePaths = new List<string>();
				foreach (string filePath in Directory.EnumerateFiles(rootFolderPath, "*.SLDPRT", SearchOption.AllDirectories))
				{
					bool flag = !this.IsTempFile(filePath);
					if (flag)
					{
						listOfFilePaths.Add(filePath);
					}
				}
				listOfFilePaths.Sort();
				result = listOfFilePaths;
			}
			catch (UnauthorizedAccessException UAEx)
			{
				this.ShowNonFatalError(UAEx.Message);
				result = null;
			}
			catch (PathTooLongException PathEx)
			{
				this.ShowNonFatalError(PathEx.Message);
				result = null;
			}
			return result;
		}

		private List<ModelDoc2> EnumerateSolidworksParts(string folderPath)
		{
			bool flag = this.swApp == null;
			List<ModelDoc2> result;
			if (flag)
			{
				this.ShowNonFatalError("Solidworks.exe is not open (var 'swApp' is null)!\n\nPlease start Solidworks and try again.");
				result = null;
			}
			else
			{
				List<ModelDoc2> listOfParts = new List<ModelDoc2>();
				List<string> listOfFilePaths = new List<string>();
				listOfFilePaths = this.EnumerateSolidworksFiles(folderPath);
				bool flag2 = listOfFilePaths == null;
				if (flag2)
				{
					this.ShowNonFatalError("Did not find any files in folder: " + folderPath);
				}
				int numParts = listOfFilePaths.Count;
				for (int i = 0; i < numParts; i++)
				{
					ModelDoc2 currentModel = this.GetSolidworksModelFromFile(listOfFilePaths[i]);
					bool flag3 = currentModel == null;
					if (flag3)
					{
						this.ShowNonFatalError("Failed to get model from folder: " + folderPath);
					}
					else
					{
						listOfParts.Add(currentModel);
					}
				}
				result = listOfParts;
			}
			return result;
		}

		private List<Feature> EnumerateSketchesInSolidworksPart(ModelDoc2 thisModel)
		{
			bool flag = thisModel == null;
			List<Feature> result;
			if (flag)
			{
				this.ShowNonFatalError("** Error: thisModel is NULL! Something's wrong...");
				result = null;
			}
			else
			{
				Console.WriteLine("Enumerating all sketches in model '{0}'...", thisModel.GetTitle());
				List<Feature> listOfSketches = new List<Feature>();
				int numParentFeatures = 0;
				int numSketches = 0;
				int numFeatures = thisModel.GetFeatureCount();
				for (int i = 0; i < numFeatures; i++)
				{
					if (Form1.<>o__34.<>p__0 == null)
					{
						Form1.<>o__34.<>p__0 = CallSite<Func<CallSite, object, Feature>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(Feature), typeof(Form1)));
					}
					Feature currentFeature = Form1.<>o__34.<>p__0.Target(Form1.<>o__34.<>p__0, thisModel.FeatureByPositionReverse(i));
					Console.WriteLine("Feature {0} ('" + currentFeature.Name + "')", i);
					bool flag2 = currentFeature.Name == "Origin";
					if (flag2)
					{
						break;
					}
					numParentFeatures++;
					bool flag3 = currentFeature.GetTypeName2() == "ProfileFeature";
					if (flag3)
					{
						Console.WriteLine("Successfully selected Sketch ('" + currentFeature.Name + "')");
						listOfSketches.Add(currentFeature);
					}
				}
				Console.WriteLine("Got " + numSketches + " sketches from this model");
				listOfSketches.Reverse();
				result = listOfSketches;
			}
			return result;
		}

		private void PopulateListOfStudents(List<string> listOfStudentFolders)
		{
			bool flag = this.swApp == null;
			if (flag)
			{
				this.ShowNonFatalError("Solidworks.exe is not open (var 'swApp' is null)!\n\nPlease start Solidworks and try again.");
			}
			else
			{
				this.numSubmissions = listOfStudentFolders.Count<string>();
				this.listOfStudents = new List<StudentInfo>(this.numSubmissions);
				this.swApp.Visible = false;
				for (int i = 0; i < this.numSubmissions; i++)
				{
					Console.WriteLine("Populating Student {0} of {1}...", i + 1, this.numSubmissions);
					string folderPath = listOfStudentFolders[i];
					string folderName = folderPath.Substring(folderPath.LastIndexOf("\\") + 1);
					StudentInfo student = new StudentInfo();
					student.Name = folderName.Split(new char[]
					{
						'_'
					})[0];
					student.ListOfFilePaths = this.EnumerateSolidworksFiles(folderPath);
					student.NumFiles = student.ListOfFilePaths.Count<string>();
					student.FolderPath = folderPath;
					student.ListOfFileNames = new List<string>(student.NumFiles);
					for (int j = 0; j < student.NumFiles; j++)
					{
						string fileName = Path.GetFileName(student.ListOfFilePaths[j]).ToString();
						student.ListOfFileNames.Add(fileName);
					}
					this.listOfStudents.Add(student);
					this.swApp.CloseAllDocuments(true);
					Console.WriteLine("Finished Student {0}.", i + 1);
				}
			}
		}

		private void ScanDirectoriesButton_Click(object sender, EventArgs e)
		{
			bool flag = this.swApp == null;
			if (flag)
			{
				this.ShowNonFatalError("Solidworks.exe is not open (var 'swApp' is null)!\n\nPlease start Solidworks and try again.");
			}
			else
			{
				this.rootDirectory = this.textBox4.Text;
				List<string> listOfStudentFolders = this.EnumerateFolders(this.rootDirectory);
				this.numSubmissions = listOfStudentFolders.Count<string>();
				this.PopulateListOfStudents(listOfStudentFolders);
				bool flag2 = this.listOfStudents == null;
				if (flag2)
				{
					this.ShowNonFatalError("Failed to populate list of students from root folder\n\nPlease try again.");
				}
				else
				{
					this.PopulateTreeViewWithStudents();
				}
			}
		}

		private void PopulateTreeViewWithStudents()
		{
			this.treeView1.Nodes.Clear();
			TreeNode rootNode = new TreeNode(new DirectoryInfo(this.rootDirectory).Name);
			rootNode.Tag = new NodeInfo("Root")
			{
				FolderPath = this.rootDirectory
			};
			for (int i = 0; i < this.numSubmissions; i++)
			{
				StudentInfo student = this.listOfStudents[i];
				StudentNodeInfo folderNodeInfo = new StudentNodeInfo(student);
				student.NodeInfo = folderNodeInfo;
				TreeNode childFolderNode = new TreeNode(folderNodeInfo.Name);
				childFolderNode.Tag = folderNodeInfo;
				rootNode.Nodes.Add(childFolderNode);
				for (int j = 0; j < student.NumFiles; j++)
				{
					PartNodeInfo fileNodeInfo = new PartNodeInfo(student, j);
					TreeNode childFileNode = new TreeNode(fileNodeInfo.Name);
					childFileNode.Tag = fileNodeInfo;
					childFolderNode.Nodes.Add(childFileNode);
				}
			}
			this.treeView1.Nodes.Add(rootNode);
		}

		private void PrintAllNodeTags(TreeNode parentNode)
		{
			bool flag = parentNode == null;
			if (!flag)
			{
				int i = 0;
				TreeNode currentNode = new TreeNode();
				do
				{
					currentNode = parentNode.Nodes[i];
					foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(currentNode.Tag))
					{
						string name = descriptor.Name;
						object value = descriptor.GetValue(currentNode.Tag);
						Console.WriteLine("{0}={1}", name, value);
					}
					bool flag2 = currentNode.Nodes.Count > 0;
					if (flag2)
					{
						this.PrintAllNodeTags(currentNode);
					}
					i++;
				}
				while (currentNode.NextNode != null);
			}
		}

		private void HandleTreeViewSelection()
		{
			TreeNode selectedNode = this.treeView1.SelectedNode;
			bool flag = selectedNode == null;
			if (!flag)
			{
				NodeInfo nodeInfo = (NodeInfo)selectedNode.Tag;
				Console.WriteLine("node level = " + nodeInfo.NodeLevel + ")");
				bool flag2 = nodeInfo.NodeLevel == "Root";
				if (!flag2)
				{
					bool flag3 = nodeInfo.NodeLevel == "Student";
					if (flag3)
					{
						Console.WriteLine("This is a student folder.");
					}
					else
					{
						bool flag4 = nodeInfo.NodeLevel == "Part";
						if (flag4)
						{
							this.swApp.Visible = false;
							bool flag5 = this.OpenStudentPartFromNode(selectedNode, false);
							if (flag5)
							{
								this.AddSketchesToPartNode(selectedNode);
							}
							this.swApp.Visible = true;
						}
						else
						{
							Console.WriteLine("Invalid node selection (node level = " + nodeInfo.NodeLevel + ")");
						}
					}
				}
			}
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Return;
			if (flag)
			{
				this.HandleTreeViewSelection();
			}
		}

		private void LoadSelectionButton_Click(object sender, EventArgs e)
		{
			this.HandleTreeViewSelection();
		}

		private void AddSketchesToPartNode(TreeNode partNode)
		{
			PartNodeInfo pNodeInfo = (PartNodeInfo)partNode.Tag;
			ModelDoc2 currentModel = this.GetSolidworksModelFromFile(pNodeInfo.FilePath);
			List<Feature> listOfSketches = new List<Feature>();
			listOfSketches = this.EnumerateSketchesInSolidworksPart(currentModel);
			bool flag = listOfSketches == null;
			if (flag)
			{
				this.ShowNonFatalError("Failed to get sketches from part '" + pNodeInfo.FileName + "'");
			}
			else
			{
				TreeNode sketchNode = new TreeNode();
				for (int i = 0; i < listOfSketches.Count; i++)
				{
					SketchInfoNode sNodeInfo = new SketchInfoNode(pNodeInfo, listOfSketches[i], i);
					sketchNode.Tag = sNodeInfo;
					sketchNode = new TreeNode(sNodeInfo.Name);
					partNode.Nodes.Add(sketchNode);
				}
			}
		}

		private bool OpenStudentPartFromNode(TreeNode partNode, bool closeOld)
		{
			if (Form1.<>o__43.<>p__0 == null)
			{
				Form1.<>o__43.<>p__0 = CallSite<Func<CallSite, object, ModelDoc2>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(ModelDoc2), typeof(Form1)));
			}
			ModelDoc2 currentPart = Form1.<>o__43.<>p__0.Target(Form1.<>o__43.<>p__0, this.swApp.ActiveDoc);
			string currentPartName = "";
			bool flag = currentPart != null;
			if (flag)
			{
				currentPartName = currentPart.GetTitle();
			}
			PartNodeInfo pNodeInfo = (PartNodeInfo)partNode.Tag;
			string fileName = pNodeInfo.FileName;
			bool flag2 = fileName == currentPartName;
			bool result;
			if (flag2)
			{
				Console.WriteLine("Part is already opened. Skipping PartOpen.");
				result = false;
			}
			else
			{
				Console.WriteLine("Opening student {0}'s part file: '{1}'.", pNodeInfo.Student.Name, fileName);
				if (closeOld)
				{
					this.CloseCurrentPart();
				}
				ModelDoc2 newPart = this.GetSolidworksModelFromFile(pNodeInfo.FilePath);
				bool flag3 = newPart == null;
				if (flag3)
				{
					Console.WriteLine("Failed to open part file '{0}'", fileName);
					result = false;
				}
				else
				{
					Console.WriteLine("Opened part successfully.");
					this.ExpandAllSolidworksFeaturesInTree(newPart);
					result = true;
				}
			}
			return result;
		}

		private void CloseCurrentPart()
		{
			bool flag = this.swApp != null;
			if (flag)
			{
				ModelDoc2 currentModel = this.swApp.IActiveDoc2;
				bool flag2 = currentModel != null;
				if (flag2)
				{
					this.swApp.CloseAllDocuments(true);
				}
			}
		}

		public void CloseSolidworks(bool withPrompt)
		{
			if (withPrompt)
			{
				bool flag = MessageBox.Show("Ok to close Solidworks?", "Close Solidworks", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes;
				if (flag)
				{
				}
			}
			Console.Write("\nExiting... ");
			bool flag2 = this.swApp == null;
			if (flag2)
			{
				this.ShowNonFatalError("Solidworks isn't open, can't close it.");
			}
			else
			{
				this.swApp.ExitApp();
				this.swApp = null;
				Console.Write("Exited Solidworks.\n");
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = this.swApp != null;
			if (flag)
			{
				this.CloseSolidworks(true);
			}
		}

		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
			this.folderBrowserDialog1 = new FolderBrowserDialog();
			this.button2 = new Button();
			this.label4 = new Label();
			this.textBox4 = new TextBox();
			this.startSolidworks = new Button();
			this.label6 = new Label();
			this.label7 = new Label();
			this.label9 = new Label();
			this.label1 = new Label();
			this.label2 = new Label();
			this.button5 = new Button();
			this.button6 = new Button();
			this.button7 = new Button();
			this.button8 = new Button();
			this.nextSubmission = new Button();
			this.button10 = new Button();
			this.ScanDirectoriesButton = new Button();
			this.treeView1 = new TreeView();
			this.bindingSource1 = new BindingSource(this.components);
			this.LoadSelectionButton = new Button();
			((ISupportInitialize)this.bindingSource1).BeginInit();
			base.SuspendLayout();
			this.folderBrowserDialog1.Description = "Select root folder of downloaded Canvas submissions";
			this.folderBrowserDialog1.SelectedPath = "Z:\\Windows.Documents\\Downloads";
			this.button2.Location = new Point(296, 37);
			this.button2.Margin = new Padding(10, 3, 10, 3);
			this.button2.Name = "button2";
			this.button2.Size = new Size(72, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Browse";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.label4.AutoSize = true;
			this.label4.Location = new Point(24, 23);
			this.label4.Name = "label4";
			this.label4.Size = new Size(187, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Select folder for student submissions...";
			this.textBox4.Location = new Point(27, 39);
			this.textBox4.MaximumSize = new Size(400, 20);
			this.textBox4.MinimumSize = new Size(180, 20);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new Size(256, 20);
			this.textBox4.TabIndex = 1;
			this.textBox4.Text = "Z:\\Windows.Documents\\My Documents\\ENGR 248\\STUDENTS (TA)\\STUDENT Lab 4 Submissions";
			this.startSolidworks.Location = new Point(26, 127);
			this.startSolidworks.Name = "startSolidworks";
			this.startSolidworks.Size = new Size(96, 26);
			this.startSolidworks.TabIndex = 3;
			this.startSolidworks.Text = "Start Solidworks";
			this.startSolidworks.UseMnemonic = false;
			this.startSolidworks.UseVisualStyleBackColor = true;
			this.startSolidworks.Click += new EventHandler(this.button3_Click);
			this.label6.AutoSize = true;
			this.label6.Location = new Point(2, 105);
			this.label6.Name = "label6";
			this.label6.Size = new Size(16, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "2)";
			this.label7.AutoSize = true;
			this.label7.Location = new Point(24, 105);
			this.label7.Name = "label7";
			this.label7.Size = new Size(149, 13);
			this.label7.TabIndex = 13;
			this.label7.Text = "Run Solidworks iteration script";
			this.label9.AutoSize = true;
			this.label9.Location = new Point(3, 23);
			this.label9.Name = "label9";
			this.label9.Size = new Size(16, 13);
			this.label9.TabIndex = 15;
			this.label9.Text = "1)";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(23, 177);
			this.label1.Name = "label1";
			this.label1.Size = new Size(113, 13);
			this.label1.TabIndex = 20;
			this.label1.Text = "Control script iterations";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(1, 177);
			this.label2.Name = "label2";
			this.label2.Size = new Size(16, 13);
			this.label2.TabIndex = 19;
			this.label2.Text = "3)";
			this.button5.Cursor = Cursors.Default;
			this.button5.Enabled = false;
			this.button5.Image = (Image)resources.GetObject("button5.Image");
			this.button5.Location = new Point(27, 202);
			this.button5.Name = "button5";
			this.button5.Size = new Size(77, 54);
			this.button5.TabIndex = 23;
			this.button5.UseVisualStyleBackColor = true;
			this.button6.Cursor = Cursors.Default;
			this.button6.Location = new Point(293, 233);
			this.button6.Name = "button6";
			this.button6.Size = new Size(75, 23);
			this.button6.TabIndex = 24;
			this.button6.Text = "Next sketch";
			this.button6.UseVisualStyleBackColor = true;
			this.button7.Location = new Point(293, 262);
			this.button7.Name = "button7";
			this.button7.Size = new Size(75, 23);
			this.button7.TabIndex = 27;
			this.button7.Text = "Next part";
			this.button7.UseVisualStyleBackColor = true;
			this.button8.Enabled = false;
			this.button8.Location = new Point(27, 262);
			this.button8.Name = "button8";
			this.button8.Size = new Size(77, 30);
			this.button8.TabIndex = 28;
			this.button8.Text = "Previous part";
			this.button8.UseVisualStyleBackColor = true;
			this.nextSubmission.Location = new Point(293, 291);
			this.nextSubmission.Name = "nextSubmission";
			this.nextSubmission.Size = new Size(75, 23);
			this.nextSubmission.TabIndex = 29;
			this.nextSubmission.Text = "Next student";
			this.nextSubmission.UseVisualStyleBackColor = true;
			this.nextSubmission.Click += new EventHandler(this.nextSubmission_Click_1);
			this.button10.Location = new Point(27, 298);
			this.button10.Name = "button10";
			this.button10.Size = new Size(77, 21);
			this.button10.TabIndex = 30;
			this.button10.Text = "button10";
			this.button10.UseVisualStyleBackColor = true;
			this.ScanDirectoriesButton.Location = new Point(27, 66);
			this.ScanDirectoriesButton.Name = "ScanDirectoriesButton";
			this.ScanDirectoriesButton.Size = new Size(75, 23);
			this.ScanDirectoriesButton.TabIndex = 33;
			this.ScanDirectoriesButton.Text = "Scan folder";
			this.ScanDirectoriesButton.UseVisualStyleBackColor = true;
			this.ScanDirectoriesButton.Click += new EventHandler(this.ScanDirectoriesButton_Click);
			this.treeView1.FullRowSelect = true;
			this.treeView1.HideSelection = false;
			this.treeView1.HotTracking = true;
			this.treeView1.Location = new Point(12, 409);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new Size(368, 307);
			this.treeView1.TabIndex = 34;
			this.bindingSource1.DataSource = typeof(StudentInfo);
			this.LoadSelectionButton.Location = new Point(13, 380);
			this.LoadSelectionButton.Name = "LoadSelectionButton";
			this.LoadSelectionButton.Size = new Size(89, 23);
			this.LoadSelectionButton.TabIndex = 7;
			this.LoadSelectionButton.Text = "Load selection";
			this.LoadSelectionButton.UseVisualStyleBackColor = true;
			this.LoadSelectionButton.Click += new EventHandler(this.LoadSelectionButton_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(392, 730);
			base.Controls.Add(this.LoadSelectionButton);
			base.Controls.Add(this.treeView1);
			base.Controls.Add(this.ScanDirectoriesButton);
			base.Controls.Add(this.button10);
			base.Controls.Add(this.nextSubmission);
			base.Controls.Add(this.button8);
			base.Controls.Add(this.button7);
			base.Controls.Add(this.button6);
			base.Controls.Add(this.button5);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.startSolidworks);
			base.Controls.Add(this.textBox4);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.button2);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.Name = "Form1";
			this.Text = "Form1";
			base.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
			base.Load += new EventHandler(this.Form1_Load);
			base.KeyDown += new KeyEventHandler(this.Form1_KeyDown);
			((ISupportInitialize)this.bindingSource1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
