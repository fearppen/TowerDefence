using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
        public readonly Rectangle rectangle;
        private readonly Texture2D texture;
        public static ContentManager Content;

        public MapCell(CellTypes cellType, Rectangle rectangle)
        {
            CellType = cellType;
            texture = GetTextureByCellType(CellType);
            this.rectangle = rectangle;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        }

        private Texture2D GetTextureByCellType(CellTypes cellType)
        {
            if (cellType == CellTypes.EmptyCell) 
            {
                return Content.Load<Texture2D>("Cells/grass");
            }

            else if (cellType == CellTypes.TowerCell)
            {
                return Content.Load<Texture2D>("Cells/purpleBlock");
            }
            return Content.Load<Texture2D>("Cells/wall");
        }
    }
}