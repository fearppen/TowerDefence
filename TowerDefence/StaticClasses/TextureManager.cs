using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TowerDefence.StaticClasses
{
    public static class TextureManager
    {
        public static Texture2D GrassTexture { get; private set; }
        public static Texture2D TowerBlockTexture { get; private set; }
        public static Texture2D WallTexture { get; private set; }

        public static Texture2D ButtonMenuTexture { get; private set; }
        public static Texture2D GameButtonTexture { get; private set; }
        public static Texture2D LevelSelectButtonTexture { get; private set; }

        public static SpriteFont Font { get; private set; }

        public static Texture2D TowerTexture { get; private set; }
        public static Texture2D BulletTexture { get; private set; }
        public static Texture2D BlockTexture { get; private set; }
        public static Texture2D BlastTowerTexture { get; private set; }
        public static Texture2D BlastTexture { get; private set; }
        public static Texture2D CannonTowerTexture { get; private set; }
        public static Texture2D CannonBallTexture { get; private set; }

        public static Texture2D OrkWalkTexture { get; private set; }
        public static Texture2D AxeEnemyWalkTexture { get; private set; }
        public static Texture2D SpearEnemyWalkTexture { get; private set; }
        public static Texture2D SwordEnemyWalkTexture { get; private set; }

        public static Texture2D MenuBackground { get; private set; }
        public static Texture2D EndBackground { get; private set; }
        public static Texture2D LevelLoadBackground { get; private set; }

        public static void InitTexture(ContentManager content)
        {
            GrassTexture = content.Load<Texture2D>("Cells/grass");
            TowerBlockTexture = content.Load<Texture2D>("Cells/towerBlock");
            WallTexture = content.Load<Texture2D>("Cells/wall");
            ButtonMenuTexture = content.Load<Texture2D>("Controls/button");
            GameButtonTexture = content.Load<Texture2D>("Controls/gameButton");
            LevelSelectButtonTexture = content.Load<Texture2D>("Controls/LevelButton");
            Font = content.Load<SpriteFont>("Fonts/font");
            BlockTexture = content.Load<Texture2D>("Other/block");
            OrkWalkTexture = content.Load<Texture2D>("Enemies/OrkEnemyWalk");
            AxeEnemyWalkTexture = content.Load<Texture2D>("Enemies/AxeEnemyWalk");
            SpearEnemyWalkTexture = content.Load<Texture2D>("Enemies/SpearEnemyWalk");
            SwordEnemyWalkTexture = content.Load<Texture2D>("Enemies/SwordEnemyWalk");
            MenuBackground = content.Load<Texture2D>("Backgrounds/MenuBackground");
            EndBackground = content.Load<Texture2D>("Backgrounds/EndBackground");
            LevelLoadBackground = content.Load<Texture2D>("Backgrounds/SelectLevelBackground");
            BlastTowerTexture = content.Load<Texture2D>("towers/BlastTower");
            BlastTexture = content.Load<Texture2D>("towers/Blast");
            CannonTowerTexture = content.Load<Texture2D>("towers/CannonTower");
            CannonBallTexture = content.Load<Texture2D>("towers/CannonBall");
            TowerTexture = content.Load<Texture2D>("towers/GenericTower");
            BulletTexture = content.Load<Texture2D>("towers/Bullet");
        }
    }
}
