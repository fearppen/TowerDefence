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
        public List<List<DelayedAction>> DelayedActions;

        private readonly Dictionary<char, Dictionary<string, int>> enemyStats;
        private readonly List<MapCell> startCell;
        private readonly List<MapCell> endCell;
        private readonly List<List<MapCell>> paths; 
        private readonly Map map;

        public EnemyManager(string filePath, Map map) 
        {
            startCell = map.StartCell;
            endCell = map.EndCell;
            enemyStats = GetEnemyStats();
            this.map = map;
            paths = new List<List<MapCell>>();
            foreach (var start in startCell)
            {
                paths.Add(map.GetPathFromCell(start));
            }
            DelayedActions = GetDelayedActions(filePath);
        }

        public bool Update(GameTime gameTime, int waveNumber)
        {
            DelayedActions?[waveNumber].RemoveAll(a => a.Update(gameTime));
            Enemies?.RemoveAll(e => e.Update(gameTime));
            return Enemies.Count == 0 && DelayedActions[waveNumber].Count == 0;
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

        private List<List<DelayedAction>> GetDelayedActions(string filePath)
        {
            var delayedActions = new List<List<DelayedAction>>();
            var data = File.ReadAllLines(filePath);
            for (var i = 0; i < data.Length; i++)
            {
                var wave = data[i].Split();
                var enemies = new List<DelayedAction>();
                for (var j = 0; j < wave.Length; j++)
                {
                    for (var k = 0; k < wave[j].Length; k++)
                    {
                        var enemyType = wave[j][k];
                        var index = j;
                        var currentPath = new List<MapCell>(paths[index]);

                        var texture = enemyType switch
                        {
                            'v' => new List<Texture2D>() { VampireTexture },
                            'o' => new List<Texture2D>() { OrkWalkTexture },
                            _ => new List<Texture2D>() { VampireTexture },
                        };

                        enemies.Add(new DelayedAction(() =>
                            Enemies.Add(new Enemy.Enemy(enemyStats[enemyType]["maxHp"],
                            enemyStats[enemyType]["speed"],
                            texture,
                            new Point(startCell[index].Rectangle.Left, startCell[index].Rectangle.Top + 32),
                            currentPath)),
                            2000 * (k + 1)));
                    }

                delayedActions.Add(enemies);
                }
            }

            return delayedActions;
        }
    }
}