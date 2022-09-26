using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WhackAMole
{
    public class Game1 : Game
    {
        void onResize(Button[] buttons, float w, float h)//w and h is the client bounds
        {
            for(int i=0; i<buttons.Length; i++)
            {
                buttons[i].onResize(w, h);
            }
        }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        enum GAMESTATE{MENU,OPTIONS,GAME }
        GAMESTATE currentState = GAMESTATE.MENU;
        Texture2D backGroundT;
        float bgscale = 1;
        SpriteFont arialSF;
        Vector2 playSize;
        Button playButton;
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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arialSF = Content.Load<SpriteFont>("Arial");
            backGroundT = Content.Load<Texture2D>("background");
            bgscale = (float)Window.ClientBounds.X / (float)backGroundT.Width;
            playSize = arialSF.MeasureString("PLAY");
            buttons = new Button[maxNrButtons];
            buttons[currentNrOfButtons++] = playButton;
            playButton.Init(playSize.X, playSize.Y, Window.ClientBounds.X, Window.ClientBounds.Y, "PLAY", Button.BUTTON_TYPE.PLAY);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (currentState)
            {

                case GAMESTATE.MENU:
                    {
                        for(int i = 0; i < currentNrOfButtons; i++)
                        {
                            buttons[i].Update(Mouse.GetState());
                        }

                        if (playButton.GetPressed()==Button.PRESSED.HIT)
                        {
                            currentState = GAMESTATE.GAME;
                        }

                        break;
                    }
                case GAMESTATE.GAME:
                    {
                        break;
                    }
                case GAMESTATE.OPTIONS:
                    {
                        break;
                    }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(backGroundT, new Vector2(0, 0), null, Color.White, 0.0f, new Vector2(0, 0), bgscale, SpriteEffects.None, 0);
            
            switch (currentState)
            {

                case GAMESTATE.MENU:
                    {
                        spriteBatch.DrawString(arialSF, "PLAY", new Vector2(Window.ClientBounds.X * 0.25f, Window.ClientBounds.Y * 0.25f), Color.Red);
                        break;
                    }
                case GAMESTATE.GAME:
                    {
                        break;
                    }
                case GAMESTATE.OPTIONS:
                    {
                        break;
                    }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}