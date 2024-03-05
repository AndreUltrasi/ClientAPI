using System.Runtime.CompilerServices;

namespace Core.Extensions
{
    public static class Helpers
    {
        public static string GetCallerName([CallerMemberName] string? caller = null)
        {
            return caller!;
        }
    }
}
