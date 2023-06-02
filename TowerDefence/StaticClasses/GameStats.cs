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
        public static int Wave;
        public static GameStates CurrentState;

        public static void CreateGameStats()
        {
            Healths = Constants.Healths;
            Gold = Constants.Gold;
            Wave = 1;
            CurrentState = GameStates.Playing;
        }
    }
}
