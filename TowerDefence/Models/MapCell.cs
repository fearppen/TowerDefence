using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.AbstractClasses;

namespace TowerDefence.Models
{
    public enum CellTypes
    {
        EmptyCell = 0,
        PathLeftCell = 1,
        PathRightCell = 2,
        PathBottomCell = 3,
        PathRightLeftCell = 4,
        PathRightLeftBottomCell = 5,
        StartCell = 6,
        EndCell = 7,
        TowerCell = 8,
        PathTopCell = 9,
    }

    public class MapCell : Component
    {
        public CellTypes CellType;
        public readonly Rectangle Rectangle;
        private readonly Texture2D texture;

        public MapCell(CellTypes cellType, Rectangle rectangle, Texture2D texture)
        {
            CellType = cellType;
            this.texture = texture;
            Rectangle = rectangle;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }

        public override bool Update(GameTime gameTime) => false;

        public MapCell CloneWithOtherType(CellTypes cellType)
        {
            return new MapCell(cellType, Rectangle, texture);
        }
    }
}