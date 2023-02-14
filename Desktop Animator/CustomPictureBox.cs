using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Animator
{
    class CustomPictureBox : PictureBox
    {
        public CustomPictureBox() : base()
        {

        }
        protected override void OnPaintBackground(PaintEventArgs e)
        // Paint background with underlying graphics from other controls
        {

            base.OnPaintBackground(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            base.OnPaint(e);
            //if (Image != null)
            //{

            //    if (Image.Size.Width > this.Width || Image.Size.Height > this.Height)

            //    {

            //        Bitmap bm_source = new Bitmap(Image);

            //        double scale = ((double)(Image.Height)) / ((double)(Image.Width));

            //        Bitmap bm_dest = new Bitmap(this.Size.Width, (int)(this.Size.Width * scale));

            //        Graphics gr_dest = Graphics.FromImage(bm_dest);
            //        gr_dest.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //        gr_dest.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //        gr_dest.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            //        gr_dest.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //        gr_dest.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

            //        gr_dest.DrawImage(bm_source, 0, 0, bm_dest.Width + 1, bm_dest.Height + 1);



            //        e.Graphics.DrawImage(bm_dest, (Width / 2) - (bm_dest.Width / 2), (Height / 2) - (bm_dest.Height / 2));



            //    }

            //    else

            //    {

            //        e.Graphics.DrawImage(Image, (Width / 2) - (Image.Width / 2), (Height / 2) - (Image.Height / 2));

            //    }

            //}
        }
    }
}
