using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TowerDefence.Towers
{
    public class GenericTower : Tower
    {
        public List<Texture2D> Texture;
        public int Damage = 15;
        public double speedAttck = 1.5f;
        public int rangeAttack = 200;
        public double lastShotTime;
        private Enemy.Enemy enemy;

        public GenericTower(List<Texture2D> texture, Rectangle rectangle) : base(texture, rectangle)
        {
            Texture = texture;
            Rectangle = rectangle;
            Cost = 20;
        }

        public override void Attack(List<Enemy.Enemy> enemies, List<Projectile> projectiles, double elapsedTime)
        {
            if (enemy != null && !enemy.IsDead)
            {
                if (GameEngine.IsInAttackRange(enemy, rangeAttack, Rectangle) && elapsedTime - lastShotTime > speedAttck)
                {
                    projectiles.Add(GameEngine.GetProjectile(enemy, Rectangle.Center, Damage, Level));
                    lastShotTime = elapsedTime;
                }
            }

            foreach (var enemy in enemies)
            {
               if (GameEngine.IsInAttackRange(enemy, rangeAttack, Rectangle) && elapsedTime - lastShotTime > speedAttck)
                {
                    this.enemy = enemy;
                    lastShotTime = elapsedTime;
                    projectiles.Add(GameEngine.GetProjectile(enemy, Rectangle.Center, Damage, Level));
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
            Cost *= 2;
        }
    }
}
