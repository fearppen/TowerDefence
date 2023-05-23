using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Managers;
using TowerDefence.States;

namespace TowerDefence
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private State currentState;
        private State nextState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ChangeState(State state)
        {
            nextState = state; 
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = Constans.WindowWidth;
            graphics.PreferredBackBufferHeight = Constans.WindowHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.InitTexture(Content);
            currentState = new MenuState(this, graphics.GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseManager.Update();
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }

            currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}