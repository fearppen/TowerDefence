using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerDefence.Managers
{
    public static class MouseManager
    {
        public static MouseState CurrentMouse;
        public static MouseState PreviousMouse;
        public static Rectangle MouseRectangle { get; private set; }

        public static void Update()
        {
            PreviousMouse = CurrentMouse;
            CurrentMouse = Mouse.GetState();
            MouseRectangle = new Rectangle(CurrentMouse.X, CurrentMouse.Y, 1, 1);
        }

        public static bool IsClicked()
        {
            if (CurrentMouse.LeftButton == ButtonState.Released && PreviousMouse.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
    }
}
