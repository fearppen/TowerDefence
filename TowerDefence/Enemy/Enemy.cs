using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Components;
using static TowerDefence.Managers.TextureManager;

namespace TowerDefence.Enemy
{
    public enum Directions
    {
        Down,
        Left, 
        Right,
        Up,
    }

    public class Enemy : Component
    {
        public int currentHp;
        public int maxHp;
        public int speed;
        public Point position;
        public Texture2D texture;
        public Directions direction;
        private Color color;

        public Rectangle Rectangle { get { return new Rectangle(position.X - texture.Width / 2, 
            position.Y - texture.Height / 2,
            texture.Width,
            texture.Height); }
        }
        
        public Enemy(int maxHp, int speed, Texture2D texture, Point position)
        {
            this.maxHp = maxHp;
            currentHp = maxHp;
            this.speed = speed;
            this.texture = texture;
            this.position = position;
            color = Color.White;
            direction = Directions.Right;
        }

        public void Damage(int damage)
        {
            currentHp -= damage;
        }

        public override bool Update(GameTime gameTime)
        {
            if (currentHp <= 0) 
            {
                return true;
            }

            position += direction switch
            {
                Directions.Up => new Point(0, -speed),
                Directions.Down => new Point(0, speed),
                Directions.Left => new Point(-speed, 0),
                _ => new Point(speed, 0),
            };

            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, color);
            var percentHp = (float)currentHp / maxHp;
            spriteBatch.Draw(BlockTexture, 
                new Rectangle(Rectangle.Left, 
                    Rectangle.Top - BlockTexture.Height,
                    (int)(texture.Width * percentHp),
                    BlockTexture.Height),
                Color.Red);
        }
    }
}