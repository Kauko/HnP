using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuttoPuput
{
    class Male : Critter
    {


        public Male(Texture2D inTexture, Rectangle inRect, int age) :
            base(inTexture, inRect, age)
        {
            SetPartner(true);
        }

    }
}
