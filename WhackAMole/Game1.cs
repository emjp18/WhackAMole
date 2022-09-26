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
        }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        enum GAMESTATE{MENU,OPTIONS,GAME }
        GAMESTATE currentState = GAMESTATE.MENU;
        Texture2D backGroundT;
        float bgscale = 1;
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
            bgscale = (float)Window.ClientBounds.X / (float)backGroundT.Width;
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
                        }
                        if (highResButton.Update(Mouse.GetState()) == Button.PRESSED.HIT)
                        {
                            OnResize(buttons, 1920, 1080, _graphics);
                        }
                        break;
                    }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
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
                        spriteBatch.Draw(backGroundT, new Vector2(0, 0), null, Color.White, 0.0f, new Vector2(0, 0), bgscale, SpriteEffects.None, 0);
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