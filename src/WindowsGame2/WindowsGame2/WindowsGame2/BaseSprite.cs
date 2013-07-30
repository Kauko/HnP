using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuttoPuput
{
    class BaseSprite
    {
        protected Rectangle spriteRect;
        protected Texture2D spriteTexture;

        public BaseSprite(Texture2D inSpriteTexture, Rectangle inRect)
        {
            spriteRect = inRect;
            spriteTexture = inSpriteTexture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRect, Color.White);
        }
    }
}
