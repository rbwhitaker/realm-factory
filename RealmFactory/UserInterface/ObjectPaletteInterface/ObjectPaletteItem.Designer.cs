namespace Starbound.RealmFactory.UserInterface
{
    partial class ObjectPaletteItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ObjectPaletteItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ObjectPaletteItem";
            this.Size = new System.Drawing.Size(40, 40);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ObjectPaletteItem_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ObjectPaletteItem_MouseClick);
            this.MouseEnter += new System.EventHandler(this.ObjectPaletteItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ObjectPaletteItem_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
