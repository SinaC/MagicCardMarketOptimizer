namespace MagicCardMarket.App
{
    public static class Parameters
    {
        public static readonly bool FilterOutSpecial = true;
        public static readonly bool UseHigherIdHeuristics = true;
        public static readonly int HigherIdHeuristicsCount = 15;
        public static readonly bool UseTrendPriceHeuristics = true;
        public static readonly double TrendPriceHeuristicsPercentage = 0.20; // trend price higher than average + 20% are filtered out
    }
}
