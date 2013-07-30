using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace RuttoPuput
{
    class Grid
    {
        int x, y, tileSize;
        int rows, cols;
        int spawnCount;
        int plagueCount;
        Critter[,] grid;
        CritterManager manager;
        BaseSelector selector;

        public Grid(CritterManager inManager, int gridX, int gridY, int gridSize)
        {
            x = gridX;
            y = gridY;
            tileSize = gridSize;
            manager = inManager;
        }

        public int LoadLevel(RuttoPuputLevel.Level level)
        {
            rows = level.GetRows();
            cols = level.GetCols();
            grid = new Critter[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    string symbol = level.GetTileContent(r, c);
                    char cType = symbol[0];
                    if (cType != '-')
                    {
                        LoadCritter(cType, Convert.ToInt32(symbol[1].ToString()), r, c);
                    }
                }
            }

            spawnCount = 0;
            return level.GetTarget();
        }

        private void LoadCritter(char cType, int age, int row, int col)
        {
            Critter critter = manager.SpawnCritter(cType, age);
            critter.Initialize(this, row, col, tileSize);
            grid[row, col] = critter;
        }


        public void PlaceSelector(int row, int col)
        {
            selector = new TwoByTwoSelector(this, new Rectangle(col * tileSize, row * tileSize, tileSize, tileSize), row, col);
        }

        public void PlaceCritter(int row, int col)
        {
            Critter critter = manager.SpawnRandomCritter();
            critter.Initialize(this, row, col, tileSize);
            grid[row,col] = critter;
            spawnCount++;
        }

        public void PlacePlagueCritter(int row, int col)
        {
            Critter critter = manager.SpawnPlagueCritter();
            critter.Initialize(this, row, col, tileSize);
            grid[row, col] = critter;
        }

        public List<Critter> GetNeighbors(int row, int col)
        {
            List<Critter> neighbours = new List<Critter>();
            if (row > 0 && grid[row - 1, col] != null) neighbours.Add(grid[row - 1, col]);
            if (row < (C.gridRows - 1) && grid[row + 1, col] != null) neighbours.Add(grid[row + 1, col]);
            if (col > 0 && grid[row,col-1] != null) neighbours.Add(grid[row,col-1]);
            if (col < (C.gridCols - 1) && grid[row, col + 1] != null) neighbours.Add(grid[row, col + 1]);
           
            return neighbours;
        }

        public Critter[] GetSelection(int row, int col, int width, int height)
        {
            Critter[] selected = new Critter[width * height];
            int i = 0;
            for (int c = col; c < col + width; c++)
            {
                for (int r = row; r < row + height; r++)
                {
                    if (grid[r, c] != null)
                    {
                        selected[i] = grid[r, c];
                        grid[r, c] = null;
                    }
                    i++;
                }
            }
            return selected;
        }

        public void SpawnFromTile(int row, int col)
        {
            if (row > 0 && col > 0 && grid[row - 1, col - 1] == null) PlaceCritter(row-1,col-1);
            if (row > 0 && grid[row - 1, col] == null) PlaceCritter(row - 1, col);
            if (row > 0 && col < (C.gridCols - 1) && grid[row - 1, col + 1] == null) PlaceCritter(row - 1, col + 1);
            if (col > 0 && grid[row, col - 1] == null) PlaceCritter(row, col - 1);
            if (col < (C.gridCols - 1) && grid[row, col + 1] == null) PlaceCritter(row, col + 1);
            if (row < (C.gridRows - 1) && col > 0 && grid[row + 1, col - 1] == null) PlaceCritter(row + 1, col - 1);
            if (row < (C.gridRows - 1) && grid[row + 1, col] == null) PlaceCritter(row + 1, col);
            if (row < (C.gridRows - 1) && col < (C.gridCols - 1) && grid[row + 1, col + 1] == null) PlaceCritter(row + 1, col + 1);
        }

        public void MoveCritters(Critter[] critters)
        {
            for (int i = 0; i < critters.Length; i++)
            {
                if (critters[i] != null)
                {
                    Critter c = critters[i];
                    grid[c.GetRow(), c.GetCol()] = c;
                }
            }
        }

        public void MousePosition(int mouseX, int mouseY)
        {
            mouseX = mouseX - x;
            int mouseCol = (int)(mouseX / tileSize + 0.5f);
            selector.MouseMoveX(mouseCol);

            mouseY = mouseY - y;
            int mouseRow = (int)(mouseY / tileSize + 0.5f);
            selector.MouseMoveY(mouseRow);
    }

        public void MoveSelector(direction dir)
        {
            selector.Move(dir);
        }

        public void ApplySelector(direction dir)
        {
            selector.Transform(dir);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            selector.Draw(spriteBatch);
        }

        public void ClearGrid(int r, int c)
        {
            grid[r, c] = null;
        }

        public void ReplaceWithPlague(int row, int col)
        {
            manager.RemovePlagued(grid[row, col]);
            Critter critter = manager.SpawnPlagueCritter();
            critter.Initialize(this, row, col, tileSize);
            grid[row, col] = critter;
            plagueCount++;
        }

        public int GetCycleScore()
        {
            int cycleScore = spawnCount * spawnCount - plagueCount * 50;
            spawnCount = 0;
            plagueCount = 0;
            return cycleScore;
        }
    }
}
