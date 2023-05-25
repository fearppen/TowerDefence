using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using TowerDefence.Models;

namespace TowerDefence.Controllers
{
    public class AnimationController
    {
        public Animation CurrentAnimation { get; private set; }
        private Dictionary<string, Animation> animations = new();

        public void AddAnimation(string animationTitle, Animation animation)
        {
            CurrentAnimation ??= animation;
            animations[animationTitle] = animation;
        }

        public void ChangeAnimation(string animationTitle)
        {
            if (animations.ContainsKey(animationTitle))
            {
                CurrentAnimation = animations[animationTitle];
            }
        }

        public void StopAnimations()
        {
            CurrentAnimation.StopAnimation();
        }

        public void Update(GameTime gameTime)
        {
            CurrentAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle position, Color color)
        {
            CurrentAnimation.Draw(spriteBatch, position, color);
        }
    }
}
