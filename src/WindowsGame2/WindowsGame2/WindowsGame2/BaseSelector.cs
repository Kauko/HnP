using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace RuttoPuput
{
    enum direction {up, down, left, right};

    class BaseSelector : BaseSprite
    {
        protected Grid grid;
        protected int originRow, originCol;

        public BaseSelector(Grid inGrid, Texture2D inTexture, Rectangle inRect, int inRow, int inCol) :
            base(inTexture, inRect)
        {
            grid = inGrid;
            originRow = inRow;
            originCol = inCol;
        }

        public virtual void Transform(direction dir)
        {
        }

        public virtual void Move(direction dir)
        {
        }

        public virtual void MouseMoveX(int col)
        {
        }

        public virtual void MouseMoveY(int row)
        {
        }
    }
}
