using System;

namespace br.com.arthurjf.shapewar.Managers
{
    public static class ScoreModel
    {
        public static event Action<int> OnScoreChanged;

        private static int score = 0;

        // ENCAPSULATION
        public static int Score
        {
            get => score;
            set
            {
                score = value;

                OnScoreChanged?.Invoke(score);
            }
        }
    }
}
