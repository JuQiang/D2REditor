namespace D2REditor.Controls
{
    partial class ListBoxEx
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
            // ListBoxEx
            // 
            this.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBoxEx_DrawItem);
            this.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.ListBoxEx_MeasureItem);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
