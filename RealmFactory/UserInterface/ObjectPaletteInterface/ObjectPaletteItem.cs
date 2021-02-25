using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Starbound.RealmFactory.DataModel;
using RealmFactory.Properties;

namespace Starbound.RealmFactory.UserInterface
{
    /// <summary>
    /// The display mode for a palette item inside of an ObjectPaletteItem control.
    /// </summary>
    public enum DisplayMode { ImageOnly, ImageAndName };

    public partial class ObjectPaletteItem : UserControl
    {
        public static ObjectPaletteItem CreateNewObjectPaletteItem()
        {
             return new ObjectPaletteItem() { ImageObject = new ImageObject2DType() { Name = "Add Object", Image = Resources.AddItemIcon }, IsUtilityItem = true };
        }

        /// <summary>
        /// Indicates whether the item is a utility item or not.
        /// Utility items will be displayed differently than regular items.
        /// Utility items include things like the palette item that allows you
        /// to add new objects to the editor.
        /// </summary>
        public bool IsUtilityItem { get; set; }

        /// <summary>
        /// Stores the object type that is being represented by this palette item.
        /// </summary>
        private GameObjectType imageObject;

        /// <summary>
        /// Gets or sets the object type being represented by this palette item.
        /// </summary>
        public GameObjectType ImageObject
        {
            get
            {
                return imageObject;
            }
            set
            {
                imageObject = value;
            }
        }

        public DisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Indicates whether the object is hovered or not.
        /// </summary>
        private bool hovered;

        /// <summary>
        /// Gets or sets whether the object is being hovered over.
        /// </summary>
        public bool Hovered
        {
            get
            {
                return hovered;
            }
            set
            {
                hovered = value;
                Refresh();
            }
        }

        /// <summary>
        /// Indicates whether the object is selected or not.
        /// </summary>
        private bool selected;

        /// <summary>
        /// Gets or sets whether the object is selected.
        /// </summary>
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                Refresh();
            }
        }

        public ObjectPaletteItem()
        {
            InitializeComponent();
            DoubleBuffered = true;
            IsUtilityItem = false;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ImageObject2DType imageObject = (ImageObject2DType)this.ImageObject;
            try
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(235, 235, 235)), new Rectangle(0, 0, Width, Height));
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, 180, 180)), new Rectangle(1, 1, Bounds.Width - 1, Bounds.Height - 1));
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), new Rectangle(0, 0, Bounds.Width - 1, Bounds.Height - 1));
                if (IsUtilityItem)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 200, 200)), new Rectangle(0, 0, Bounds.Width - 1, Bounds.Height - 1));
                }

                int itemHeight = this.Height - 3;

                double scale = Math.Min((double)itemHeight / imageObject.Image.Width, (double)itemHeight / imageObject.Image.Height);
                if (scale > 1) { scale = 1; }

                Rectangle imageBounds = new Rectangle(
                    (int)(1 + itemHeight / 2 - imageObject.Image.Width / 2 * scale),
                    (int)(1 + itemHeight / 2 - imageObject.Image.Height / 2 * scale),
                    (int)(imageObject.Image.Width * scale),
                    (int)(imageObject.Image.Height * scale));

                e.Graphics.DrawImage(imageObject.Image, imageBounds);

                SizeF size = e.Graphics.MeasureString(imageObject.Name, Font);

                if (Selected || Hovered)
                {
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Green), 2), new Rectangle(1, 1, Bounds.Width - 3, Bounds.Height - 3));
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.White), 2), new Rectangle(3, 3, Bounds.Width - 7, Bounds.Height - 7));
                }

                if (DisplayMode == UserInterface.DisplayMode.ImageAndName)
                {
                    Rectangle rectangleBounds = new Rectangle(Bounds.X + 3, Bounds.Y + 3, Bounds.Width - 6, Bounds.Height - 6);
                    Font font = new Font("Segoe UI", 10, FontStyle.Regular);
                    string text = (string)imageObject.Name;
                    size = e.Graphics.MeasureString(text, font);

                    int x = imageBounds.Right + 2;
                    int y = (int)((Bounds.Height - size.Height) / 2);
                    bool addEllipsis = false;

                    while (size.Width + x + 2 > Bounds.Width)
                    {
                        addEllipsis = true;
                        text = text.Substring(0, text.Length - 1);
                        size = e.Graphics.MeasureString(text + "...", font);
                    }

                    if (addEllipsis) { text += "..."; }
                    
                    e.Graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(150, 150, 150)), new Point(x, y));
                }
            }
            catch (Exception)
            {
            }
        }

        private void ObjectPaletteItem_MouseEnter(object sender, EventArgs e)
        {
            Hovered = true;
        }

        private void ObjectPaletteItem_MouseLeave(object sender, EventArgs e)
        {
            Hovered = false;
        }

        private void ObjectPaletteItem_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void ObjectPaletteItem_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //    this.Ob
            //}
        }
    }
}
