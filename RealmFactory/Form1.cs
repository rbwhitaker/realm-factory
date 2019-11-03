using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RealmEngine.UndoRedo;
using Starbound.RealmFactory.UserInterface;
using Starbound.Common.IO;
using Starbound.RealmFactory.IO;
using Starbound.RealmFactory;
using Starbound.RealmFactory.DataModel;

namespace RealmEngine
{
    public partial class Form1 : Form
    {
        private FileReaderManager<Project> fileReaderManager;
        private FileWriterManager<Project> fileWriterManager;

        private UndoRedoSystem undoRedoSystem;

        private Project project;

        public Project Project
        {
            get
            {
                return project;
            }
            set
            {
                project = value;
                BuildLevelList();
                BuildTypeList();
            }
        }

        public Form1()
        {
            InitializeComponent();
            ActiveTool = Tool.Pencil;
            SetDoubleBuffered(this.levelRenderer);

            undoRedoSystem = new UndoRedoSystem();
            undoRedoSystem.SystemChanged += UndoRedoSystemChanged;
            UndoRedoSystemChanged(null, EventArgs.Empty);

            Project = new Starbound.RealmFactory.DataModel.Project();

            SetupFileReaders();
        }

        private void SetupFileReaders()
        {
            fileReaderManager = new FileReaderManager<Project>();
            FileReader<Project> fileReader = new RealmFactoryNativeFileReader();
            fileReaderManager.AddFileReader(fileReader);
            fileReaderManager.SetDefaultFileReader(fileReader);

            fileWriterManager = new FileWriterManager<Project>();
            FileWriter<Project> fileWriter = new RealmFactoryNativeFileWriter();
            fileWriterManager.AddFileWriter(fileWriter);
            fileWriterManager.SetDefaultFileWriter(fileWriter);
        }

        private bool saveNeeded = false;
        public bool SaveNeeded
        {
            get
            {
                return saveNeeded;
            }
            set
            {
                saveNeeded = value;
                if (saveNeeded)
                {
                    if (!this.Text.EndsWith("*"))
                    {
                        this.Text += "*";
                    }
                }
                else
                {
                    if (this.Text.EndsWith("*"))
                    {
                        this.Text = Text.Substring(0, Text.Length - 1);
                    }
                }
            }
        }

        public void UndoRedoSystemChanged(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = undoRedoSystem.CanUndo();
            redoToolStripMenuItem.Enabled = undoRedoSystem.CanRedo();
            SaveNeeded = true;
        }

        private OpenFileDialog imageFileDialog;

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddType();
        }

        private void AddType()
        {
            if (imageFileDialog == null)
            {
                imageFileDialog = new OpenFileDialog();
                imageFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.tga;*.tif;*.gif|All Files|*.*";
                imageFileDialog.Multiselect = true;
            }

            if (imageFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] imageFiles = imageFileDialog.FileNames;

                foreach (string imageFile in imageFiles)
                {
                    Project.AddType(ImageObject2DType.FromFile(imageFile));
                }

                BuildTypeList();
                SelectType(Project.Types.Count - 1);

                AddUndoRedoState();
            }
        }

        private void BuildTypeList()
        {
            objectPalette.GameObjects = Project.Types;
        }

        private void BuildLevelList()
        {
            int selectedIndex = levelListBox.SelectedIndex;

            levelListBox.Items.Clear();

            if (Project == null) { return; }

            foreach (Level level in Project.Levels)
            {
                levelListBox.Items.Add(level);
            }

            levelListBox.Items.Add("Click to add a level...");

            SelectLevel(selectedIndex);

            levelListBox.Refresh();
        }

        private void SelectLevel(int selectedIndex)
        {
            if (selectedIndex == -1) { return; }

            if (selectedIndex > levelListBox.Items.Count - 1) { selectedIndex = levelListBox.Items.Count - 1; }
            
            if (selectedIndex != -1) { levelListBox.SelectedIndex = selectedIndex; }
        }

        private void levelListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index < 0 || e.Index >= levelListBox.Items.Count) { return; }

                e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);

                if (e.Index < project.Levels.Count)
                {
                    Level level = project.Levels[e.Index];

                    int itemHeight = levelListBox.ItemHeight;
                    SizeF size = e.Graphics.MeasureString(level.Name, levelListBox.Font);
                    e.Graphics.DrawString(level.Name, levelListBox.Font, new SolidBrush(Color.Black),
                        new PointF(5, e.Bounds.Y + itemHeight / 2 - size.Height / 2));

                    if (levelListBox.SelectedItems.Contains(level))
                    {
                        Rectangle outlineRectangle = e.Bounds;
                        outlineRectangle.Inflate(new Size(-3, -3));
                        e.Graphics.DrawRectangle(new Pen(Color.FromArgb(17, 110, 0), 3), outlineRectangle);
                    }
                }
                else
                {
                    Rectangle rectangleBounds = new Rectangle(e.Bounds.X + 3, e.Bounds.Y + 3, e.Bounds.Width - 6, e.Bounds.Height - 6);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(225, 225, 225)), rectangleBounds);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(150, 150, 150))), rectangleBounds);
                    Font font = new Font("Segoe UI", 12, FontStyle.Italic);
                    string text = (string)levelListBox.Items[e.Index];
                    SizeF size = e.Graphics.MeasureString(text, font);
                    int x = (int)(e.Bounds.X + (e.Bounds.Width - size.Width) / 2);
                    int y = (int)(e.Bounds.Y + (e.Bounds.Height - size.Height) / 2);
                    e.Graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(150, 150, 150)), new Point(x, y));
                }
            }
            catch (Exception)
            {
            }
        }

        private void typeListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            
        }

        private void typeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            AddLevel();
        }

        private void AddLevel()
        {
            Project.CreateNewLevel();
            this.BuildLevelList();
            AddUndoRedoState();
        }



        private void levelRenderer_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Level level = (Level)levelListBox.SelectedItem;
                if (level == null) { return; }

                int cellWidth = level.Settings.CellWidth;
                int cellHeight = level.Settings.CellHeight;

                e.Graphics.FillRectangle(new SolidBrush(level.Settings.BackColor),
                    new Rectangle(0, 0, level.Settings.CellWidth * level.Settings.Columns, level.Settings.CellHeight * level.Settings.Rows));

                if (this.drawGrid) { DrawGrid(e, level); }
                
                for (int row = 0; row < level.Settings.Rows; row++)
                {
                    for (int column = 0; column < level.Settings.Columns; column++)
                    {
                        ImageObject2D type = (ImageObject2D)level.Get(column, row);
                        if (type != null)
                        {
                            e.Graphics.DrawImage(((ImageObject2DType)type.ParentType).Image, new Rectangle(column * cellWidth, row * cellHeight, cellWidth, cellHeight));
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private static void DrawGrid(PaintEventArgs e, Level level)
        {
            Pen gridPen = new Pen(new SolidBrush(Color.FromArgb(50, 50, 50)), 1);
            for (int row = 0; row < level.Settings.Rows + 1; row++)
            {
                e.Graphics.DrawLine(
                    gridPen,
                    new Point(0, row * level.Settings.CellHeight),
                    new Point(level.Settings.Columns * level.Settings.CellWidth, row * level.Settings.CellHeight));
            }

            for (int column = 0; column < level.Settings.Columns + 1; column++)
            {
                e.Graphics.DrawLine(
                    gridPen,
                    new Point(column * level.Settings.CellWidth, 0),
                    new Point(column * level.Settings.CellWidth, level.Settings.Rows * level.Settings.CellHeight));
            }
        }

        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            //Taxes: Remote Desktop Connection and painting 
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx 
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        } 

        private void levelListBox_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if(levelListBox.SelectedItem is Level)
            {
                ResizeLevelRenderer();
                levelRenderer.Refresh();
            }

            levelListBox.Refresh();
        }

        private void ResizeLevelRenderer()
        {
            if (levelListBox.SelectedItem is string) { return; }

            Level selectedLevel = (Level)levelListBox.SelectedItem;
            if (selectedLevel == null)
            {
                levelRenderer.Width = 0;
                levelRenderer.Height = 0;
            }
            else
            {
                levelRenderer.Width = selectedLevel.Settings.CellWidth * selectedLevel.Settings.Columns;
                levelRenderer.Height = selectedLevel.Settings.CellHeight * selectedLevel.Settings.Rows;
            }
        }

        private void pencilTool_Click(object sender, EventArgs e)
        {
            ActiveTool = Tool.Pencil;
        }

        private void paintBucketTool_Click(object sender, EventArgs e)
        {
            ActiveTool = Tool.Bucket;
        }

        private void clearLevelButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear this entire level?", "Clear Level", MessageBoxButtons.YesNoCancel) == System.Windows.Forms.DialogResult.Yes)
            {
                if (levelListBox.SelectedItem == null) { return; }

                Level activeLevel = (Level)levelListBox.SelectedItem;

                activeLevel.Clear();
                levelRenderer.Refresh();
                AddUndoRedoState();
            }
        }

        private Tool activeTool = Tool.Pencil;

        public Tool ActiveTool
        {
            get
            {
                return activeTool;
            }
            set
            {
                activeTool = value;
                if (activeTool == Tool.Pencil) { levelRenderer.Cursor = SuperAwesomeCursor.CreatePencilCursor(); }
                else if (activeTool == Tool.Bucket) { levelRenderer.Cursor = SuperAwesomeCursor.CreatePaintCanCursor(); }
                else if (activeTool == Tool.Eraser) { levelRenderer.Cursor = SuperAwesomeCursor.CreateEraserCursor(); }
            }
        }

        bool mouseStartedInRenderer = false;

        private void levelRenderer_MouseDown(object sender, MouseEventArgs e)
        {
            if (levelListBox.SelectedItem == null) { return; }

            mouseStartedInRenderer = true;

            Level activeLevel = (Level)levelListBox.SelectedItem;
            ImageObject2DType activeType = (ImageObject2DType)objectPalette.SelectedItem;

            if (ActiveTool == Tool.Pencil)
            {
                Point position = new Point(e.X, e.Y);
                int row = position.Y / activeLevel.Settings.CellHeight;
                int column = position.X / activeLevel.Settings.CellWidth;

                if (row < 0) { return; }
                if (row >= activeLevel.Settings.Rows) { return; }

                if (column < 0) { return; }
                if (column >= activeLevel.Settings.Columns) { return; }

                ImageObject2D type = (ImageObject2D)activeLevel.Get(column, row);
                if (type == null || type.ParentType != activeType)
                {
                    if (type != null && !project.Types.Contains(activeType)) { throw new Exception("type not in project"); }
                    activeLevel.Put(type == null ? null : activeType.GenerateNew(), column, row);
                    levelRenderer.Refresh();
                    somethingChanged = true;
                }
            }
            if (ActiveTool == Tool.Eraser)
            {
                Point position = new Point(e.X, e.Y);
                int row = position.Y / activeLevel.Settings.CellHeight;
                int column = position.X / activeLevel.Settings.CellWidth;

                if (row < 0) { return; }
                if (row >= activeLevel.Settings.Rows) { return; }

                if (column < 0) { return; }
                if (column >= activeLevel.Settings.Columns) { return; }

                ImageObject2D type = (ImageObject2D)activeLevel.Get(column, row);
                if (type != null)
                {
                    activeLevel.Erase(column, row);
                    levelRenderer.Refresh();
                    somethingChanged = true;
                }
            }
            if (ActiveTool == Tool.Bucket)
            {
                Point position = new Point(e.X, e.Y);
                int row = position.Y / activeLevel.Settings.CellHeight;
                int column = position.X / activeLevel.Settings.CellWidth;

                if (row < 0) { return; }
                if (row >= activeLevel.Settings.Rows) { return; }

                if (column < 0) { return; }
                if (column >= activeLevel.Settings.Columns) { return; }

                if (activeLevel.Get(column, row) == null || activeLevel.Get(column, row).ParentType != activeType)
                {
                    Fill(activeLevel, row, column, activeLevel.Get(column, row), activeType.GenerateNew());
                }

                levelRenderer.Refresh();

                somethingChanged = true;
            }
        }

        private void Fill(Level activeLevel, int row, int column, GameObject original, GameObject changeToType)
        {
            if (row < 0) { return; }
            if (row >= activeLevel.Settings.Rows) { return; }

            if (column < 0) { return; }
            if (column >= activeLevel.Settings.Columns) { return; }

            if (activeLevel.Get(column, row) != original) { return; }

            activeLevel.Put(changeToType, column, row);

            Fill(activeLevel, row - 1, column, original, changeToType);
            Fill(activeLevel, row + 1, column, original, changeToType);
            Fill(activeLevel, row, column - 1, original, changeToType);
            Fill(activeLevel, row, column + 1, original, changeToType);
        }

        private void levelRenderer_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseStartedInRenderer) { return; }

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (levelListBox.SelectedItem == null) { return; }

                Level activeLevel = (Level)levelListBox.SelectedItem;
                ImageObject2DType activeType = (ImageObject2DType)objectPalette.SelectedItem;

                if (ActiveTool == Tool.Pencil)
                {
                    Point position = new Point(e.X, e.Y);
                    int row = position.Y / activeLevel.Settings.CellHeight;
                    int column = position.X / activeLevel.Settings.CellWidth;

                    if (row < 0) { return; }
                    if (row >= activeLevel.Settings.Rows) { return; }

                    if (column < 0) { return; }
                    if (column >= activeLevel.Settings.Columns) { return; }

                    ImageObject2D type = (ImageObject2D)activeLevel.Get(column, row);
                
                    if (type == null || type.ParentType != activeType)
                    {
                        activeLevel.Put(activeType == null ? null : activeType.GenerateNew(), column, row);
                        levelRenderer.Refresh();
                        somethingChanged = true;
                    }
                }
                if (ActiveTool == Tool.Eraser)
                {
                    Point position = new Point(e.X, e.Y);
                    int row = position.Y / activeLevel.Settings.CellHeight;
                    int column = position.X / activeLevel.Settings.CellWidth;

                    if (row < 0) { return; }
                    if (row >= activeLevel.Settings.Rows) { return; }

                    if (column < 0) { return; }
                    if (column >= activeLevel.Settings.Columns) { return; }

                    GameObject gameObject = activeLevel.Get(column, row);
                    if (gameObject != null)
                    {
                        activeLevel.Erase(column, row);
                        levelRenderer.Refresh();
                        somethingChanged = true;
                    }
                }
            }
        }

        private void eraserButton_Click(object sender, EventArgs e)
        {
            ActiveTool = Tool.Eraser;
        }

        private bool somethingChanged = false;

        private void levelRenderer_MouseUp(object sender, MouseEventArgs e)
        {
            mouseStartedInRenderer = false;

            if (somethingChanged)
            {
                AddUndoRedoState();
            }

            somethingChanged = false;
        }

        private void AddUndoRedoState()
        {
            undoRedoSystem.PushState(new ProgramState() { Project = Project.Copy(), ActiveLevelIndex = levelListBox.SelectedIndex, ActiveTypeIndex = objectPalette.SelectedIndex });
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoRedoSystem.CanUndo())
            {
                int previouslySelectedType = objectPalette.SelectedIndex;
                ProgramState programState = undoRedoSystem.Undo();
                this.Project = programState.Project.Copy();
                objectPalette.GameObjects = Project.Types;
                SelectType(previouslySelectedType);
                Refresh();
            }
        }

        private void SelectType(int typeIndex)
        {
            objectPalette.SelectedIndex = typeIndex;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoRedoSystem.CanRedo())
            {
                int previouslySelectedType = objectPalette.SelectedIndex;
                ProgramState programState = undoRedoSystem.Redo();
                this.Project = programState.Project.Copy();
                levelListBox.SelectedIndex = programState.ActiveLevelIndex;
                objectPalette.GameObjects = Project.Types;
                SelectType(previouslySelectedType);
                Refresh();
            }
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateChecker.CheckForUpdates();
        }

        private void bugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using (SubmitBugDialog submitBugDialog = new SubmitBugDialog())
            //{
            //    if (submitBugDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        try
            //        {
            //            WebServicesCommunicator.SubmitBugReport(submitBugDialog.BugReport);
            //            MessageBox.Show("Your bug was submitted successfully!", "Realm Factory", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show("An error occurred while submitting your bug.  Please try again later.", "Realm Factory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
        }

        private void featureRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using (SubmitFeatureRequestDialog submitFeatureDialog = new SubmitFeatureRequestDialog())
            //{
            //    if (submitFeatureDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        try
            //        {
            //            WebServicesCommunicator.SubmitFeatureRequest(submitFeatureDialog.FeatureRequestReport);
            //            MessageBox.Show("Your feature request was submitted successfully!", "Realm Factory", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show("An error occurred while submitting your feature request.  Please try again later.", "Realm Factory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
        }

        private void feedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using (SubmitFeedbackDialog feedbackDialog = new SubmitFeedbackDialog())
            //{
            //    if(feedbackDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        try
            //        {
            //            WebServicesCommunicator.SubmitFeedback(feedbackDialog.FeedbackReport);
            //            MessageBox.Show("Your feedback was submitted successfully!", "Realm Factory", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show("An error occurred while submitting your feedback.  Please try again later.", "Realm Factory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
        }

        private void aboutRealmEngineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutDialog aboutDialog = new AboutDialog())
            {
                aboutDialog.ShowDialog();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string currentProjectPath = null;

        private void Save()
        {
            if (currentProjectPath == null)
            {
                SaveAs();
            }
            else
            {
                SaveToPath(currentProjectPath);
            }
        }

        private void SaveToPath(string path)
        {
            fileWriterManager.Write(path, this.Project);
            SaveNeeded = false;
        }

        private void SaveAs()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = fileWriterManager.BuildFilter();
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    currentProjectPath = saveFileDialog.FileName;

                    SaveToPath(currentProjectPath);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SaveNeeded)
            {
                DialogResult result = MessageBox.Show("Do you want to save before exiting?",
                    "Realm Factory", MessageBoxButtons.YesNoCancel);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Save();
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private bool ProjectHasBeenEdited()
        {
            return SaveNeeded;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformOpenFile();
        }

        private void PerformOpenFile(bool checkSaveNeeded = true)
        {
            bool abort = false;

            if (checkSaveNeeded && ProjectHasBeenEdited())
            {
                abort = MaybeSave();
            }

            if (!abort)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = fileReaderManager.BuildFilter();

                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        OpenFile(openFileDialog.FileName);
                    }
                }
            }
        }

        private void OpenFile(string path)
        {
            this.Project = fileReaderManager.Read(path);
            undoRedoSystem.Clear();
            this.levelListBox.SelectedIndex = 0;
            AddUndoRedoState();
            currentProjectPath = path;
            SaveNeeded = false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool abort = false;

            if (SaveNeeded)
            {
                abort = MaybeSave();
            }

            if (!abort)
            {
                NewProject();
            }
        }

        /// <summary>
        /// Starts a new project from scratch, killing any old information.
        /// </summary>
        private void NewProject()
        {
            Project project = new Project();

            using (Wizard wizard = new Wizard())
            {
                wizard.Project = project;
                if (wizard.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    project.CreateNewLevel();
                    Project = project;
                }
                else
                {
                    project = Project.CreateDefaultProject();
                }
            }

            undoRedoSystem.Clear();
            this.levelListBox.SelectedIndex = 0;
            AddUndoRedoState();
            SaveNeeded = false;

            currentProjectPath = null;
        }

        /// <summary>
        /// Returns whether to abort the operation.
        /// </summary>
        /// <returns></returns>
        private bool MaybeSave()
        {
            DialogResult result = MessageBox.Show("Do you want to save the current project before closing it?",
                "Realm Factory", MessageBoxButtons.YesNoCancel);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Save();
            }
            else if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return true;
            }

            return false;
        }

        private bool drawGrid = true;

        private void drawGridButton_Click(object sender, EventArgs e)
        {
            this.drawGrid = drawGridButton.Checked;
            this.Refresh();
        }

        private void addTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddType();
        }

        private void addLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLevel();
        }

        private void ShowTypeProperties()
        {
            GameObjectType selectedType = (GameObjectType)objectPalette.SelectedItem;

            ShowTypeProperties(selectedType);
        }

        private void ShowTypeProperties(GameObjectType gameObjectType)
        {
            if (gameObjectType == null) { return; }

            using (TypeSettings typeSettings = new TypeSettings())
            {
                typeSettings.Type = gameObjectType;
                typeSettings.ShowDialog();
                Refresh();
            }
        }

        private void ShowLevelProperties()
        {
            Level selectedLevel = (Level)levelListBox.SelectedItem;

            if (selectedLevel == null) { return; }

            using (LevelSettings levelSettings = new LevelSettings())
            {
                levelSettings.Level = selectedLevel;
                levelSettings.ShowDialog();
                ResizeLevelRenderer();
                AddUndoRedoState();
                Refresh();
            }
        }

        private void defaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowProjectProperties();
        }

        private void ShowProjectProperties()
        {
            using (ProjectSettings projectSettings = new ProjectSettings())
            {
                projectSettings.Project = this.Project;
                projectSettings.ShowDialog();
                ResizeLevelRenderer();
                AddUndoRedoState();
                Refresh();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddType();
        }

        private void addLevelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddLevel();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteLevel();
        }

        private void DeleteLevel()
        {
            Level selectedLevel = (Level)levelListBox.SelectedItem;

            if (selectedLevel == null) { return; }

            Project.Levels.Remove(selectedLevel);

            this.BuildLevelList();
            AddUndoRedoState();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DeleteType();
        }

        private void DeleteType()
        {
            GameObjectType selectedType = (GameObjectType)objectPalette.SelectedItem;

            if (selectedType == null) { return; }
            bool inUse = false;

            foreach (Level level in project.Levels)
            {
                if (level.UsesType(selectedType))
                {
                    inUse = true;
                    break;
                }
            }

            bool acceptedDelete = false;

            if (inUse)
            {
                DialogResult result = MessageBox.Show("The selected object type is in use.  Deleting it will remove it from all places it is used throughout the project.\n\nProceed anyway?", "Realm Factory", MessageBoxButtons.YesNoCancel);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    acceptedDelete = true;
                }
            }

            if (!inUse || acceptedDelete)
            {
                foreach (Level level in project.Levels)
                {
                    level.RemoveType(selectedType);
                }

                project.Types.Remove(selectedType);
                this.BuildTypeList();
                AddUndoRedoState();
                Refresh();
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLevelProperties();
        }

        private void typePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTypeProperties();
        }

        private void typeListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowTypeProperties();
        }


        private void typeListBox_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            SplashScreen.HideSplashScreen();
            ShowEula();
            ShowGettingStartedDialog();
        }

        private void ShowGettingStartedDialog()
        {
            using (NewOrOpenDialog gettingStartedDialog = new NewOrOpenDialog())
            {
                DialogResult result = gettingStartedDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    NewProject();
                }
                else if (result == System.Windows.Forms.DialogResult.Ignore) //arbitrary value, but that's what it is...
                {
                    ShowTutorial();
                    NewProject();
                }
                else if (result == System.Windows.Forms.DialogResult.Retry) // another arbitrary value... for open project.
                {
                    Project = new Project();
                    PerformOpenFile(false);
                }
                else
                {
                    NewProject(); // For the moment, I don't want to leave the program in a state where no project is open.  It may cause all sorts of bugs that I don't want to address right away.  But that's what this should do, ideally.
                }
            }
        }

        private void ShowEula()
        {
            if (Starbound.RealmFactory.Properties.Settings.Default.AcceptedEulaVersion == "")
            {
                using (EulaDialog eulaDialog = new EulaDialog())
                {
                    if (eulaDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Starbound.RealmFactory.Properties.Settings.Default.AcceptedEulaVersion = Constants.VersionString;
                        Starbound.RealmFactory.Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ShowProjectProperties();
        }

        private void tutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTutorial();
        }

        private static void ShowTutorial()
        {
            System.Diagnostics.Process.Start("http://StarboundSoftware.com/software/realm-factory/tutorial");
        }

        private void onlineForumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://rbwhitaker.wikidot.com/forum/c-503425/general-questions-about-realm-factory");
        }

        private void levelListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (levelListBox.SelectedItem is string)
            {
            }
            else
            {
                ShowLevelProperties();
            }
        }


        private void levelListBox_MouseClick(object sender, MouseEventArgs e)
        {
            int index = levelListBox.IndexFromPoint(e.Location);
            if (index == levelListBox.Items.Count - 1)
            {
                AddLevel();
            }

            this.Refresh();
        }

        private void objectPalette_NewObjectClicked(object sender, EventArgs e)
        {
            AddType();
        }

        private void objectPalette_ItemClicked(object sender, EventArgs e)
        {

        }

        private void objectPalette_ItemDoubleClicked(object sender, EventArgs e)
        {
            ShowTypeProperties();
        }

        private void rowLayoutButton_Click(object sender, EventArgs e)
        {
            objectPalette.DisplayMode = DisplayMode.ImageAndName;
        }

        private void grid3LayoutButton_Click(object sender, EventArgs e)
        {
            objectPalette.DisplayMode = DisplayMode.ImageOnly;
            objectPalette.GridColumns = 3;
        }

        private void grid4LayoutButton_Click(object sender, EventArgs e)
        {
            objectPalette.DisplayMode = DisplayMode.ImageOnly;
            objectPalette.GridColumns = 4;
        }

        private void grid6LayoutButton_Click(object sender, EventArgs e)
        {
            objectPalette.DisplayMode = DisplayMode.ImageOnly;
            objectPalette.GridColumns = 6;
        }
    }
}

