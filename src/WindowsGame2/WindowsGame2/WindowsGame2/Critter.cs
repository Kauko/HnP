using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuttoPuput
{
    enum Age { dummy, baby, teen, adult, old, grave }

    class Critter
    {
        protected int row, col;
        protected int age;
        private bool partner = false;
        protected bool heal = false;
        protected bool canBeSick = true;

        protected Rectangle spriteRect;
        protected Texture2D spriteTexture;
        public int textRows { get; set; }
        public int textCols { get; set; }
        private int currentFrame;
        private int totalFrames;

        protected Grid grid;

        public Critter(Texture2D inTexture, Rectangle inRect, int inAge)
        {
            spriteTexture = inTexture;
            age = inAge;
            currentFrame = age;
        }

        public void Initialize(Grid inGrid, int inRow, int inCol, int size)
        {
            grid = inGrid;
            row = inRow;
            col = inCol;
            spriteRect.Width = size;
            spriteRect.Height = size;
            spriteRect.X = col * size + C.xMarginLeft;
            spriteRect.Y = row * size + C.yMargin;
        }

        public void Move(int rowAdjust, int colAdjust)
        {
            row = row + rowAdjust;
            col = col + colAdjust;
            spriteRect.X = col * spriteRect.Width + C.xMarginLeft;
            spriteRect.Y = row * spriteRect.Height + C.yMargin;
        }

        public virtual void IncreaseAge()
        {
            age++;
            if (currentFrame != C.atlasLength) currentFrame++;
            if (age == C.death) canBeSick = false;
        }

        public int GetRow()
        {
            return row;
        }

        public int GetCol()
        {
            return col;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int width = spriteTexture.Width / C.atlasLength;
            int height = spriteTexture.Height;
            int row = (int)((float)currentFrame / (float)textCols);
            int column = currentFrame % C.atlasLength;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

            spriteBatch.Draw(spriteTexture, spriteRect, sourceRectangle, Color.White);
        }

        public int GetAge()
        {
            return age;
        }

        public void SetPartner(bool k)
        {
            partner = k;
        }

        public bool GetPartner()
        {
            return partner;
        }

        public void RemoveFromGrid()
        {
            grid.ClearGrid(row, col);
        }

        public bool IsFertile()
        {
            if (age == 3) return true;
            else return false;
        }

        public void SetCanBeSick(bool a)
        {
            canBeSick = a;
        }

        public bool GetCanBeSick()
        {
            return canBeSick;
        }

        
    }
}
