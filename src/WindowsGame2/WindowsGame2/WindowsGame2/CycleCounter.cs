using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuttoPuput
{
    class CycleCounter : BaseSprite
    {
        float size;

        public CycleCounter(Texture2D inTexture, Rectangle inRect) :
            base(inTexture, inRect)
        {
        }

        public void Start()
        {
            size = 1.00f;
        }

        public void Decrement(float newSize)
        {
            size = newSize;
            spriteRect.Height = (int)(C.cycleCounterHeight * size + 0.5f);
            spriteRect.Y = C.cycleCounterY + (C.cycleCounterHeight - spriteRect.Height);
        }
    }
}
