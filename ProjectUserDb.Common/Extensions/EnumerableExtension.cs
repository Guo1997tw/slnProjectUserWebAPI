namespace ProjectUser.Common.Extensions
{
    public static class EnumerableExtension
    {
        public static bool BeEmpty<T>(this IEnumerable<T> source)
        {
            return source.Any().Equals(false);
        }
    }
}