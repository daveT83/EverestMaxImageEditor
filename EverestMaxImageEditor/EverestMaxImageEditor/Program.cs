using EverestMaxImageEditor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EverestMaxImageEditor
{
    static class Program
    {
        public static List<RoundedButton> buttons;
        public static Form main;
        public static ExportButton exportButton;
        public static FolderButton filePathButton;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread exportUpdater = new Thread(new ThreadStart(UpdateExport));
            main = new Form1();
            main.FormBorderStyle = FormBorderStyle.FixedSingle;
            Image background = Image.FromFile(GetKeyboardImage());

            main.Text = "Everest Max Image Editor";
            main.Icon = new Icon(GetIconImage());
            main.Height = background.Height;
            main.Width = background.Width;
            main.BackgroundImage = background;
            buttons = new List<RoundedButton>();
            main.Controls.Add(GetMediaScreenButton(main));

            filePathButton = GetFilePathButton(main);
            main.Controls.Add(filePathButton);
            GetExportButton();
            main.Controls.Add(exportButton);

            for (int i = 0; i < 4; i++)
            {
                main.Controls.Add(GetNumPadButton(i, main));
            }

            exportUpdater.Start();
            Application.Run(main);

            System.Environment.Exit(0);
        }

        /// <summary>
        /// updates the export buttons list of images
        /// </summary>
        public static void UpdateExport()
        {
            while (true)
            {
                foreach (RoundedButton button in buttons)
                {
                    button.Resizer.ExportPath = filePathButton.textBox.Text;
                    if (button.IsChanged)
                    {
                        List<Image_ReSizer> images = new List<Image_ReSizer>();
                        foreach (RoundedButton round in buttons)
                        {
                            images.Add(round.Resizer);
                            round.IsChanged = false;
                        }

                        exportButton.images = images;
                    }
                }
                Thread.Sleep(300);
            }
        }

        private static void GetExportButton()
        {
            exportButton = new ExportButton();
            exportButton.Text = "Export";
            exportButton.Location = new Point(790, 0);
        }

        private static FolderButton GetFilePathButton(Form form)
        {
            FolderButton button = new FolderButton(form);

            return button;
        }

        private static RoundedButton GetNumPadButton(int buttonPosition, Form form)
        {
            RoundedButton button = new RoundedButton(form);
            int startX = 759;
            int y = 77;
            int heightWidth = 26;

            button.Height = heightWidth;
            button.Width = heightWidth;
            Point location = new Point(startX + (buttonPosition * 36), y);

            button.Location = location;
            button.BackColor = Color.Transparent;
            button.Cursor = Cursors.Hand;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            buttons.Add(button);

            return button;
        }

        private static RoundedButton GetMediaScreenButton(Form form)
        {
            RoundedButton button = new RoundedButton(form);
            int x = 638;
            int y = 4;
            int heightWidth = 75;

            button.Height = heightWidth;
            button.Width = heightWidth;
            Point location = new Point(x, y);

            button.Location = location;
            button.BackColor = Color.Transparent;
            button.Cursor = Cursors.Hand;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.CornerRadius = 75;
            button.IsMediaButton = true;
            buttons.Add(button);

            return button;
        }

        private static string GetIconImage()
        {
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            path = Path.Combine(path, "Assets");
            path = Path.Combine(path, "Icon");
            path = Path.Combine(path, "EverestMaxImageEditorLogo.ico");
            return path;
        }

        private static string GetKeyboardImage()
        {
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            path = Path.Combine(path, "Assets");
            path = Path.Combine(path, "Keyboard");
            path = Path.Combine(path, "EverestMax.PNG");
            return path;
        }
    }
}
