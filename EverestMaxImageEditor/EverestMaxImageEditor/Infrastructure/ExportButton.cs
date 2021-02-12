using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EverestMaxImageEditor.Infrastructure
{
    public class ExportButton : Button
    {
        public List<Image_ReSizer> images;

        protected override void OnClick(EventArgs e)
        {
            if (images != null)
            {
                try
                {
                    foreach (Image_ReSizer image in images)
                    {
                        if (image.IsMediaButton)
                        {
                            image.SaveKeyboardMediaImage();
                        }
                        else
                        {
                            image.SaveKeyboardImage();
                        }
                    }
                    MessageBox.Show("Images saved.", "", MessageBoxButtons.OK);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error saving files. Please try again.", "", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("No images to save...","",MessageBoxButtons.OK);
            }
        }
    }
}
