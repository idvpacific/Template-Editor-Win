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
    public class TransparentLabel : Label
    {
        public TransparentLabel()
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

        private bool outputshow = false;
        public bool OutputShow
        {
            get { return outputshow; }
            set
            {
                outputshow = value;
            }
        }

        private string outputtitle = "";
        public string OutputTitle
        {
            get { return outputtitle; }
            set
            {
                outputtitle = value;
            }
        }

        private bool keyactive = false;
        public bool KeyActive
        {
            get { return keyactive; }
            set
            {
                keyactive = value;
            }
        }

        private string keyvalue = "";
        public string KeyValue
        {
            get { return keyvalue; }
            set
            {
                keyvalue = value;
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

        private string ocrindex = "1";
        public string OcrIndex
        {
            get { return ocrindex; }
            set
            {
                ocrindex = value;
            }
        }

        private string ocrposition = "L";
        public string OcrPosition
        {
            get { return ocrposition; }
            set
            {
                ocrposition = value;
            }
        }

        private bool data_processing  = false;
        public bool DataProcessing
        {
            get { return data_processing; }
            set
            {
                data_processing = value;
            }
        }

        private int typecode = 1;
        public int TypeCode
        {
            get { return typecode; }
            set
            {
                typecode = value;
            }
        }

        private int substring_start = 0;
        public int SubstringStart
        {
            get { return substring_start; }
            set
            {
                substring_start = value;
            }
        }

        private int substring_length = 0;
        public int SubstringLength
        {
            get { return substring_length; }
            set
            {
                substring_length = value;
            }
        }

        private bool substring_left = false;
        public bool SubstringLeft
        {
            get { return substring_left; }
            set
            {
                substring_left = value;
            }
        }

        private string inputformat = "";
        public string InputFormat
        {
            get { return inputformat; }
            set
            {
                inputformat = value;
            }
        }

        private string inputformatseprator = "";
        public string InputFormatSeprator
        {
            get { return inputformatseprator; }
            set
            {
                inputformatseprator = value;
            }
        }

        private string Outputformat = "";
        public string OutputFormat
        {
            get { return Outputformat; }
            set
            {
                Outputformat = value;
            }
        }

        private string Outputformatseprator = "";
        public string OutputFormatSeprator
        {
            get { return Outputformatseprator; }
            set
            {
                Outputformatseprator = value;
            }
        }
    }
}
