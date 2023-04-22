using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TowerDefence.Components;
using static TowerDefence.Managers.TextureManager;

namespace TowerDefence.States
{
    public class MenuState : State
    {
        private List<Component> components;

        public MenuState(Game1 game, GraphicsDevice graphics, ContentManager content) : base(game, graphics, content)
        {
            var buttonTexture = ButtonMenuTexture;
            var buttonFont = Font;

            var loadGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(1920 / 2 - 412 / 2, 1080 / 2 - 150 / 2 - 300),
                Text = "Уровни"
            };

            loadGameButton.Click += ClickLoadButton;

            var quitGameButton = new Button(buttonTexture, buttonFont) 
            { 
                Position = new Vector2(1920 / 2 - 412 / 2, 1080 / 2 - 150 / 2 + 300), 
                Text = "Выход" 
            };

            quitGameButton.Click += ClickExitButton;

            var testGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(1920 / 2 - 412 / 2, 1080 / 2 - 50),
                Text = "Тест"
            };

            testGameButton.Click += ClickTestButton;

            components = new List<Component>() { loadGameButton, quitGameButton, testGameButton };
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
            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }

        }

        private void ClickLoadButton(object sender, EventArgs e)
        {
            game.ChangeState(new LevelLoadState(game, graphicsDevice, contentManager));
        }

        private void ClickExitButton(object sender, EventArgs e)
        {
            game.Exit();
        }

        private void ClickTestButton(object sender, EventArgs e)
        {
            game.ChangeState(new GameState(game, graphicsDevice, contentManager, 0));
        }
    }
}
