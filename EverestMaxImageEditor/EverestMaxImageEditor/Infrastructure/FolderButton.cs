using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EverestMaxImageEditor.Infrastructure
{
    public class FolderButton : Button
    {
        public string folderPath { get; private set; }
        public TextBox textBox { get; private set; }
        public bool isChanged;

        public FolderButton(Form form)
        {
            this.Text = "File Path";
            this.textBox = new TextBox();
            this.textBox.Height = this.Height;
            this.textBox.Width = 350;
            this.textBox.Location = new System.Drawing.Point(this.Location.X+this.Width, this.Location.Y);
            form.Controls.Add(textBox);
            this.isChanged = false;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.ShowNewFolderButton = true;
                folderDialog.ShowDialog();
                folderPath = folderDialog.SelectedPath;
                this.textBox.Text = folderPath;
                this.isChanged = true;
            }
        }
    }
}
