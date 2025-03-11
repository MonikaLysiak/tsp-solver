class Parameters {
    // main parameters
    public int POPULATION_SIZE { get; set; } = 80_000;
    public int TOURNAMENT_SIZE { get; set; } = 70;
    public int MAX_GENERATIONS { get; set; } = 100;

    // probability parameters
    public double CHANCE_OF_NODE_MUTATING { get; set; } = 0.05;
    public double CROSS_OVER_PROBABILITY { get; set; } = 0.9;

    // selections method - parameters
    public TournamentMethod TOURNAMENT_METHOD { get; set; } = TournamentMethod.BEST_RANDOM;
    public CrossoverMethod CROSSOVER_METHOD { get; set; } = CrossoverMethod.TWO_POINT;

    public enum TournamentMethod
    {
        BEST_RANDOM, 
        ROULETEE
    }

    public enum CrossoverMethod
    {
        ONE_POINT, 
        TWO_POINT
    }

    // constructor to allow dynamic initialization
    public Parameters(int populationSize, int tournamentSize, int maxGenerations, double mutationChance, double crossoverProbability, TournamentMethod tournamentMethod, CrossoverMethod crossoverMethod)
    {
        POPULATION_SIZE = populationSize;
        TOURNAMENT_SIZE = tournamentSize;
        MAX_GENERATIONS = maxGenerations;
        CHANCE_OF_NODE_MUTATING = mutationChance;
        CROSS_OVER_PROBABILITY = crossoverProbability;
        TOURNAMENT_METHOD = tournamentMethod;
        CROSSOVER_METHOD = crossoverMethod;
    }

    // cefault constructor
    public Parameters() { }
}
