using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefence.Models;
using TowerDefence.StaticClasses;

using static TowerDefence.StaticClasses.TextureManager;

namespace TowerDefence.Controllers
{
    public class TextController
    {
        private const int StatsOffsetX = 170;
        private const int StatsOffsetY = 100;
        private const int DistanceBetweenTexts = 30;
        private readonly List<Text> texts;

        public TextController() 
        {
            texts = new List<Text>();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            texts.ForEach(t => t.Draw(gameTime, spriteBatch));
        }

        public void Update(GameTime gameTime)
        {
            foreach (var text in texts)
            {
                if (text.result.Contains("Золото"))
                    text.Update(gameTime, GameStats.Gold.ToString());
                else if (text.result.Contains("Жизни"))
                    text.Update(gameTime, GameStats.Healths.ToString());
            }
        }

        public void AddGoldInfoText()
        {
            texts.Add(new Text(Font, "Золото: ", 
                new Vector2(Constants.WindowWidth - StatsOffsetX, Constants.WindowHeight - StatsOffsetY),
                Color.Yellow, GameStats.Gold.ToString())); 
        }

        public void AddHealthsInfoText()
        {
            texts.Add(new Text(Font, "Жизни: ", 
                new Vector2(Constants.WindowWidth - StatsOffsetX, Constants.WindowHeight - StatsOffsetY - DistanceBetweenTexts),
                Color.Red, GameStats.Healths.ToString()));
        }

        public void AddEndLevelMessage(string text, Vector2 position)
        {
            texts.Add(new Text(Font, text, position, Color.Black, null));
        }
    }
}
