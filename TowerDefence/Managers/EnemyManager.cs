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

        private readonly Dictionary<char, Dictionary<string, int>> enemyStats;
        private readonly List<MapCell> startCells;
        private readonly List<List<MapCell>> paths; 

        public EnemyManager(string filePath, Map map)
        {
            Enemies = new ();
            startCells = map.StartCell;
            enemyStats = GameEngine.GetEnemyStats();
            paths = new ();
            foreach (var start in startCells)
            {
                paths.Add(map.GetPathFromCell(start));
            }
            DelayedActions = GameEngine.GetDelayedActions(filePath, enemyStats, paths, startCells, Enemies);
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
    }
}