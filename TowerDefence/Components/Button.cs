using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static TowerDefence.Managers.MouseManager;

namespace TowerDefence.Components
{
    public class Button : Component
    {
        private SpriteFont font;
        private bool isHovering;
        private Texture2D texture;

        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }
        public string Text { get; set; }

        public Button(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = Color.White;

            if (isHovering)
            {
                color = Color.Gray;
            }

            spriteBatch.Draw(texture, Rectangle, color);

            if (!string.IsNullOrEmpty(Text))
            {
                var xCoordinate = Rectangle.X + Rectangle.Width / 2 - font.MeasureString(Text).X / 2;
                var yCoordinate = Rectangle.Y + Rectangle.Height / 2 - font.MeasureString(Text).Y / 2;

                spriteBatch.DrawString(font, Text, new Vector2(xCoordinate, yCoordinate), Color.Black);
            }
        }

        public override bool Update(GameTime gameTime)
        {
            isHovering = false;

            if (MouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;
                if (IsClicked())
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }

            return Clicked;
        }

        public void ClickButton()
        {
            Clicked = true;
        }
    }
}
