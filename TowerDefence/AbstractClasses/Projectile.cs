using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence.AbstractClasses
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
