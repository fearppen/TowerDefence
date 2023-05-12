using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Components;
using TowerDefence.Enemy;

namespace TowerDefence.Towers
{
    public abstract class Projectile : Component
    {
        public int speed = 10;

        public Projectile(Texture2D texture, Point position)
        {
        }

        public abstract void Damage();
    }
}
