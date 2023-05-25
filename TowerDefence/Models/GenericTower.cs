using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TowerDefence.AbstractClasses;
using TowerDefence.Controllers;
using TowerDefence.StaticClasses;

namespace TowerDefence.Models
{
    public class GenericTower : Tower
    {
        public List<Texture2D> Texture;
        public int Damage = 15;
        public double speedAttck = 1.5f;
        public int rangeAttack = 200;
        public double lastShotTime;
        private Enemy enemy;

        public GenericTower(List<Texture2D> texture, Rectangle rectangle) : base(texture, rectangle)
        {
            Texture = texture;
            Rectangle = rectangle;
            Cost = (int)Math.Pow(2, Level) * Constants.TowerCost;
        }

        public override void Attack(List<Enemy> enemies, ProjectileController projectileController, double elapsedTime)
        {
            if (enemy != null && !enemy.IsDead)
            {
                if (IsInAttackRange(enemy) && elapsedTime - lastShotTime > speedAttck)
                {
                    projectileController.AddBullet(enemy, Rectangle.Center, Damage, Level);
                    lastShotTime = elapsedTime;
                }
            }

            foreach (var enemy in enemies)
            {
                if (IsInAttackRange(enemy) && elapsedTime - lastShotTime > speedAttck)
                {
                    this.enemy = enemy;
                    lastShotTime = elapsedTime;
                    projectileController.AddBullet(enemy, Rectangle.Center, Damage, Level);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture[Level - 1], Rectangle, Color.White);
        }

        public override bool Update(GameTime gameTime)
        {
            return base.Update(gameTime);
        }

        public override void Upgrade()
        {
            Damage = (int)(Damage * 1.3);
            speedAttck /= 1.2f;
            rangeAttack += 20;
            GameStats.Gold -= Cost;
            Level++;
            Cost = (int)Math.Pow(2, Level) * Constants.TowerCost;
        }

        public override bool CanUpgrade()
        {
            return Level < 3;
        }

        private bool IsInAttackRange(Enemy enemy)
        {
            if (Math.Sqrt((enemy.Rectangle.Center.X - Rectangle.Center.X) * (enemy.Rectangle.Center.X - Rectangle.Center.X)
                + (enemy.Rectangle.Center.Y - Rectangle.Center.Y) * (enemy.Rectangle.Center.Y - Rectangle.Center.Y)) <= rangeAttack)
                return true;
            return false;
        }
    }
}