namespace TowerDefence
{
    public enum GameStates
    {
        Paused,
        Playing,
    }

    public static class GameStats
    {
        public static int Gold;
        public static int Healths;
        public static GameStates CurrentState;
    }
}
