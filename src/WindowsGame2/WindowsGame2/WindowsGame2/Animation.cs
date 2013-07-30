using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuttoPuput
{
    class Animation
    {
        protected Rectangle spriteRect;
        public Texture2D spriteTexture;
        public int textRows;
        public int textCols;
        protected int currentFrame;
        protected int totalFrames;

        public Animation(Texture2D texture, int rows, int columns, Rectangle position)
        {
            spriteTexture = texture;
            textRows = rows;
            textCols = columns;
            currentFrame = 0;
            totalFrames = textRows * textCols;
        }

        public void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = spriteTexture.Width / textCols;
            int height = spriteTexture.Height / textRows;
            int row = (int)((float)currentFrame / (float)textCols);
            int column = currentFrame % textCols;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

            spriteBatch.Draw(spriteTexture, spriteRect, sourceRectangle, Color.White);
        }
    }
}
