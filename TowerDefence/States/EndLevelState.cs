using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TowerDefence.Components;
using TowerDefence.Managers;

namespace TowerDefence.States
{
    public class EndLevelState : State
    {
        private readonly string text;
        private readonly Vector2 position;
        private readonly List<Component> components;

        public EndLevelState(Game1 game, GraphicsDevice graphics, int levelId, bool isWon) : base(game, graphics)
        {
            text = isWon ? "Вы победили! Сыграйте снова или попробуйте другой уровень" : "Вы проиграли. Попробуйте сыграть снова!";
            var width = isWon ? Constans.WindowWidth / 3.6f : Constans.WindowWidth / 2.78f;
            position = new Vector2(width, Constans.WindowHight / 10);
            components = new List<Component>
            {
                GameEngine.GetGoToMenuButton(game, graphics),
                GameEngine.GetRestartLevelButton(game, graphics, levelId)
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.EndBackground, new Rectangle(0, 0, Constans.WindowWidth, Constans.WindowHight), Color.White);
            components.ForEach(component => component.Draw(gameTime, spriteBatch));
            spriteBatch.DrawString(TextureManager.Font, text, position, Color.Black);
        }

        public override void Update(GameTime gameTime)
        {
            components.RemoveAll(component => component.Update(gameTime));
        }
    }
}
