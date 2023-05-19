using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TowerDefence.Components;
using TowerDefence.Managers;
using TowerDefence.States;
using TowerDefence.Towers;
using static TowerDefence.Managers.TextureManager;

namespace TowerDefence
{
    public static class GameEngine
    {
        public static Button GetGoToMenuButton(Game1 game, GraphicsDevice graphics)
        {
            var button = new Button(ButtonMenuTexture, Font)
            {
                Position = new Vector2(Constans.WindowWidth / 2 - ButtonMenuTexture.Width / 2,
                Constans.WindowHight / 2 + ButtonMenuTexture.Height),
                Text = "Выйти в меню",
            };

            button.Click += (o, s) => { game.ChangeState(new MenuState(game, graphics)); };
            return button; 
        }

        public static Button GetContinueButton()
        {
            var button = new Button(ButtonMenuTexture, Font)
            {
                Position = new Vector2(Constans.WindowWidth / 2 - ButtonMenuTexture.Width / 2,
                Constans.WindowHight / 2 - ButtonMenuTexture.Height),
                Text = "Продолжить игру",
            };

            button.Click += (o, s) => { GameStats.CurrentState = GameStates.Playing; };

            return button;
        }

        public static Button GetRestartLevelButton(Game1 game, GraphicsDevice graphics, int levelId)
        {
            var button = new Button(ButtonMenuTexture, Font)
            {
                Position = new Vector2(Constans.WindowWidth / 2 - ButtonMenuTexture.Width / 2,
                Constans.WindowHight / 2 - ButtonMenuTexture.Height),
                Text = "Перезапустить уровень",
            };

            button.Click += (o, s) => { game.ChangeState(new GameState(game, graphics, levelId)); };
            return button;
        }

        public static Button GetLoadGameMenuButton(Game1 game, GraphicsDevice graphics)
        {
            var loadGameButton = new Button(ButtonMenuTexture, Font)
            {
                Position = new Vector2(Constans.WindowWidth / 2 - ButtonMenuTexture.Width / 2,
                Constans.WindowHight / 2 - ButtonMenuTexture.Height / 2 - ButtonMenuTexture.Height * 2),
                Text = "Уровни"
            };

            loadGameButton.Click += (o, s) => { game.ChangeState(new LevelLoadState(game, graphics)); };
            return loadGameButton;
        }

        public static Button GetExitMenuButton(Game1 game, GraphicsDevice graphics)
        {
            var quitGameButton = new Button(ButtonMenuTexture, Font)
            {
                Position = new Vector2(Constans.WindowWidth / 2 - ButtonMenuTexture.Width / 2,
                Constans.WindowHight / 2 - ButtonMenuTexture.Height / 2 + ButtonMenuTexture.Height * 2),
                Text = "Выход"
            };

            quitGameButton.Click += (o, s) => { game.Exit(); };

            return quitGameButton;
        }

        public static Button GetTestMenuButton(Game1 game, GraphicsDevice graphics)
        {
            var testGameButton = new Button(ButtonMenuTexture, Font)
            {
                Position = new Vector2(Constans.WindowWidth / 2 - ButtonMenuTexture.Width / 2,
                Constans.WindowHight / 2 - ButtonMenuTexture.Height / 3),
                Text = "Тест"
            };

            testGameButton.Click += (o, s) => { game.ChangeState(new GameState(game, graphics, 0)); };

            return testGameButton;
        }

        public static Texture2D GetTextureByCellType(CellTypes cellType)
        {
            if (cellType == CellTypes.EmptyCell)
            {
                return GrassTexture;
            }

            else if (cellType == CellTypes.TowerCell)
            {
                return TowerBlockTexture;
            }
            return WallTexture;
        }

        public static Button GetBuyButton(MapCell cell, List<Tower> towers)
        {
            var button = new Button(GameButtonTexture, Font)
            {
                Position = new Vector2(cell.Rectangle.Left + Constans.XBuyOffset, cell.Rectangle.Top + Constans.YBuyOffset),
                Text = "Купить башню"
            };

            button.Click += (o, s) =>
            {
                var tower = GameEngine.GetGenericTowerByCell(cell);
                if (GameStats.Gold >= tower.Cost)
                {
                    towers.Add(tower);
                    GameStats.Gold -= tower.Cost;
                }
                button.ClickButton();
            };

            return button;
        }

        public static Button GetUpdateButton(MapCell cell, List<Tower> towers)
        {
            var button = new Button(GameButtonTexture, Font)
            {
                Position = new Vector2(cell.Rectangle.Left + Constans.XBuyOffset, cell.Rectangle.Top + Constans.YBuyOffset),
                Text = "Улучшить"
            };

            button.Click += (o, s) =>
            {
                var tower = GetTowerByCell(towers, cell);
                if (GameStats.Gold >= tower.Cost)
                {
                    tower.Upgrade();
                }
                button.ClickButton();
            };

            return button;
        }

        public static GenericTower GetGenericTowerByCell(MapCell cell) 
        {
            return new GenericTower(TowerTexture,
                            new Rectangle(cell.Rectangle.Left - Constans.CellSize / 2, cell.Rectangle.Top - Constans.CellSize / 2,
                            TowerTexture.Width, TowerTexture.Height));

        }

        public static List<MapCell> GetAllTowerCells(Map map)
        {
            var cells = new List<MapCell>();
            foreach (var cell in map.cells)
            {
                if (cell.CellType == CellTypes.TowerCell)
                    cells.Add(cell);
            }
            return cells;
        }

        public static DelayedAction GetDelayedActionEnemy(Dictionary<char, Dictionary<string, int>> enemyStats,
            char enemyType, MapCell startCell, List<MapCell> path, int delay, List<Enemy.Enemy> enemies)
        {
            var texture = enemyType switch
            {
                'v' => new List<Texture2D>() { VampireTexture },
                'o' => new List<Texture2D>() { OrkWalkTexture, OrkDieTexture },
                _ => new List<Texture2D>() { VampireTexture },
            };

            return new DelayedAction(() => enemies.Add(new Enemy.Enemy(enemyStats[enemyType]["maxHp"],
                enemyStats[enemyType]["speed"], texture, new Point(startCell.Rectangle.Left, startCell.Rectangle.Top + 32), 
                path)), 
                delay);
        }

        public static Projectile GetBulletProjectile(Enemy.Enemy enemy, Point position, int damage)
        {
            return new Bullet(BulletTexture, position, damage, enemy);
        }

        public static Dictionary<char, Dictionary<string, int>> GetEnemyStats()
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

        public static List<List<DelayedAction>> GetDelayedActions(string filePath, Dictionary<char, Dictionary<string, int>> stats,
            List<List<MapCell>> paths, List<MapCell> startCells, List<Enemy.Enemy> Enemies)
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

                        enemies.Add(GetDelayedActionEnemy(stats, enemyType, startCells[j],
                            currentPath, (k + 1) * Constans.EnemyDelay, Enemies));
                    }

                    delayedActions.Add(enemies);
                }
            }

            return delayedActions;
        }

        public static bool IsInAttackRange(Enemy.Enemy enemy, int distance, Rectangle rectangle)
        {
            if (Math.Sqrt((enemy.Rectangle.Center.X - rectangle.Center.X) * (enemy.Rectangle.Center.X - rectangle.Center.X)
                + (enemy.Rectangle.Center.Y - rectangle.Center.Y) * (enemy.Rectangle.Center.Y - rectangle.Center.Y)) <= distance)
                return true;
            return false;
        }

        public static Tower GetTowerByCell(List<Tower> towers, MapCell cell)
        {
            foreach (var  tower in towers)
            {
                var centerTowerRectangle = new Rectangle(tower.Rectangle.Center.X, tower.Rectangle.Center.Y, 1, 1);
                if (centerTowerRectangle.Intersects(cell.Rectangle))
                    return tower;
            }

            return null;
        }
    }
}
