namespace SomniumSpace.Runtime.Scripts.Services.Networks
{
    internal static class PlayerEnumerableExtensions
    {
        internal static int Count(this System.Collections.Generic.IEnumerable<Fusion.PlayerRef> source)
        {
            int n = 0;
            foreach (var _ in source) n++;
            return n;
        }
    }
}