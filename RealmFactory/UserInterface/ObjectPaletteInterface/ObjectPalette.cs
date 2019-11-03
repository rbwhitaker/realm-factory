using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Starbound.RealmFactory.DataModel;
using Starbound.RealmFactory.UserInterface.ObjectPaletteInterface;

namespace Starbound.RealmFactory.UserInterface
{
    public partial class ObjectPalette : UserControl
    {
        private DisplayMode displayMode;
        public DisplayMode DisplayMode
        {
            get
            {
                return displayMode;
            }
            set
            {
                displayMode = value;

                ObjectPaletteLayoutEngine layoutEngine = (layoutPanel.LayoutEngine as ObjectPaletteLayoutEngine);
                layoutEngine.DisplayMode = value;
                
                if (displayMode == UserInterface.DisplayMode.ImageAndName)
                {                    
                    layoutEngine.RowHeight = 40;
                }
                layoutPanel.PerformLayout();
                Refresh();
            }
        }


        private int gridColumns = 4;

        public int GridColumns
        {
            get
            {
                return gridColumns;
            }
            set
            {
                gridColumns = value;
                ObjectPaletteLayoutEngine layoutEngine = (layoutPanel.LayoutEngine as ObjectPaletteLayoutEngine);
                layoutEngine.Columns = gridColumns;
                if (gridColumns == 3) { layoutEngine.RowHeight = 68; }
                if (gridColumns == 4) { layoutEngine.RowHeight = 50; }
                if (gridColumns == 6) { layoutEngine.RowHeight = 32; }
                layoutPanel.PerformLayout();
                Refresh();
            }
        }

        private SmartList<GameObjectType> gameObjects;

        public SmartList<GameObjectType> GameObjects
        {
            get
            {
                return gameObjects;
            }
            set
            {
                if (gameObjects != null) { gameObjects.ListModified -= GameObjects_ListModified; }
                gameObjects = value;
                if (gameObjects != null) { gameObjects.ListModified += GameObjects_ListModified; }
                GameObjects_ListModified(this, EventArgs.Empty);
            }
        }

        public GameObjectType SelectedItem
        {
            get
            {
                if (selectedPaletteItem == null) { return null; }
                else { return selectedPaletteItem.ImageObject; }
            }
        }

        public int SelectedIndex
        {
            get
            {
                if (selectedPaletteItem == null) { return -1; }
                return (GameObjects.IndexOf(selectedPaletteItem.ImageObject));
            }
            set
            {
                if (selectedPaletteItem != null) { selectedPaletteItem.Selected = false; }
                if (value == -1) { selectedPaletteItem = null; }
                else if (GameObjects == null || value >= GameObjects.Count) { selectedPaletteItem = null; }
                else { selectedPaletteItem = (ObjectPaletteItem)layoutPanel.Controls[value]; }
                if (selectedPaletteItem != null) { selectedPaletteItem.Selected = true; }
            }
        }

        public ObjectPalette()
        {
            InitializeComponent();
            layoutPanel = new ObjectPaletteLayoutPanel();
            DoubleBuffered = true;
            DisplayMode = UserInterface.DisplayMode.ImageOnly;
            RebuildGui();
        }

        public void GameObjects_ListModified(object sender, EventArgs e)
        {
            RebuildGui();
        }

        private void RebuildGui()
        {
            BuildList();

            this.Refresh();
        }

        private ObjectPaletteLayoutPanel layoutPanel;

        private void BuildList()
        {
            SuspendLayout();

            layoutPanel.Controls.Clear();

            if (GameObjects != null) 
            {
                for (int index = 0; index < GameObjects.Count; index++)
                {
                    GameObjectType imageObject = GameObjects[index];
                    ObjectPaletteItem paletteItem = new ObjectPaletteItem() { ImageObject = imageObject, DisplayMode = this.DisplayMode };
                    paletteItem.MouseClick += paletteItem_MouseClick;
                    paletteItem.MouseDoubleClick += paletteItem_MouseDoubleClick;
                    layoutPanel.Controls.Add(paletteItem);
                }
            }

            newObjectPaletteItem = ObjectPaletteItem.CreateNewObjectPaletteItem();
            layoutPanel.Controls.Add(newObjectPaletteItem);
            newObjectPaletteItem.MouseClick += paletteItem_MouseClick;

            
            Controls.Clear();
            Panel wrapperPanel = new Panel();
            wrapperPanel.AutoScroll = true;
            wrapperPanel.Dock = DockStyle.Fill;
            layoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            layoutPanel.BackColor = Color.FromArgb(235, 235, 235);
            this.BackColor = Color.FromArgb(235, 235, 235);
            wrapperPanel.Controls.Add(layoutPanel);
            Controls.Add(wrapperPanel);
            ResumeLayout();
        }

        private ObjectPaletteItem newObjectPaletteItem;

        private void paletteItem_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender == newObjectPaletteItem)
            {
                OnNewObjectClicked();
            }
            else
            {
                ObjectPaletteItem item = sender as ObjectPaletteItem;
                if (selectedPaletteItem != null) { selectedPaletteItem.Selected = false; }
                selectedPaletteItem = item;
                if (selectedPaletteItem != null) { selectedPaletteItem.Selected = true; }
            }
        }

        private void paletteItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ObjectPaletteItem item = sender as ObjectPaletteItem;
            OnItemDoubleClicked(item.ImageObject);
        }

        public event EventHandler ItemClicked;

        public event EventHandler ItemDoubleClicked;

        public void OnItemClicked(GameObjectType gameObject)
        {
            if (ItemClicked != null)
            { 
                ItemClicked(gameObject, EventArgs.Empty);
            }
        }

        public void OnItemDoubleClicked(GameObjectType gameObject)
        {
            if(ItemDoubleClicked != null)
            {
                ItemDoubleClicked(gameObject, EventArgs.Empty);
            }
        }

        private ObjectPaletteItem selectedPaletteItem;

        public event EventHandler NewObjectClicked;

        public void OnNewObjectClicked()
        {
            if (NewObjectClicked != null)
            {
                NewObjectClicked(this, EventArgs.Empty);
            }
        }

        private void ObjectPalette_MouseMove(object sender, MouseEventArgs e)
        {
            int index = IndexAtPosition(this.PointToClient(e.Location));
        }

        private int IndexAtPosition(Point location)
        {
            return -1;
        }

        private void ObjectPalette_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void ObjectPalette_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

            }
        }
    }
}
