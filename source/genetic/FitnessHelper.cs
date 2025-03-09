class FitnessHelper {

    private static double[,] distanceMatrix;

    public static double[,] DistanceMatrix
    {
        set => distanceMatrix = value;
    }


    public static void setFitnessOfIndividual(Individual individual) {

        int totalDepth = individual.Cities.Length;

        double fitness = 0;

        for(int i = 1; i < totalDepth; i++) {

            int firstCity = individual.Cities[i - 1];
            int secondCity = individual.Cities[i];

            double distance = distanceMatrix[firstCity, secondCity];

            fitness += distance;
        }

        fitness += distanceMatrix[individual.Cities[0], individual.Cities[totalDepth - 1]];

        individual.Fitness = fitness;
    }
}