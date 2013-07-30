using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuttoPuput
{
    class Female : Critter
    {


        public Female(Texture2D inTexture, Rectangle inRect, int age) :
            base(inTexture, inRect, age)
        {
        }

        public void CheckMates()
        {
            List<Critter> n = grid.GetNeighbors(row, col);
            foreach (Critter c in n)
            {
                if (c.GetPartner() && c.IsFertile())
                {
                    grid.SpawnFromTile(row, col);
                    return;
                }
            }
        }

    }
}
