using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Animator
{
    [Serializable]
    public class AnimatorProfile
    {
        public string imageLoc;
        public System.Drawing.Point location;
        public double scale;

        [NonSerialized]
        public Form1 form;

        public AnimatorProfile(string imageLoc, System.Drawing.Point location, float scale)
        {
            this.imageLoc = imageLoc;
            this.location = location;
            this.scale = scale;
        }
    }
}
