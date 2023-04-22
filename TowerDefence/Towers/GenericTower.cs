using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TowerDefence.Towers
{
    public class GenericTower : Tower
    {
        public Texture2D Texture;
        public int Damage = 15;

        public GenericTower(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
            Texture = texture;
            Rectangle = rectangle;
            Cost = 20;
        }

        public override List<Projectile> Attack(List<Enemy.Enemy> enemies)
        {
            return new List<Projectile>();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

        public override void Update()
        {
        }
    }
}
