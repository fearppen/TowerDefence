namespace TowerDefence
{
    public enum GameStates
    {
        Paused,
        Playing,
    }

    public static class GameStats
    {
        public static int Gold = 50;
        public static int Healths = 10;
        public static GameStates CurrentState = GameStates.Playing;
    }
}
