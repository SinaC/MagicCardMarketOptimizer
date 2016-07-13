namespace MagicCardMarket.App
{
    public static class Parameters
    {
        public static bool FilterOutSpecial = true;
        public static bool FilterOutBasicLand = true;
        public static bool UseHigherIdHeuristic = true;
        public static int HigherIdHeuristicCount = 15;
        public static bool UseTrendPriceHeuristic = false; // TODO: probably a wrong heuristic  on VampireNighthawk (metaproduct id 10738 we're losing product 22100 expansion gateway promos)
        public static double TrendPriceHeuristicPercentage = 0.20; // trend price higher than trend average
    }
}
