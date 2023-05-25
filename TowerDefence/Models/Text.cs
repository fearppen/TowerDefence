using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.AbstractClasses;

namespace TowerDefence.Models
{
    public class Text : UIComponent
    {
        public string mainText;
        public string result;
        private readonly SpriteFont font;
        private readonly Vector2 position;
        private readonly Color color;

        public Text(SpriteFont font, string text, Vector2 position, Color color, string additionalInfo = null)
        {
            this.font = font;
            this.position = position;
            this.color = color;
            mainText = text;
            result = additionalInfo == null ? text : string.Format("{0}{1}", text, additionalInfo);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, result, position, color);
        }

        public bool Update(GameTime gameTime, string newInfo)
        {
            result = string.Format("{0}{1}", mainText, newInfo);
            return false;
        }

        public override bool Update(GameTime gameTime) => false;
    }
}
