using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WhackAMole
{
    public class Game1 : Game
    {
        void OnResize(Button[] buttons, int w, int h, GraphicsDeviceManager _graphics)//w and h is the client bounds
        {
            for(int i=0; i< currentNrOfButtons; i++)
            {
                buttons[i].OnResize(w, h);
            }
            _graphics.PreferredBackBufferWidth = w;
            _graphics.PreferredBackBufferHeight = h;
            _graphics.ApplyChanges();
            bgscale = (float)w / (float)backGroundT.Width;
            holescale = (float)w / (float)moleT.Width;
            
        }
        void SetHoles(Mole[,] holes, int w, int h)
        {;
            float hStep = (float)h / (float)3;
            float hStep1 = ((float)h / (float)3)*2;
            float hStep2 = h;
            float wStep = (float)w / (float)3;
            float wStep1 = ((float)w / (float)3) * 2;
            float wStep2 = w;
            holes[0, 0] = new Mole(moleT, holeT, holeForeT,koT, new Vector2(wStep - moleT.Width, hStep - moleT.Height * 0.5f),0);
            holes[0, 1] = new Mole(moleT, holeT, holeForeT,koT,new Vector2(wStep - moleT.Width, hStep1 - moleT.Height * 0.5f),1);
            holes[0, 2] = new Mole(moleT, holeT, holeForeT,koT,new Vector2(wStep - moleT.Width, hStep2 - moleT.Height * 0.5f),2);
            holes[1, 0] = new Mole(moleT, holeT, holeForeT,koT,new Vector2(wStep1 - moleT.Width, hStep - moleT.Height * 0.5f),3);
            holes[1, 1] = new Mole(moleT, holeT, holeForeT,koT,new Vector2(wStep1 - moleT.Width, hStep1 - moleT.Height * 0.5f),4);
            holes[1, 2] = new Mole(moleT, holeT, holeForeT,koT,new Vector2(wStep1 - moleT.Width, hStep2 - moleT.Height * 0.5f),5);
            holes[2, 0] = new Mole(moleT, holeT, holeForeT,koT,new Vector2(wStep2 - moleT.Width, hStep - moleT.Height * 0.5f),6);
            holes[2, 1] = new Mole(moleT, holeT, holeForeT,koT,new Vector2(wStep2 - moleT.Width, hStep1 - moleT.Height * 0.5f),7);
            holes[2, 2] = new Mole(moleT, holeT, holeForeT,koT,new Vector2(wStep2 - moleT.Width, hStep2 - moleT.Height*0.5f),8);

        }
        void Init()
        {
            timeValue = 60;
            scoreValue = 0;
            SetHoles(holes, windowW, windowH);
            ani.SetPos(new Vector2(windowW / 2, 0));
        }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        Animation ani;
        Color bgColor = new Color(111, 209, 72);
        enum GAMESTATE{MENU,OPTIONS,GAME, GAME_OVER }
        GAMESTATE currentState = GAMESTATE.MENU;
        Texture2D backGroundT;
        Texture2D holeT;
        Texture2D holeForeT;
        Texture2D moleT;
        Texture2D koT;
        Texture2D aniT;
        float bgscale = 1;
        float holescale = 1;
        SpriteFont arialSF;
        Vector2 backSize;
        Vector2 playSize;
        Vector2 mediumResSize;
        Vector2 highResSize;
        Vector2 optionsSize;
        Button backButton = new Button();
        Button optionsButton = new Button();
        Button playButton = new Button();
        Button mediumResButton = new Button();
        Button highResButton = new Button();
        int maxNrButtons = 10;
        int currentNrOfButtons = 0;
        Mole[,] holes = new Mole [3,3];
        Button[] buttons;
        int scoreValue = 0;
        string score = "SCORE: ";
        double timeValue = 60.0;
        int roundedTimeValue = 60;
        string time = "TIME: ";
        bool canClick = true;
        bool isInit = false;
        int windowW = 800;
        int windowH = 600;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arialSF = Content.Load<SpriteFont>("test");
            backGroundT = Content.Load<Texture2D>("background");
            holeT = Content.Load<Texture2D>("hole");
            holeForeT = Content.Load<Texture2D>("hole_foreground");
            moleT = Content.Load<Texture2D>("mole");
            koT = Content.Load<Texture2D>("mole_KO");
            aniT = Content.Load<Texture2D>("spritesheet_stone");
            bgscale = (float)Window.ClientBounds.X / (float)backGroundT.Width;
            holescale = (float)Window.ClientBounds.X / (float)moleT.Width;
            playSize = arialSF.MeasureString("PLAY");
            playButton.Init(playSize.X, playSize.Y, Window.ClientBounds.X, Window.ClientBounds.Y, "PLAY", Button.BUTTON_TYPE.PLAY);
            mediumResSize = arialSF.MeasureString("800x600");
            highResSize = arialSF.MeasureString("1920x1080");
            mediumResButton.Init(mediumResSize.X, mediumResSize.Y, Window.ClientBounds.X, Window.ClientBounds.Y, "800x600", Button.BUTTON_TYPE.MEDIUM);
            highResButton.Init(highResSize.X, highResSize.Y, Window.ClientBounds.X, Window.ClientBounds.Y, "1920x1080", Button.BUTTON_TYPE.HIGH);
            optionsSize = arialSF.MeasureString("OPTIONS");
            optionsButton.Init(optionsSize.X, optionsSize.Y, Window.ClientBounds.X, Window.ClientBounds.Y, "OPTIONS", Button.BUTTON_TYPE.OPTIONS);
            backSize = arialSF.MeasureString("BACK");
            backButton.Init(backSize.X, backSize.Y, Window.ClientBounds.X, Window.ClientBounds.Y, "BACK", Button.BUTTON_TYPE.BACK);
            ani = new Animation(aniT, new Vector2(400, 0));

            buttons = new Button[maxNrButtons];
            buttons[currentNrOfButtons++] = playButton;
            buttons[currentNrOfButtons++] = mediumResButton;
            buttons[currentNrOfButtons++] = highResButton;
            buttons[currentNrOfButtons++] = optionsButton;
            buttons[currentNrOfButtons++] = backButton;
            OnResize(buttons, 800, 600, _graphics);
            Init();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (currentState)
            {

                case GAMESTATE.MENU:
                    {
                        if(!isInit)
                        {
                            Init();
                            isInit = true;
                        }
                         Keys[] keys = Keyboard.GetState().GetPressedKeys();
                        if(keys.Length > 0)
                        {
                            if(keys[0] == Keys.Enter)
                            {
                                currentState = GAMESTATE.GAME;
                            }
                        }
                        if (playButton.Update(Mouse.GetState())==Button.PRESSED.HIT)
                        {
                            currentState = GAMESTATE.GAME;
                        }
                        if (optionsButton.Update(Mouse.GetState()) == Button.PRESSED.HIT)
                        {
                            currentState = GAMESTATE.OPTIONS;
                        }
                        ani.Update(gameTime.ElapsedGameTime.TotalSeconds);
                        break;
                    }
                case GAMESTATE.GAME:
                    {
                        if(timeValue<=0)
                        {
                            currentState = GAMESTATE.GAME_OVER;
                        }
                        if (backButton.Update(Mouse.GetState()) == Button.PRESSED.HIT)
                        {
                            currentState = GAMESTATE.MENU;
                            isInit = false;
                        }
                        
                            timeValue -= gameTime.ElapsedGameTime.TotalSeconds;
                        roundedTimeValue = (int)timeValue;
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                MouseState ms = Mouse.GetState();
                                holes[i, j].Update(gameTime.ElapsedGameTime.TotalSeconds, ms, ref scoreValue);
                                


                            }
                        }
                        break;
                    }
                case GAMESTATE.OPTIONS:
                    {
                        if (backButton.Update(Mouse.GetState()) == Button.PRESSED.HIT)
                        {
                            currentState = GAMESTATE.MENU;
                        }
                        if (mediumResButton.Update(Mouse.GetState()) == Button.PRESSED.HIT)
                        {
                            windowW = 800;
                            windowH = 600;
                            OnResize(buttons, windowW, windowH, _graphics);
                            SetHoles(holes, windowW, windowH);
                        }
                        if (highResButton.Update(Mouse.GetState()) == Button.PRESSED.HIT)
                        {
                            windowW = 1920;
                            windowH = 1080;
                            OnResize(buttons, windowW, windowH, _graphics);
                            SetHoles(holes, windowW, windowH);
                        }
                        break;
                    }
                case GAMESTATE.GAME_OVER:
                    {
                        if (backButton.Update(Mouse.GetState()) == Button.PRESSED.HIT)
                        {
                            currentState = GAMESTATE.MENU;
                            isInit = false;
                        }
                        break;
                    }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bgColor);
            spriteBatch.Begin();
            
            
            switch (currentState)
            {

                case GAMESTATE.MENU:
                    {
                        ani.Draw(spriteBatch, 1);
                        optionsButton.Draw(spriteBatch, arialSF);
                        playButton.Draw(spriteBatch, arialSF);
                        break;
                    }
                case GAMESTATE.GAME:
                    {
                        spriteBatch.Draw(backGroundT, new Vector2(0, 0), null, Color.White, 0.0f, new Vector2(0, 0), bgscale, SpriteEffects.None, 0);
                        

                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                holes[i, j].Draw(spriteBatch, 1);
                            }
                        }
                        backButton.Draw(spriteBatch, arialSF);
                        spriteBatch.DrawString(arialSF, score+scoreValue, new Vector2(0, windowH * 0.33f), Color.Red);
                        spriteBatch.DrawString(arialSF, time + roundedTimeValue, new Vector2(0, windowH * 0.66f), Color.Red);
                        break;
                    }
                case GAMESTATE.OPTIONS:
                    {
                        mediumResButton.Draw(spriteBatch, arialSF);
                        highResButton.Draw(spriteBatch, arialSF);
                        backButton.Draw(spriteBatch, arialSF);

                        break;
                    }
                case GAMESTATE.GAME_OVER:
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                holes[i, j].Draw(spriteBatch, 1);
                            }
                        }
                        backButton.Draw(spriteBatch, arialSF);
                        spriteBatch.DrawString(arialSF, "GAME OVER", new Vector2(windowW*0.5f, windowH * 0.5f), Color.Red);
                        spriteBatch.DrawString(arialSF, score+scoreValue, new Vector2(0, windowH * 0.33f), Color.Red);
                        break;
                    }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}