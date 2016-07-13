namespace MagicCardMarket.App
{
    public static class Parameters
    {
        public static readonly bool FilterOutSpecial = true;
        public static readonly bool FilterOutBasicLand = true;
        public static readonly bool UseHigherIdHeuristic = true;
        public static readonly int HigherIdHeuristicCount = 15;
        public static readonly bool UseTrendPriceHeuristic = false; // TODO: probably a wrong heuristic  on VampireNighthawk (metaproduct id 10738 we're losing product 22100 expansion gateway promos)
        public static readonly double TrendPriceHeuristicPercentage = 0.20; // trend price higher than trend average
    }
}
