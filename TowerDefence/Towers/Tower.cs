using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TowerDefence.Components;
using TowerDefence.Enemy;

namespace TowerDefence.Towers
{
    public abstract class Tower : Component
    {
        public int Cost { get; internal set; }
        public Rectangle Rectangle { get; internal set; }
        private bool isSold = false;

        public Tower(Texture2D texture, Rectangle rectangle)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public int Sell()
        {
            isSold = true;
            return Cost;
        }

        public abstract void Update();

        public abstract List<Projectile> Attack(List<Enemy.Enemy> enemies);

    }
}
