using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TowerDefence.Controls;

namespace TowerDefence.States
{
    public class MenuState : State
    {
        private List<Component> components;

        public MenuState(Game1 game, GraphicsDevice graphics, ContentManager content) : base(game, graphics, content)
        {
            var buttonTexture = content.Load<Texture2D>("Controls/Button");
            var buttonFont = content.Load<SpriteFont>("Fonts/Font");

            var loadGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(1920 / 2 - 412 / 2, 1080 / 2 - 150 / 2 - 150),
                Text = "Уровни"
            };

            loadGameButton.Click += ClickLoadButton;

            var quitGameButton = new Button(buttonTexture, buttonFont) 
            { 
                Position = new Vector2(1920 / 2 - 412 / 2, 1080 / 2 - 150 / 2 + 150), 
                Text = "Выход" 
            };

            quitGameButton.Click += ClickExitButton;

            this.components = new List<Component>() { loadGameButton, quitGameButton };
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                component.Update(gameTime);
            }    
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        private void ClickLoadButton(object sender, EventArgs e)
        {
            game.ChangeState(new GameState(game, graphicsDevice, contentManager));
        }

        private void ClickExitButton(object sender, EventArgs e)
        {
            game.Exit();
        }
    }
}
