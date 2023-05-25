using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefence.Controllers;
using TowerDefence.Models;

namespace TowerDefence.AbstractClasses
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

        public abstract void Upgrade();

        public override bool Update(GameTime gameTime)
        {
            return isSold;
        }

        public int Sell()
        {
            isSold = true;
            return Cost;
        }

        public abstract void Attack(List<Enemy> enemies, ProjectileController projectileController, double elapsedTime);

        public abstract bool CanUpgrade();
    }
}
