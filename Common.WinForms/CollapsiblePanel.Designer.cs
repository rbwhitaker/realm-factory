namespace Starbound.Common.WinForms
{
    partial class CollapsiblePanel
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
            this.workingArea = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // workingArea
            // 
            this.workingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workingArea.Location = new System.Drawing.Point(0, 30);
            this.workingArea.Margin = new System.Windows.Forms.Padding(3, 33, 3, 3);
            this.workingArea.Name = "workingArea";
            this.workingArea.Size = new System.Drawing.Size(247, 216);
            this.workingArea.TabIndex = 0;
            // 
            // CollapsiblePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.workingArea);
            this.Name = "CollapsiblePanel";
            this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.Size = new System.Drawing.Size(247, 246);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CollapsiblePanel_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel workingArea;

    }
}
