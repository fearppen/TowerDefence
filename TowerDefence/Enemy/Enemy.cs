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
        public bool IsDead;
        public MapCell currentCell;
        private Directions direction;
        private Color color;
        private readonly AnimationManager animationManager;
        private readonly List<MapCell> path;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(position.X,
            position.Y - animationManager.CurrentAnimation.FrameHeight,
            animationManager.CurrentAnimation.FrameWidth,
            animationManager.CurrentAnimation.FrameHeight);
            }
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
            animationManager.AddAnimation("walk",
                new Animation(textures[0], 7, Constans.FrameTime, Constans.EnemySpriteSize, Constans.EnemySpriteSize));
        }

        public void Damage(int damage)
        {
            currentHp -= damage;
        }

        public override bool Update(GameTime gameTime)
        {
            if (currentHp <= 0)
            {
                IsDead = true;
                GameStats.Gold += Constans.GoldForEnemy;
                return true;
            }

            if (Math.Abs(currentCell.Rectangle.Center.X - Rectangle.Center.X) >= Constans.CellSize
                    || Math.Abs(currentCell.Rectangle.Center.Y - Rectangle.Center.Y) >= Constans.CellSize)
            {
                if (path.Count > 0)
                {
                    currentCell = path[0];
                    path.Remove(currentCell);
                    direction = ConvertDirection(currentCell.CellType);
                }

                else
                {
                    if (GameStats.Healths > 0)
                    {
                        GameStats.Healths--;
                    }

                    IsDead = true;
                    return true;
                }
            }

            position += direction switch
            {
                Directions.Up => new Point(0, -speed),
                Directions.Down => new Point(0, speed),
                Directions.Left => new Point(-speed, 0),
                _ => new Point(speed, 0),
            };

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

                _ => this.direction,
            };

            return direction;
        }
    }
}