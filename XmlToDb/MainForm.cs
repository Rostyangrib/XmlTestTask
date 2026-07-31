// Главная форма: загрузка xml-файлов в базу, список сохранённых файлов и показ карточки.

using System;
using System.IO;
using System.Windows.Forms;
using XmlToDb.Core;

namespace XmlToDb
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshDocuments();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { Filter = "XML-файлы (*.xml)|*.xml" })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    var name = Path.GetFileName(dialog.FileName);
                    var content = File.ReadAllBytes(dialog.FileName);

                    CardXmlParser.Parse(content, name); // разбор до сохранения, чтобы не класть в базу лишнее
                    Database.AddDocument(name, content);
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }

                RefreshDocuments();
            }
        }

        private void lstDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var document = lstDocuments.SelectedItem as DocumentInfo;
            if (document == null)
                return;

            try
            {
                var card = CardXmlParser.Parse(Database.GetContent(document.Id), document.FileName);
                webBrowser.Navigate(new Uri(CardPage.Save(card, document.Id)));
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void RefreshDocuments()
        {
            lstDocuments.Items.Clear();
            foreach (var document in Database.GetDocuments())
                lstDocuments.Items.Add(document);

            if (lstDocuments.Items.Count > 0)
                lstDocuments.SelectedIndex = 0;
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "XmlToDb", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
