using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RuttoPuput
{
    enum GameState { title, page1, page2, playing, gameover }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameState state;

        Grid grid;
        CritterManager critterManager;
        CycleCounter cycleCounter;

        ScoreString totalScoreString;
        ScoreString targetCyclesString;
        ScoreString currentCyclesString;
        ScoreString finalScoreString;

        int cycleTimer = 0;
        int cycleLength;
        int target;
        int cycles;
        int currentLevel = 0;
        int totalScore;

        int delayTimer;

        List<int> highScore = new List<int>();
        List<ScoreString> highScoreStrings;

        MouseState oldMouse;

        List<RuttoPuputLevel.Level> levels = new List<RuttoPuputLevel.Level>();

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = C.screenHeight;
            graphics.PreferredBackBufferWidth = C.screenWidth;

            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            int gridSize = C.screenWidth - C.xMarginLeft - C.xMarginRight;

            base.Initialize();

            critterManager = new CritterManager();
            cycleCounter = new CycleCounter(TextureRefs.cycleBar, new Rectangle(C.cycleCounterX, C.cycleCounterY, C.cycleCounterWidth, C.cycleCounterHeight));

            totalScoreString = new ScoreString(TextureRefs.pointsTexture, 0, C.totalScoreX, C.totalScoreY);
            currentCyclesString = new ScoreString(TextureRefs.pointsTexture, 0, C.currentCyclesX, C.currentCyclesY);
            targetCyclesString = new ScoreString(TextureRefs.pointsTexture, 0, C.targetCyclesX, C.targetCyclesY);
            finalScoreString = new ScoreString(TextureRefs.pointsTexture, 0, C.finalScoreX, C.finalScoreY);

            grid = new Grid(critterManager, C.xMarginLeft, C.yMargin, gridSize / C.gridCols);            
            grid.PlaceSelector(0, 0);

            this.IsMouseVisible = true;
            state = GameState.title;
        }


        private void NewLevel()
        {
            currentCyclesString.SetValue(0);
            critterManager.Initialize();
            grid.LoadLevel(levels[currentLevel]);
            target = levels[currentLevel].GetTarget();
            targetCyclesString.SetValue(target);
            cycleLength = levels[currentLevel].GetCycle();
            cycles = 0;
            cycleTimer = cycleLength;
            cycleCounter.Start();
            Mouse.SetPosition(C.xMarginLeft, C.yMargin);
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            levels.Add(this.Content.Load<RuttoPuputLevel.Level>("Levels/level_1"));
            levels.Add(this.Content.Load<RuttoPuputLevel.Level>("Levels/level_2"));
            levels.Add(this.Content.Load<RuttoPuputLevel.Level>("Levels/level_3"));
            levels.Add(this.Content.Load<RuttoPuputLevel.Level>("Levels/level_4"));
            levels.Add(this.Content.Load<RuttoPuputLevel.Level>("Levels/level_5"));
            levels.Add(this.Content.Load<RuttoPuputLevel.Level>("Levels/level_6"));
            levels.Add(this.Content.Load<RuttoPuputLevel.Level>("Levels/level_7"));
            levels.Add(this.Content.Load<RuttoPuputLevel.Level>("Levels/level_8"));
            levels.Add(this.Content.Load<RuttoPuputLevel.Level>("Levels/level_9"));

            TextureRefs.doctorTexture = this.Content.Load<Texture2D>("Textures/atlas_doctor");
            TextureRefs.maleTexture = this.Content.Load<Texture2D>("Textures/atlas_male");
            TextureRefs.femaleTexture = this.Content.Load<Texture2D>("Textures/atlas_female");
            TextureRefs.selectorTexture = this.Content.Load<Texture2D>("selector");
            TextureRefs.plagueTexture = this.Content.Load<Texture2D>("Textures/plague");
            TextureRefs.cycleBar = this.Content.Load<Texture2D>("Textures/cyclebar");
            TextureRefs.pointsTexture = this.Content.Load<Texture2D>("Textures/atlas_score");
            TextureRefs.levelComplete = this.Content.Load<Texture2D>("Textures/level_complete");

            TextureRefs.highscore = this.Content.Load<Texture2D>("Backgrounds/highscore");
            TextureRefs.background = this.Content.Load<Texture2D>("Backgrounds/background");
            TextureRefs.title = this.Content.Load<Texture2D>("Backgrounds/title");
            TextureRefs.hamsters = this.Content.Load<Texture2D>("Backgrounds/hamsters");
            TextureRefs.rules = this.Content.Load<Texture2D>("Backgrounds/rules");

            
            SoundRefs.bgMusic = this.Content.Load<Song>("Sounds/bgmusic");
            SoundRefs.selectorMove = this.Content.Load<SoundEffect>("Sounds/selector_move");
            SoundRefs.transformLeft = this.Content.Load<SoundEffect>("Sounds/transform_left");
            SoundRefs.transformRight = this.Content.Load<SoundEffect>("Sounds/transform_right");
            SoundRefs.error = this.Content.Load<SoundEffect>("Sounds/error");
            SoundRefs.gameOver = this.Content.Load<SoundEffect>("Sounds/gameover");
            SoundRefs.victory = this.Content.Load<SoundEffect>("Sounds/multiply");
            SoundRefs.countdown = this.Content.Load<SoundEffect>("Sounds/countdown");
            SoundRefs.blob = this.Content.Load<SoundEffect>("Sounds/blob");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        private void NewGame()
        {
            totalScore = 0;
            MediaPlayer.Play(SoundRefs.bgMusic);
            currentLevel = 0;
            NewLevel();
            totalScoreString.SetValue(0);

            state = GameState.playing;
            this.IsMouseVisible = false;
        }

        private void GameOver()
        {
            highScoreStrings = new List<ScoreString>();

            highScore.Add(totalScore);
            finalScoreString.SetValue(totalScore);
            highScore.Sort();
            highScore.Reverse();

            if (highScore.Count > 5)
            {
                highScore = highScore.GetRange(0, 5);
            }

            int i = 0;
            foreach (int hs in highScore)
            {
                highScoreStrings.Add(new ScoreString(TextureRefs.pointsTexture, hs, C.highScoreTopX, C.highScoreTopY + i * C.highScoreYSpacing));
                i++;
            }

            MediaPlayer.Stop();

            state = GameState.gameover;

            this.IsMouseVisible = true;
        }



        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Mouse control


            MouseState mouse = Mouse.GetState();

            if (delayTimer == 0)
            {

                switch (state)
                {
                    case GameState.title:
                        TitleScreen(mouse);
                        break;

                    case GameState.page1:
                        HelpPage1(mouse);
                        break;

                    case GameState.page2:
                        HelpPage2(mouse);
                        break;

                    case GameState.playing:
                        PlayingGame(gameTime, mouse);
                        break;

                    case GameState.gameover:
                        HighScore(mouse);
                        break;
                }
            }
            else if (delayTimer > 0)
            {
                delayTimer--;
            }

            oldMouse = mouse;
            base.Update(gameTime);
        }

        private void PlayingGame(GameTime gameTime, MouseState mouse)
        {
            grid.MousePosition(mouse.X, mouse.Y);

            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                grid.ApplySelector(direction.left);
            }
            else if (mouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Released)
            {
                grid.ApplySelector(direction.right);
            }

            // Timer

            cycleTimer = cycleTimer - gameTime.ElapsedGameTime.Milliseconds;

            cycleCounter.Decrement((float)cycleTimer / cycleLength);

            if (cycleTimer <= 0.0f)
            {
                cycleTimer = cycleLength;
                cycleCounter.Start();
                SoundRefs.blob.Play();

                if (critterManager.ManageCritters())
                {
                    SoundRefs.gameOver.Play();
                    GameOver();
                }

                cycles++;
                currentCyclesString.SetValue(cycles);
                totalScore = totalScore + grid.GetCycleScore();

                if (totalScore < 0) totalScore = 0;
                totalScoreString.SetValue(totalScore);

                if (cycles == target)
                {
                    cycles = 0;
                    SoundRefs.victory.Play();
                    currentLevel++;
                    totalScore = totalScore + (currentLevel + 1) * 200;
                    delayTimer = C.levelLoadWait;
                    if (currentLevel < levels.Count)
                    {
                        NewLevel();
                    }
                    else
                    {
                        GameOver();
                    }
                }
            }
        }

        private void HighScore(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released && delayTimer == 0)
            {
                NewGame();
            }
            else if (delayTimer > 0)
            {
                delayTimer--;
            }

        }

        private void TitleScreen(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                state = GameState.page1;
            }
        }

        private void HelpPage1(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                state = GameState.page2;
            }

        }

        private void HelpPage2(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                NewGame();
            }
        }

        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);

            // TODO: Add your drawing code here

            //spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin();

            switch (state)
            {
                case GameState.title:
                    spriteBatch.Draw(TextureRefs.title, new Vector2(0.0f, 0.0f), Color.White);
                    break;

                case GameState.page1:
                    spriteBatch.Draw(TextureRefs.hamsters, new Vector2(0.0f, 0.0f), Color.White);
                    break;

                case GameState.page2:
                    spriteBatch.Draw(TextureRefs.rules, new Vector2(0.0f, 0.0f), Color.White);
                    break;

                case GameState.playing:
                    spriteBatch.Draw(TextureRefs.background, new Vector2(0.0f, 0.0f), Color.White);

                    totalScoreString.Draw(spriteBatch);
                    critterManager.Draw(spriteBatch);
                    grid.Draw(spriteBatch);
                    cycleCounter.Draw(spriteBatch);
                    currentCyclesString.Draw(spriteBatch);
                    targetCyclesString.Draw(spriteBatch);

                    if (delayTimer > 0)
                    {
                        spriteBatch.Draw(TextureRefs.levelComplete, new Vector2(580, 20), Color.White);
                    }

                    break;

                case GameState.gameover:
                    spriteBatch.Draw(TextureRefs.highscore, new Vector2(0.0f, 0.0f), Color.White);
                    finalScoreString.Draw(spriteBatch);
                    foreach (ScoreString hss in highScoreStrings)
                    {
                        hss.Draw(spriteBatch);
                    }
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
