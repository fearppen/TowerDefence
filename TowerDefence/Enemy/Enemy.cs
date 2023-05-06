using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
        private const int SpriteSize = 64;
        private Directions direction;
        private Color color;
        private AnimationManager animationManager;
        private List<MapCell> path;
        private MapCell currentCell;

        public Rectangle Rectangle { get { return new Rectangle(position.X, 
            position.Y - animationManager.CurrentAnimation.FrameHeight,
            animationManager.CurrentAnimation.FrameWidth,
            animationManager.CurrentAnimation.FrameHeight); }
        }
        
        public Enemy(int maxHp, int speed, List<Texture2D> textures, Point position, List<MapCell> path)
        {
            this.maxHp = maxHp;
            currentHp = maxHp;
            this.speed = speed;
            this.position = position;
            this.path = path;
            currentCell = path[0];
            path.Remove(currentCell);
            color = Color.White;
            direction = ConvertDirection(path[1].CellType);
            animationManager = new AnimationManager();
            animationManager.AddAnimation("walk", new Animation(textures[0], 7, 100, SpriteSize, SpriteSize));
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
            if (Math.Abs(currentCell.Rectangle.Center.X - Rectangle.Center.X) >= 64
                    || Math.Abs(currentCell.Rectangle.Center.Y - Rectangle.Center.Y) >= 64)
            {
                if (path.Count > 0)
                {
                    currentCell = path[0];
                    path.Remove(currentCell);
                    direction = ConvertDirection(currentCell.CellType);
                }
            }

            position += direction switch
            {
                Directions.Up => new Point(0, -speed),
                Directions.Down => new Point(0, speed),
                Directions.Left => new Point(-speed, 0),
                _ => new Point(speed, 0),
            };
            /*if (position.X < 0 || position.Y >= 1920 || position.Y < 0 || position.Y > 1080)
                return true;
*/
            animationManager.CurrentAnimation.Update(gameTime);
            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var percentHp = (float)currentHp / maxHp;
            spriteBatch.Draw(BlockTexture,
                new Rectangle(Rectangle.Left,
                    Rectangle.Top - BlockTexture.Height,
                    (int)(animationManager.CurrentAnimation.FrameWidth * percentHp),
                    BlockTexture.Height),
                Color.Red);
            animationManager.CurrentAnimation.Draw(spriteBatch, Rectangle, color);
        }

        private Directions ConvertDirection(CellTypes cellType)
        {
            var direction = cellType switch
            {
                CellTypes.PathLeftCell => Directions.Left,
                CellTypes.PathBottomCell => Directions.Down,
                CellTypes.PathRightCell => Directions.Right,
                CellTypes.PathTopCell => Directions.Up,
               /* CellTypes.PathRightLeftBottomCell => (number % 3) switch
                {
                    1 => Directions.Left,
                    2 => Directions.Right,
                    _ => Directions.Down,
                },
                CellTypes.PathRightLeftCell => (number % 2) switch
                {
                    1 => Directions.Right,
                    _ => Directions.Left,
                },*/

                _ => Directions.Down,
            };

            return direction;
        }
    }
}