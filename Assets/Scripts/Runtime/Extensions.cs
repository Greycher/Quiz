using System;

namespace QuizGame.Runtime
{
    public static class Extensions
    {
        public static string TrimAndRemoveNewLines(this string s)
        {
            return s.Trim().Replace("\n", String.Empty);
        }
    }
}