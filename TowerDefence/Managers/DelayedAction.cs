using Microsoft.Xna.Framework;
using System;

namespace TowerDefence.Managers
{
    public class DelayedAction
    {
        private readonly Action action;
        private float time;

        public DelayedAction(Action action, float delay)
        {
            this.action = action;
            time = delay;
        }

        public bool Update(GameTime gameTime)
        {
            time -= gameTime.ElapsedGameTime.Milliseconds;
            if (time <= 0)
            {
                action();
                return true;
            }
            return false;
        }
    }
}
