using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace EverestMaxImageEditor.Infrastructure
{
    public class RoundedButton : Button
    {
        //we can use this to modify the color of the border 
        public Color BorderColor = Color.Transparent;
        //we can use this to modify the border size
        public int BorderSize = 0;
        //to modify the corner radius
        public int CornerRadius = 10;
        public string ExportPath;
        public string ExportFileName;
        public Image_ReSizer Resizer { get; private set; }
        public bool IsMediaButton;
        public bool IsChanged;
        private Form form;
        int top;
        int left;
        int right;
        int bottom;

        public RoundedButton(Form form)
        {
            this.form = form;
            Resizer = new Image_ReSizer();
            IsMediaButton = false;
        }


        GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();
            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                             Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
            GraphPath.CloseFigure();
            return GraphPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // to draw the control using base OnPaint
            base.OnPaint(e);

            Pen DrawPen = new Pen(BorderColor);
            GraphicsPath gfxPath_mod = new GraphicsPath();

            top = 0;
            left = 0;
            right = Width;
            bottom = Height;

            gfxPath_mod.AddArc(left, top, CornerRadius, CornerRadius, 180, 90);
            gfxPath_mod.AddArc(right - CornerRadius, top, CornerRadius, CornerRadius, 270, 90);
            gfxPath_mod.AddArc(right - CornerRadius, bottom - CornerRadius,
                CornerRadius, CornerRadius, 0, 90);
            gfxPath_mod.AddArc(left, bottom - CornerRadius, CornerRadius, CornerRadius, 90, 90);

            gfxPath_mod.CloseAllFigures();

            e.Graphics.DrawPath(DrawPen, gfxPath_mod);

            int inside = 1;

            Pen newPen = new Pen(BorderColor, BorderSize);
            GraphicsPath gfxPath = new GraphicsPath();
            gfxPath.AddArc(left + inside + 1, top + inside, CornerRadius, CornerRadius, 180, 100);

            gfxPath.AddArc(right - CornerRadius - inside - 2,
                top + inside, CornerRadius, CornerRadius, 270, 90);
            gfxPath.AddArc(right - CornerRadius - inside - 2,
                bottom - CornerRadius - inside - 1, CornerRadius, CornerRadius, 0, 90);

            gfxPath.AddArc(left + inside + 1,
            bottom - CornerRadius - inside, CornerRadius, CornerRadius, 95, 95);
            e.Graphics.DrawPath(newPen, gfxPath);

            this.Region = new System.Drawing.Region(gfxPath_mod);

            if (IsMediaButton)
            {
                Rectangle rect = new Rectangle(0, 65, 70, 10);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 10), rect);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.CheckFileExists = true;
                fileDialog.CheckPathExists = true;
                fileDialog.Title = "Select Image";
                fileDialog.Multiselect = false;
                fileDialog.Filter = "JPEG (*.jpg)|*.jpg;|PNG (*.png)|*.png;|BMP (*.bmp)|*.bmp;";
                DialogResult result = fileDialog.ShowDialog();
                try
                {
                    Resizer.ImportPath = fileDialog.FileName;
                    Resizer.IsMediaButton = IsMediaButton;
                    Resizer.ExportPath = ExportPath;
                    Resizer.ExportFileName = ExportFileName;
                    if (IsMediaButton)
                    {
                        this.Image = Resizer.GetMediaButton();
                        IsChanged = true;
                    }
                    else
                    {
                        this.Image = Resizer.GetKeyboardButtons();
                        IsChanged = true;
                    }
                }
                catch (Exception ex) { }
            }
        }
    }
}
