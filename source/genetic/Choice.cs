class Choice {

    private static Random random = new Random();

    public static Individual BestRandomTournament(int tournamentSize, Individual[] population) {

        Individual bestIndividual = GetRandomIndividual(population);

        for(int i = 1; i < tournamentSize; i++) {

            Individual individual = GetRandomIndividual(population);

            if(individual.Fitness < bestIndividual.Fitness)
                bestIndividual = individual;
        }

        return bestIndividual;
    }

    public static Individual RouletteTournament(int tournamentSize, Individual[] population) {

        Individual[] individuals = new Individual[tournamentSize];
        double[] winPointsArray = new double[tournamentSize];

        Individual worstIndividual = GetRandomIndividual(population);
        individuals[0] = worstIndividual;

        for(int i = 1; i < tournamentSize; i++) {

            Individual individual = GetRandomIndividual(population);
            individuals[i] = individual;

            if(individual.Fitness > worstIndividual.Fitness)
                worstIndividual = individual;
        }

        double sumAllPoints = 0;
        for (int i = 0; i < tournamentSize; i++) {

            double winPoints = worstIndividual.Fitness - individuals[i].Fitness;

            winPointsArray[i] = winPoints + sumAllPoints;
            sumAllPoints += winPoints;
        }

        double chosenNumber = random.NextDouble() * sumAllPoints;
        Individual selectedIndividual = worstIndividual;

        for(int i = 0; i < tournamentSize; i++) {

            if(chosenNumber < winPointsArray[i] ) {

                selectedIndividual = individuals[i];
                return selectedIndividual;
            }
            else
                chosenNumber -= winPointsArray[i];

        }
        return selectedIndividual;
    }

    private static Individual GetRandomIndividual(Individual[] population) {

            int populationSize = population.Length;

            int randomIndex = random.Next(populationSize);
            return population[randomIndex];
    }
}