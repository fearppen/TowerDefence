using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using TowerDefence.Models;
using TowerDefence.StaticClasses;
using static TowerDefence.StaticClasses.TextureManager;

namespace TowerDefence.Controllers
{
    public class EnemyController
    {
        public const int EnemyDelay = 4000;
        public const int DelayFromWaves = 5000;
        public const int EnemyHeightSpriteSize = 64;
        public const int EnemyWidthSpriteSize = 77;

        public List<Enemy> Enemies;
        public List<List<DelayedAction>> DelayedActions;
        public List<Enemy> EnemiesInEnd;
        public int MaxWaves;

        private readonly Dictionary<char, Dictionary<string, int>> enemyStats;
        private readonly List<MapCell> startCells;
        private readonly List<List<List<MapCell>>> paths;

        public EnemyController(string filePath, Map map)
        {
            Enemies = new();
            startCells = map.StartCells;
            enemyStats = GetEnemyStats();
            paths = map.Paths;
            DelayedActions = GetDelayedActions(filePath);
            MaxWaves = DelayedActions?.Count ?? 0;
        }

        public bool Update(GameTime gameTime)
        {
            if (GameStats.Wave < MaxWaves)
            {
                DelayedActions?[GameStats.Wave - 1].RemoveAll(a => a.Update(gameTime));
            }

            Enemies?.RemoveAll(e => e.Update(gameTime));
            return Enemies.Count == 0 && DelayedActions[GameStats.Wave - 1].Count == 0;
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
                var enemyStats = data[i].Split();
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
                        var currentPath = new List<MapCell>(paths[index][(k + 1) % paths[index].Count]);

                        enemies.Add(GetDelayedActionEnemy(enemyType, startCells[j],
                            currentPath, (int)((k + 1) * EnemyDelay / (i + 1) * 1.5) + DelayFromWaves));
                    }

                    delayedActions.Add(enemies);
                }
            }

            return delayedActions;
        }

        private DelayedAction GetDelayedActionEnemy(char enemyType, MapCell startCell, List<MapCell> path,
            int delay)
        {
            var texture = enemyType switch
            {
                'o' => new List<Texture2D>() { OrkWalkTexture },
                'a' => new List<Texture2D>() { AxeEnemyWalkTexture },
                's' => new List<Texture2D>() { SwordEnemyWalkTexture },
                'k' => new List<Texture2D>() { SpearEnemyWalkTexture },
                _ => new List<Texture2D>() { OrkWalkTexture },
            };

            return new DelayedAction(() => Enemies.Add(new Enemy(enemyStats[enemyType]["maxHp"],
                enemyStats[enemyType]["speed"], texture, new Point(startCell.Rectangle.Left, startCell.Rectangle.Top + 64),
                path, EnemyWidthSpriteSize, EnemyHeightSpriteSize)),
                delay);
        }
    }
}