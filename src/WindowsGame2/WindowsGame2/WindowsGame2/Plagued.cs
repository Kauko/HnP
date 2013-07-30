using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuttoPuput
{
    class Plagued : Critter
    {

        public Plagued(Texture2D inTexture, Rectangle inRect, int age) :
            base(inTexture, inRect, age)
        {
            SetCanBeSick(false);
        }

        public void SpreadPlague()
        {
            List<Critter> n = grid.GetNeighbors(row, col);
            foreach (Critter cr in n)
            {
                if (cr.GetCanBeSick())
                {
                    grid.ReplaceWithPlague(cr.GetRow(), cr.GetCol());
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRect, Color.White);
        }
    }
}
