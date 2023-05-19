using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Security.Policy;
using TowerDefence.Enemy;

namespace TowerDefence.Components
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
        public readonly CellTypes CellType;
        public readonly Rectangle Rectangle;
        private readonly Texture2D texture;

        public MapCell(CellTypes cellType, Rectangle rectangle)
        {
            CellType = cellType;
            texture = GameEngine.GetTextureByCellType(CellType);
            Rectangle = rectangle;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }

        public override bool Update(GameTime gameTime) => false;
    }
}