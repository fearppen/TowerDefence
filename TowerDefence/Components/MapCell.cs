using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Managers;

namespace TowerDefence.Components
{
    public enum CellTypes
    {
        EmptyCell,
        PathLeftCell,
        PathRightCell,
        PathBottomCell,
        PathRightLeftCell,
        PathRightLeftBottomCell,
        StartCell,
        EndCell,
        TowerCell,
    }

    public class MapCell : Component
    {
        public readonly CellTypes CellType;
        public readonly Rectangle Rectangle;
        private readonly Texture2D texture;
        public static ContentManager Content;

        public MapCell(CellTypes cellType, Rectangle rectangle)
        {
            CellType = cellType;
            texture = GetTextureByCellType(CellType);
            Rectangle = rectangle;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        }

        private Texture2D GetTextureByCellType(CellTypes cellType)
        {
            if (cellType == CellTypes.EmptyCell)
            {
                return TextureManager.GrassTexture;
            }

            else if (cellType == CellTypes.TowerCell)
            {
                return TextureManager.TowerBlockTexture;
            }
            return TextureManager.WallTexture;
        }
    }
}