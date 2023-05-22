using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefence.Components;

namespace TowerDefence.Towers
{
    public abstract class Tower : Component
    {
        public int Level = 1;

        public int Cost { get; internal set; }
        public Rectangle Rectangle { get; internal set; }
        internal bool isSold = false;

        public Tower(List<Texture2D> texture, Rectangle rectangle)
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
