class MainGP
{
    private Individual[] population;
    private Individual[] newPopulation;
    private Individual currentBest;
    private Random random = new Random();
    private Parameters parameters = new Parameters();

    public MainGP(string dataFile, Parameters parameters)
    {
        this.parameters = parameters;

        DataReader dataReader = new DataReader(dataFile);

        FitnessHelper.DistanceMatrix = dataReader.DistanceMatrix;
        population = RandomGeneration.CreateRandomGeneration(parameters.POPULATION_SIZE, dataReader.NumberOfCities);
        currentBest = RandomGeneration.best;

        newPopulation = new Individual[parameters.POPULATION_SIZE];
    }

    public (List<int> BestRoute, double BestDistance) Evolve() {

        for(int genNumber = 0; genNumber < parameters.MAX_GENERATIONS; genNumber ++) {
            for(int indivNumber = 0; indivNumber < parameters.POPULATION_SIZE - 1; indivNumber ++) {

                if(random.NextDouble() <= parameters.CROSS_OVER_PROBABILITY && indivNumber < parameters.POPULATION_SIZE - 2) {
                    
                    Individual? firstParent = null;
                    Individual? secondParent = null;

                    if(parameters.TOURNAMENT_METHOD == Parameters.TournamentMethod.BEST_RANDOM) {

                        firstParent = Choice.BestRandomTournament(parameters.TOURNAMENT_SIZE, population);
                        secondParent = Choice.BestRandomTournament(parameters.TOURNAMENT_SIZE, population);
                    }
                    if(parameters.TOURNAMENT_METHOD == Parameters.TournamentMethod.ROULETEE) {

                        firstParent = Choice.RouletteTournament(parameters.TOURNAMENT_SIZE, population);
                        secondParent = Choice.RouletteTournament(parameters.TOURNAMENT_SIZE, population);
                    }

                    Individual[]? children = null;

                    if(parameters.CROSSOVER_METHOD == Parameters.CrossoverMethod.ONE_POINT)
                        children = ChromosomesOperations.New_onePointCrossover(firstParent, secondParent);

                    if(parameters.CROSSOVER_METHOD == Parameters.CrossoverMethod.TWO_POINT)
                        children = ChromosomesOperations.New_twoPointCrossover(firstParent, secondParent);

                    if(children[0].Fitness < currentBest.Fitness) 
                        currentBest = children[0];

                    if(children[1].Fitness < currentBest.Fitness)
                        currentBest = children[1];

                    newPopulation[indivNumber] = children[0];
                    indivNumber ++;
                    newPopulation[indivNumber] = children[1];
                }
                else {

                    Individual? individual = null;

                    if (parameters.TOURNAMENT_METHOD == Parameters.TournamentMethod.BEST_RANDOM)
                        individual = Choice.BestRandomTournament(parameters.TOURNAMENT_SIZE, population);

                    if (parameters.TOURNAMENT_METHOD == Parameters.TournamentMethod.ROULETEE)
                        individual = Choice.RouletteTournament(parameters.TOURNAMENT_SIZE, population);

                    Individual mutatedIndividual = ChromosomesOperations.Mutation(individual, parameters.CHANCE_OF_NODE_MUTATING);

                    if(mutatedIndividual.Fitness < currentBest.Fitness)
                        currentBest = mutatedIndividual;

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
        return (currentBest.GetRoute(), currentBest.Fitness);
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
