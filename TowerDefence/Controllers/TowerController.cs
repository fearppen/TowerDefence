using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System.Collections.Generic;
using TowerDefence.AbstractClasses;
using TowerDefence.Models;
using TowerDefence.StaticClasses;

using static TowerDefence.StaticClasses.TextureManager;

namespace TowerDefence.Controllers
{
    public class TowerController
    {
        public int LastTowerCost;
        private readonly List<Tower> towers;
        
        public TowerController() 
        {
            towers = new List<Tower>();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            towers.ForEach(tower => tower.Draw(gameTime, spriteBatch));
        }

        public void Update(GameTime gameTime, ProjectileController projectileController, List<Enemy> enemies)
        {
            towers?.ForEach(t => t.Attack(enemies, projectileController, gameTime.TotalGameTime.TotalSeconds));
            towers.RemoveAll(t => t.Update(gameTime));
        }

        public void AddGenericTowerInCell(MapCell cell)
        {
            var tower = new GenericTower(new List<Texture2D> { TowerTexture, CannonTowerTexture, BlastTowerTexture },
                            new Rectangle(cell.Rectangle.Left - Constants.CellSize / 2, cell.Rectangle.Top - Constants.CellSize / 2,
                            TowerTexture.Width, TowerTexture.Height));
            if (GameStats.Gold >= tower.Cost)
            {
                GameStats.Gold -= tower.Cost;
                towers.Add(tower); 
            }

        }

        public int GetTowerCost(MapCell cell)
        {
            return GetTowerByCell(cell).Cost;
        }

        public void UpgradeTowerInCell(MapCell cell)
        {
            var tower = GetTowerByCell(cell);
            if (tower == null || !tower.CanUpgrade() || tower.Cost > GameStats.Gold)
                return;
            tower.Upgrade();
        }

        public bool CanUpgradeTowerInCell(MapCell cell)
        {
            return GetTowerByCell(cell).CanUpgrade();
        }

        public Tower GetTowerByCell(MapCell cell)
        {
            foreach (var tower in towers)
            {
                var centerTowerRectangle = new Rectangle(tower.Rectangle.Center.X, tower.Rectangle.Center.Y, 1, 1);
                if (centerTowerRectangle.Intersects(cell.Rectangle))
                    return tower;
            }

            return null;
        }

        public bool IsTheseTowerCoords(double x, double y)
        {
            var rectangle = new Rectangle((int)x, (int)y, 1, 1);
            foreach (var tower in towers)
            {
                if (tower.Rectangle.Intersects(rectangle))
                    return true;
            }
            return false;
        }
    }
}
