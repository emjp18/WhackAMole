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
            holes[0, 0] = new Mole(moleT, holeT, holeForeT, new Vector2(wStep - moleT.Width, hStep - moleT.Height));
            holes[0, 1] = new Mole(moleT, holeT, holeForeT, new Vector2(wStep - moleT.Width, hStep1 - moleT.Height));
            holes[0, 2] = new Mole(moleT, holeT, holeForeT, new Vector2(wStep - moleT.Width, hStep2 - moleT.Height));
            holes[1, 0] = new Mole(moleT, holeT, holeForeT, new Vector2(wStep1 - moleT.Width, hStep - moleT.Height));
            holes[1, 1] = new Mole(moleT, holeT, holeForeT, new Vector2(wStep1 - moleT.Width, hStep1 - moleT.Height));
            holes[1, 2] = new Mole(moleT, holeT, holeForeT, new Vector2(wStep1 - moleT.Width, hStep2 - moleT.Height));
            holes[2, 0] = new Mole(moleT, holeT, holeForeT, new Vector2(wStep2 - moleT.Width, hStep - moleT.Height));
            holes[2, 1] = new Mole(moleT, holeT, holeForeT, new Vector2(wStep2 - moleT.Width, hStep1 - moleT.Height));
            holes[2, 2] = new Mole(moleT, holeT, holeForeT, new Vector2(wStep2 - moleT.Width, hStep2 - moleT.Height));

        }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        enum GAMESTATE{MENU,OPTIONS,GAME }
        GAMESTATE currentState = GAMESTATE.MENU;
        Texture2D backGroundT;
        Texture2D holeT;
        Texture2D holeForeT;
        Texture2D moleT;
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
            arialSF = Content.Load<SpriteFont>("Arial");
            backGroundT = Content.Load<Texture2D>("background");
            holeT = Content.Load<Texture2D>("hole");
            holeForeT = Content.Load<Texture2D>("hole_foreground");
            moleT = Content.Load<Texture2D>("mole");
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

            SetHoles(holes, Window.ClientBounds.Width, Window.ClientBounds.Height);

            buttons = new Button[maxNrButtons];
            buttons[currentNrOfButtons++] = playButton;
            buttons[currentNrOfButtons++] = mediumResButton;
            buttons[currentNrOfButtons++] = highResButton;
            buttons[currentNrOfButtons++] = optionsButton;
            buttons[currentNrOfButtons++] = backButton;
           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (currentState)
            {

                case GAMESTATE.MENU:
                    {

                        if (playButton.Update(Mouse.GetState())==Button.PRESSED.HIT)
                        {
                            currentState = GAMESTATE.GAME;
                        }
                        if (optionsButton.Update(Mouse.GetState()) == Button.PRESSED.HIT)
                        {
                            currentState = GAMESTATE.OPTIONS;
                        }
                        break;
                    }
                case GAMESTATE.GAME:
                    {
                        if (backButton.Update(Mouse.GetState()) == Button.PRESSED.HIT)
                        {
                            currentState = GAMESTATE.MENU;
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                holes[i, j].Update(gameTime.ElapsedGameTime.TotalSeconds);
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
                            OnResize(buttons, 800, 600, _graphics);
                            SetHoles(holes, 800, 600);
                        }
                        if (highResButton.Update(Mouse.GetState()) == Button.PRESSED.HIT)
                        {
                            OnResize(buttons, 1920, 1080, _graphics);
                            SetHoles(holes, 1920, 1080);
                        }
                        break;
                    }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);
            spriteBatch.Begin();
            
            
            switch (currentState)
            {

                case GAMESTATE.MENU:
                    {
                        
                        optionsButton.Draw(spriteBatch, arialSF);
                        playButton.Draw(spriteBatch, arialSF);
                        break;
                    }
                case GAMESTATE.GAME:
                    {
                        //spriteBatch.Draw(backGroundT, new Vector2(0, 0), null, Color.White, 0.0f, new Vector2(0, 0), bgscale, SpriteEffects.None, 0);
                        

                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                holes[i, j].Draw(spriteBatch, 1);
                            }
                        }
                        backButton.Draw(spriteBatch, arialSF);
                        break;
                    }
                case GAMESTATE.OPTIONS:
                    {
                        mediumResButton.Draw(spriteBatch, arialSF);
                        highResButton.Draw(spriteBatch, arialSF);
                        backButton.Draw(spriteBatch, arialSF);

                        break;
                    }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}