using System.Diagnostics;

public class MainGP
{
    private Individual[] population;
    private Individual[] newPopulation;
    private Individual currentBest;
    private Random random = new Random();
    private Parameters parameters = new Parameters();

    // to be fixed in the future - not supposed to be static
    private static CancellationTokenSource cts = new CancellationTokenSource();
    
    private const int HARDCODED_TIMEOUT_SECONDS = 600;

    public MainGP(string dataFile, Parameters parameters)
    {
        this.parameters = parameters;
        cts = new CancellationTokenSource();

        DataReader dataReader = new DataReader(dataFile);

        FitnessHelper.DistanceMatrix = dataReader.DistanceMatrix;
        population = RandomGeneration.CreateRandomGeneration(parameters.POPULATION_SIZE, dataReader.NumberOfCities);
        currentBest = RandomGeneration.best;

        newPopulation = new Individual[parameters.POPULATION_SIZE];
    }

    // to be fixed in the future - not supposed to be static - stops all running processes ?
    public static void StopEvolution()
    {
        cts.Cancel();
    }

    public (List<int> BestRoute, double BestDistance) Evolve(int maxDurationSeconds = HARDCODED_TIMEOUT_SECONDS) 
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        int totalMaxTime = Math.Min(maxDurationSeconds, HARDCODED_TIMEOUT_SECONDS);

        try
        {
            for (int genNumber = 0; genNumber < parameters.MAX_GENERATIONS; genNumber++)
            {
                if (cts.Token.IsCancellationRequested || stopwatch.Elapsed.TotalSeconds > totalMaxTime)
                {
                    Console.WriteLine($"Stopping at generation {genNumber}. Best fitness so far: {currentBest.Fitness}");
                    break;
                }

                for(int indivNumber = 0; indivNumber < parameters.POPULATION_SIZE - 1; indivNumber ++)
                {
                    if(random.NextDouble() <= parameters.CROSS_OVER_PROBABILITY && indivNumber < parameters.POPULATION_SIZE - 2) {
                        
                        var firstParent = SelectParent();
                        var secondParent = SelectParent();

                        var children = PerformCrossover(firstParent, secondParent);

                        SetCurrentBest(children);

                        newPopulation[indivNumber] = children[0];

                        indivNumber ++;
                        newPopulation[indivNumber] = children[1];
                    }
                    else {

                        var individual = SelectParent();

                        var mutatedIndividual = ChromosomesOperations.Mutation(individual, parameters.CHANCE_OF_NODE_MUTATING);

                        SetCurrentBest([mutatedIndividual]);

                        newPopulation[indivNumber] = mutatedIndividual;
                    }
                }

                newPopulation[parameters.POPULATION_SIZE - 1] = currentBest;
                // Console.WriteLine("Generation: " + genNumber);
                // Console.WriteLine("The best fitness: " + currentBest.Fitness);
                // Console.WriteLine("The best order of cities: ");

                // Console.WriteLine();
                UpdatePopulation();
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Evolution process was manually stopped.");
        }
        
        return (currentBest.GetRoute(), currentBest.Fitness);
    }

    private Individual SelectParent()
    {
        return parameters.TOURNAMENT_METHOD switch
        {
            Parameters.TournamentMethod.BEST_RANDOM => Choice.BestRandomTournament(parameters.TOURNAMENT_SIZE, population),
            Parameters.TournamentMethod.ROULETEE => Choice.RouletteTournament(parameters.TOURNAMENT_SIZE, population),
            _ => throw new Exception("Unknown tournament method"),
        };
    }

    private Individual[] PerformCrossover(Individual parent1, Individual parent2)
    {
        return parameters.CROSSOVER_METHOD switch
        {
            Parameters.CrossoverMethod.ONE_POINT => ChromosomesOperations.New_onePointCrossover(parent1, parent2),
            Parameters.CrossoverMethod.TWO_POINT => ChromosomesOperations.New_twoPointCrossover(parent1, parent2),
            _ => throw new Exception("Unknown crossover method"),
        };
    }

    private void SetCurrentBest(Individual[] children)
    {
        foreach (var child in children)
        {
            if (child.Fitness < currentBest.Fitness)
                currentBest = child;
        }
    }

    private void UpdatePopulation()
    {
        // code to display all children 

        // for(int indivNumber = 0; indivNumber < parameters.POPULATION_SIZE; indivNumber ++)
        //     population[indivNumber].display();

        // Console.WriteLine();

        // for(int indivNumber = 0; indivNumber < parameters.POPULATION_SIZE; indivNumber ++)
        //     newPopulation[indivNumber].display();

        // Console.WriteLine();

        for (int indivNumber = 0; indivNumber < parameters.POPULATION_SIZE; indivNumber++)
            population[indivNumber] = newPopulation[indivNumber];
    }
}
