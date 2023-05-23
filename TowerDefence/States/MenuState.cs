﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefence.Components;
using TowerDefence.Managers;

namespace TowerDefence.States
{
    public class MenuState : State
    {
        private readonly List<Component> components;

        public MenuState(Game1 game, GraphicsDevice graphics) : base(game, graphics)
        {
            var loadGameButton = GameEngine.GetLoadGameMenuButton(game, graphics);

            var quitGameButton = GameEngine.GetExitMenuButton(game, graphics);

            var testGameButton = GameEngine.GetTestMenuButton(game, graphics);

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
            spriteBatch.Draw(TextureManager.MenuBackground,
                new Rectangle(0, 0, Constans.WindowWidth, Constans.WindowHeight), 
                Color.White);
            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }
    }
}
