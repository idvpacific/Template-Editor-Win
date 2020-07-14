using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDV_Document_Templates
{
    public class TransparentColor : Label
    {
        public TransparentColor()
        {
            this.transparentBackColor = Color.Blue;
            this.opacity = 50;
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (Parent != null)
                {
                    using (var bmp = new Bitmap(Parent.Width, Parent.Height))
                    {
                        Parent.Controls.Cast<Control>()
                              .Where(c => Parent.Controls.GetChildIndex(c) > Parent.Controls.GetChildIndex(this))
                              .Where(c => c.Bounds.IntersectsWith(this.Bounds))
                              .OrderByDescending(c => Parent.Controls.GetChildIndex(c))
                              .ToList()
                              .ForEach(c => c.DrawToBitmap(bmp, c.Bounds));


                        e.Graphics.DrawImage(bmp, -Left, -Top);
                        using (var b = new SolidBrush(Color.FromArgb(this.Opacity, this.TransparentBackColor)))
                        {
                            e.Graphics.FillRectangle(b, this.ClientRectangle);
                        }
                        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        TextRenderer.DrawText(e.Graphics, this.Text, this.Font, this.ClientRectangle, this.ForeColor, Color.Transparent);
                    }
                }
            }
            catch (Exception)
            { }
        }

        private int opacity;
        public int Opacity
        {
            get { return opacity; }
            set
            {
                if (value >= 0 && value <= 255)
                    opacity = value;
                this.Invalidate();
            }
        }

        public Color transparentBackColor;
        public Color TransparentBackColor
        {
            get { return transparentBackColor; }
            set
            {
                transparentBackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {
                base.BackColor = Color.Transparent;
            }
        }

        private int color_R = 0;
        public int Color_R
        {
            get { return color_R; }
            set
            {
                color_R = value;
            }
        }

        private int color_G = 0;
        public int Color_G
        {
            get { return color_G; }
            set
            {
                color_G = value;
            }
        }

        private int color_B = 0;
        public int Color_B
        {
            get { return color_B; }
            set
            {
                color_B = value;
            }
        }

        private string similarity = "";
        public string Similarity
        {
            get { return similarity; }
            set
            {
                similarity = value;
            }
        }
    }
}
