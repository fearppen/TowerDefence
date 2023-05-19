using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence.Towers
{
    public class Bullet : Projectile
    {
        public Enemy.Enemy Target;
        private readonly int damage; 
        public Rectangle Rectangle { get { return new Rectangle(position.X, position.Y, texture.Width, texture.Height); } }
        private Point position;
        private readonly Texture2D texture;

        public Bullet(Texture2D texture, Point position, int damage, Enemy.Enemy enemy) : base(texture, position) 
        {
            this.texture = texture;
            this.position = position;
            this.damage = damage;
            Target = enemy;
        }

        public override void Damage()
        {
            Target.Damage(damage);
        }

        public override bool Update(GameTime gameTime)
        {
            if (Target.Rectangle.Intersects(Rectangle))
            {
                Damage();
                return true;
            }

            var vector = new Vector2(Rectangle.Center.X - Target.Rectangle.Center.X, 
                Rectangle.Center.Y - Target.Rectangle.Center.Y);
            vector.Normalize();
            position -= (speed * vector).ToPoint();

            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }
}
