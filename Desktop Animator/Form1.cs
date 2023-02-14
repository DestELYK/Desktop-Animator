using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Windows.Input;
using System.Threading;

namespace Desktop_Animator
{
    public partial class Form1 : Form
    {
        public static Form1 application;

        AnimatorProfile profile;
        bool mouseDown;
        Point origin;
        Point originalLoc;

        public Form1(AnimatorProfile profile)
        {
            if (application == null)
                application = this;

            if (!File.Exists(profile.imageLoc))
            {
                Program.RemoveProfile(profile);
                return;
            }

            this.profile = profile;
            profile.form = this;
            InitializeComponent();
            this.ResizeRedraw = true;
            this.DoubleBuffered = true;
        }

        public Form1(AnimatorProfile profile, List<AnimatorProfile> profiles) : this(profile)
        {
            for(int i = 1; i < profiles.Count; i++)
            {
                new Form1(profiles[i]).Show();
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void PictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!mouseDown)
            {
                mouseDown = true;
                origin = e.Location;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.Location = new Point(this.Location.X - 8, this.Location.Y - 31);
            }
        }

        private void PictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (mouseDown)
            {
                Reset();
            }
        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (mouseDown)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    Reset();
                }
                else if (e.KeyCode == Keys.Add)
                {
                    profile.scale += 0.05d;
                    UpdateScale();
                }
                else if (e.KeyCode == Keys.Subtract)
                {
                    profile.scale -= 0.05d;
                    UpdateScale();
                }
                else if (e.KeyCode == Keys.D0)
                {
                    profile.scale = 1;
                    UpdateScale();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    Reset();
                    Program.RemoveProfile(profile);
                }
                else if (e.KeyCode == Keys.Left)
                {
                    this.Location = new Point(this.Location.X - 1, this.Location.Y);
                }
                else if (e.KeyCode == Keys.Right)
                {
                    this.Location = new Point(this.Location.X + 1, this.Location.Y);
                }
                else if (e.KeyCode == Keys.Up)
                {
                    this.Location = new Point(this.Location.X, this.Location.Y - 1);
                }
                else if (e.KeyCode == Keys.Down)
                {
                    this.Location = new Point(this.Location.X, this.Location.Y + 1);
                }
            }

            if (e.KeyCode == Keys.F4)
            {
                Reset();
                Program.AddGIF();
            }
        }

        private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }

        private void Reset()
        {
            mouseDown = false;

            profile.location = this.Location;
            Program.WriteAllProfiles();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = new Point(this.Location.X + 8, this.Location.Y + 31);
        }
    }
}
