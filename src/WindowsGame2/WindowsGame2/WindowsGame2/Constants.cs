using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace RuttoPuput
{
    static class C
    {
        public const int screenWidth = 800;
        public const int screenHeight = 600;

        public const int xMarginLeft = 215;
        public const int xMarginRight = 5;
        public const int yMargin = 15;

        public const int gridRows = 15;
        public const int gridCols = 15;

        public const Keys upKey = Keys.Up;
        public const Keys downKey = Keys.Down;
        public const Keys leftKey = Keys.Left;
        public const Keys rightKey = Keys.Right;
        public const Keys leftTransformKey = Keys.D1;
        public const Keys rightTransformKey = Keys.D2;

        public static char[] genderDistribution = new char[10] {'M', 'F', 'M', 'F', 'M', 'F', 'M', 'F', 'M', 'D'};

        public const int cycleCounterHeight = 363;
        public const int cycleCounterWidth = 36;
        public const int cycleCounterX = 88;
        public const int cycleCounterY = 219;

        public const int atlasLength = 6;
        public const int stringAtlasLenght = 12;

        /*public const int maleTextureRows = 1;
        public const int femaleTextureRows = 1;
        public const int doctorTextureRows = 1;
        public const int maleTextureCols = 6;
        public const int femaleTextureCols = 6;
        public const int doctorTextureCols = 6;*/

        public const int death = 4;

        //Muokkaamalla näitä muutetaan piste stringien paikkaa ruudulla
        public const int totalScoreY = 85;
        public const int totalScoreX = 175;
        public const int currentCyclesY = 545;
        public const int currentCyclesX = 75;
        public const int targetCyclesY = 545;
        public const int targetCyclesX = 180;

        public const int highScoreTopX = 555;
        public const int highScoreTopY = 297;
        public const int highScoreYSpacing = 54;

        public const int finalScoreX = 465;
        public const int finalScoreY = 120;

        //Kuinka suuri tila on yhdellä kirjaimella
        public const int letterSize = 25;

        public const int levelLoadWait = 120;
    }
}
