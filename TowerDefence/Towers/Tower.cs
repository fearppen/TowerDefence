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
        internal bool isSold = false;

        public Tower(Texture2D texture, Rectangle rectangle)
        {
        }

        public int Sell()
        {
            isSold = true;
            return Cost;
        }

        public abstract void Attack(List<Enemy.Enemy> enemies, List<Projectile> projectiles, double elapsedTime);

        public abstract void Upgrade();

        public override bool Update(GameTime gameTime)
        {
            return isSold;
        }
    }
}
