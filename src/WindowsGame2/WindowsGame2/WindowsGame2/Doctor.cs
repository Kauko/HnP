using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuttoPuput 
{
    class Doctor : Critter
    {

        public Doctor(Texture2D inTexture, Rectangle inRect, int age) :
            base(inTexture, inRect, age)
        {
        }

        public override void IncreaseAge()
        {
            base.IncreaseAge();
            SetCanBeSick(false);
        }



    }
}
