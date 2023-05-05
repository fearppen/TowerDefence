using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using TowerDefence.Components;
using static TowerDefence.Managers.TextureManager;

namespace TowerDefence.Managers
{
    public class EnemyManager
    {
        public List<Enemy.Enemy> Enemies = new ();
        public List<DelayedAction> DelayedActions;

        private readonly Dictionary<char, Dictionary<string, int>> enemyStats;
        private readonly MapCell startCell;
        private readonly MapCell endCell;

        public EnemyManager(string filePath, MapCell startCell, MapCell endCell) 
        {
            this.startCell = startCell;
            enemyStats = GetEnemyStats();
            this.endCell = endCell;
            DelayedActions = GetDelayedActions(filePath);
        }

        public void Update(GameTime gameTime)
        {
            DelayedActions?.RemoveAll(a => a.Update(gameTime));
            Enemies?.RemoveAll(e => e.Update(gameTime));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Enemies?.ForEach(e => e.Draw(gameTime, spriteBatch));
        }

        private Dictionary<char, Dictionary<string, int>> GetEnemyStats()
        {
            var stats = new Dictionary<char, Dictionary<string, int>>();
            var data = File.ReadAllLines(string.Format(@"..\..\..\Content\stats.txt"));
            for (int i = 0; i < data.Length; i++)
            {
                var enemyStats = data[0].Split();
                var maxHp = int.Parse(enemyStats[1].Split(':')[1]);
                var speed = int.Parse(enemyStats[2].Split(':')[1]);
                stats[enemyStats[0].ToCharArray()[0]] = new Dictionary<string, int>
                {
                    ["maxHp"] = maxHp,
                    ["speed"] = speed
                };
            }

            return stats;
        }

        private List<DelayedAction> GetDelayedActions(string filePath)
        {
            var delayedActions = new List<DelayedAction>();
            var data = File.ReadAllText(filePath);
            for (var i = 0; i < data.Length; i++)
            {
                var enemyType = data[i];
                var texture = enemyType switch
                {
                    'v' => VampireTexture,
                    _ => VampireTexture,
                };
                delayedActions.Add(new DelayedAction(() =>
                    Enemies.Add(new Enemy.Enemy(enemyStats[enemyType]["maxHp"],
                    enemyStats[enemyType]["speed"],
                    texture,
                    new Point(startCell.Rectangle.Left, startCell.Rectangle.Right))), 2000 * (i + 1)));
            }

            return delayedActions;
        }
    }
}
