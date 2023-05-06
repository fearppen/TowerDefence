using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TowerDefence.Managers
{
    public static class TextureManager
    {
        public static Texture2D GrassTexture { get; private set; }
        public static Texture2D TowerBlockTexture { get; private set; }
        public static Texture2D WallTexture { get; private set; }
        public static Texture2D ButtonMenuTexture { get; private set; }
        public static Texture2D GameButtonTexture { get; private set; }
        public static SpriteFont Font { get; private set; }
        public static Texture2D TowerTexture { get; private set; }
        public static Texture2D BulletTexture { get; private set; }
        public static Texture2D BlockTexture { get; private set; }
        public static Texture2D VampireTexture { get; private set; }
        public static Texture2D OrkWalkTexture { get; private set; }

        public static void InitTexture(ContentManager content)
        {
            GrassTexture = content.Load<Texture2D>("Cells/grass");
            TowerBlockTexture = content.Load<Texture2D>("Cells/towerBlock");
            WallTexture = content.Load<Texture2D>("Cells/wall");
            ButtonMenuTexture = content.Load<Texture2D>("Controls/button");
            GameButtonTexture = content.Load<Texture2D>("Controls/gameButton");
            Font = content.Load<SpriteFont>("Fonts/font");
            TowerTexture = content.Load<Texture2D>("Towers/genericTower");
            BulletTexture = content.Load<Texture2D>("Towers/bullet");
            BlockTexture = content.Load<Texture2D>("Other/block");
            VampireTexture = content.Load<Texture2D>("Enemies/vampire");
            OrkWalkTexture = content.Load<Texture2D>("Enemies/orkWalk");
        }
    }
}
