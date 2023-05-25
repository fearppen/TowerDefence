using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Models;
using TowerDefence.StaticClasses;

using static TowerDefence.StaticClasses.TextureManager;

namespace TowerDefence.Controllers
{
    public class BackgroundController
    {
        private Point startPosition;
        private Background currentBackground;

        public BackgroundController() 
        {
            startPosition = new Point(0, 0);
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            currentBackground.Draw(gametime, spriteBatch);
        }

        public bool Update(GameTime gameTime) => currentBackground.Update(gameTime);

        public void SetMenuBackground()
        {
            currentBackground = new Background(MenuBackground, startPosition, Constants.WindowWidth, Constants.WindowHeight);
        }

        public void SetLevelLoadBackground()
        {
            currentBackground = new Background(LevelLoadBackground, startPosition, Constants.WindowWidth, Constants.WindowHeight);
        }

        public void SetEndLevelBackground()
        {
            currentBackground = new Background(EndBackground, startPosition, Constants.WindowWidth, Constants.WindowHeight);
        }
    }
}
