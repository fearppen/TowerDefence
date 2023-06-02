using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefence.AbstractClasses;
using TowerDefence.Models;

using static TowerDefence.StaticClasses.TextureManager;

namespace TowerDefence.Controllers
{
    public class ProjectileController
    {
        public List<Projectile> Projectiles;

        public ProjectileController()
        {
            Projectiles = new List<Projectile>();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Projectiles.ForEach(projectile => projectile.Draw(gameTime, spriteBatch));
        }

        public void Update(GameTime gameTime)
        {
            Projectiles.RemoveAll(p => p.Update(gameTime));
        }

        public void AddBullet(Enemy enemy, Point position, int damage, int level)
        {
            var texture = level switch
            {
                2 => CannonBallTexture,
                3 => BlastTexture,
                _ => BulletTexture
            };

            Projectiles.Add(new Bullet(texture, position, damage, enemy));
        }
    }
}
