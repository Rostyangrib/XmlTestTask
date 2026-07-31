// Разметка главной формы: кнопка загрузки, список файлов и область просмотра карточки.

namespace XmlToDb
{
    partial class MainForm
    {
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ListBox lstDocuments;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.SplitContainer splitContainer;

        private void InitializeComponent()
        {
            this.btnLoad = new System.Windows.Forms.Button();
            this.lstDocuments = new System.Windows.Forms.ListBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.SuspendLayout();
            //
            // btnLoad
            //
            this.btnLoad.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLoad.Height = 34;
            this.btnLoad.Text = "Загрузить xml-файл...";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            //
            // lstDocuments
            //
            this.lstDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDocuments.IntegralHeight = false;
            this.lstDocuments.SelectedIndexChanged += new System.EventHandler(this.lstDocuments_SelectedIndexChanged);
            //
            // webBrowser
            //
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            //
            // splitContainer
            //
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Panel1.Controls.Add(this.lstDocuments);
            this.splitContainer.Panel2.Controls.Add(this.webBrowser);
            this.splitContainer.Size = new System.Drawing.Size(940, 526);
            this.splitContainer.SplitterDistance = 320;
            //
            // MainForm
            //
            this.ClientSize = new System.Drawing.Size(940, 560);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.btnLoad);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
