namespace Programmers.Utils
{
    public static class RandomHelper
    {
        static Random random = new Random();

        /// <summary>
        /// @percentage 확률로 TRUE를 반환합니다.
        /// </summary>
        public static bool Get(int percentage = 50)
        {
            return random.Next(100) < percentage;
        }

        public static int Get(int min, int max)
        {
            return random.Next(min, max);
        }

        public static T Get<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));

            return (T)values.GetValue(random.Next(values.Length));
        }
    }
}
