namespace TowerDefence.StaticClasses
{
    public enum GameStates
    {
        Intermediate,
        Playing,
        Paused,
    }

    public static class GameStats
    {
        public static int Gold;
        public static int Healths;
        public static GameStates CurrentState;
    }
}
