using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefence.Components;

namespace TowerDefence.Managers
{
    public class EnemyManager
    {
        public List<Enemy.Enemy> Enemies;
        public List<List<DelayedAction>> DelayedActions;
        public List<Enemy.Enemy> EnemiesInEnd;
        public int MaxWaves;

        private readonly Dictionary<char, Dictionary<string, int>> enemyStats;
        private readonly List<MapCell> startCells;
        private readonly List<List<List<MapCell>>> paths; 

        public EnemyManager(string filePath, Map map)
        {
            Enemies = new ();
            startCells = map.StartCells;
            enemyStats = GameEngine.GetEnemyStats();
            paths = map.Paths;
            DelayedActions = GameEngine.GetDelayedActions(filePath, enemyStats, paths, startCells, Enemies);
            MaxWaves = DelayedActions?.Count ?? 0;
        }

        public bool Update(GameTime gameTime, int waveNumber)
        {
            if (waveNumber < MaxWaves)
            {
                DelayedActions?[waveNumber].RemoveAll(a => a.Update(gameTime));
            }

            Enemies?.RemoveAll(e => e.Update(gameTime));
            return Enemies.Count == 0 && DelayedActions[waveNumber].Count == 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Enemies?.ForEach(e => e.Draw(gameTime, spriteBatch));
        }
    }
}