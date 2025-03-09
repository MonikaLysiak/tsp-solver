class RandomGeneration {

    public static Individual? best = null;

    public static Individual[] createRandomGeneration(int populationSize, int depth) {

        Individual[] population = new Individual[populationSize];

        for(int i = 0; i < populationSize; i++) {

            Individual indiv = new Individual(depth);
            population[i] = indiv;

            if(best == null || indiv.Fitness > best.Fitness )
                best = indiv;
        }

        return population;
    }
}