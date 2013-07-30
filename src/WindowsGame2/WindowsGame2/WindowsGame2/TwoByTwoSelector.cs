using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuttoPuput
{
    class TwoByTwoSelector : BaseSelector
    {
        private const int width = 2;
        private const int height = 2;

        private int[,] rightTransform = new int[,] { { 0, 1 }, { -1, 0 }, { 1, 0 }, { 0, -1 } };
        private int[,] leftTransform = new int[,] { { 1, 0 }, { 0, 1 }, { 0, -1 }, { -1, 0 } };

        public TwoByTwoSelector(Grid inGrid, Rectangle inRect, int inRow, int inCol) :
            base(inGrid, TextureRefs.selectorTexture, inRect, inRow, inCol) 
        {
            spriteRect.Width = spriteRect.Width * width;
            spriteRect.Height = spriteRect.Height * height;
        } 

        public override void Transform(direction dir)
        {
            Critter[] critters = grid.GetSelection(originRow, originCol, width, height);
            Boolean canTransform = true;

            for (int i = 0; i < critters.Length; i++)
            {
                if (critters[i] != null && (critters[i].GetType().Name == "Plagued" || critters[i].GetAge() == 5))
                {
                    canTransform = false;
                    break;
                }
            }

            if (canTransform)
            {
                for (int i = 0; i < critters.Length; i++)
                {
                    if (critters[i] != null)
                    {
                        if (dir == direction.left)
                        {
                            critters[i].Move(leftTransform[i, 0], leftTransform[i, 1]);
                            SoundRefs.transformLeft.Play();
                        }
                        else if (dir == direction.right)
                        {
                            critters[i].Move(rightTransform[i, 0], rightTransform[i, 1]);
                            SoundRefs.transformRight.Play();
                        }
                    }
                }
            }
            else
            {
                SoundRefs.error.Play();
            }


            grid.MoveCritters(critters);
        }

        public override void Move(direction dir)
        {
            if (dir == direction.up && originRow > 0)
            {
                originRow = originRow - 1;
                spriteRect.Y = spriteRect.Y - spriteRect.Height / 2;
            }
            else if (dir == direction.down && originRow + 1 < C.gridRows - 1)
            {
                originRow = originRow + 1;
                spriteRect.Y = spriteRect.Y + spriteRect.Height / 2;
            }
            else if (dir == direction.left && originCol > 0)
            {
                originCol = originCol - 1;
                spriteRect.X = spriteRect.X - spriteRect.Width / 2;
            }
            else if (dir == direction.right && originCol + 1 < C.gridCols - 1)
            {
                originCol = originCol + 1;
                spriteRect.X = spriteRect.X + spriteRect.Width / 2;
            }
        }

        public override void MouseMoveX(int col)
        {
            if (col >= 0 && col < C.gridCols - 2)
            {
                originCol = col;
            }
            else if (col < 0)
            {
                originCol = 0;
            }
            else if (col > C.gridCols - 2)
            {
                originCol = C.gridCols - 2;
            }
            spriteRect.X = originCol * spriteRect.Width / 2 + C.xMarginLeft;

        }

        public override void MouseMoveY(int row)
        {
            if (row >= 0 && row < C.gridRows - 2)
            {
                originRow = row;
            }
            else if (row < 0)
            {
                originRow = 0;
            }
            else if (row > C.gridRows - 2)
            {
                originRow = C.gridRows - 2;
            }
            spriteRect.Y = originRow * spriteRect.Height / 2 + C.yMargin;

        }

    }
}
