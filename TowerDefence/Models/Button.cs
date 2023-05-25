using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TowerDefence.AbstractClasses;
using static TowerDefence.StaticClasses.MouseManager;

namespace TowerDefence.Models
{
    public class Button : UIComponent
    {
        private bool isHovering;
        private readonly SpriteFont font;
        private readonly Texture2D texture;

        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Rectangle Rectangle
        {
            get => new((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }
        public string Text { get; set; }
        private Vector2 Position { get; set; }

        public Button(Texture2D texture, SpriteFont font, Vector2 position)
        {
            this.texture = texture;
            this.font = font;
            Position = position;
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
