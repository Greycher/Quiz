namespace ColorTilesHop.Runtime.Signals
{
    public readonly struct QuizSessionEndSignal
    {
        public readonly int Score;

        public QuizSessionEndSignal(int score)
        {
            Score = score;
        }
    }
}