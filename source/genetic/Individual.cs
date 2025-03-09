class Individual {

    private int[] cities;
    private double fitness;

    private static Random random = new Random();

    public int[] Cities
    {
        get => cities;
    }

    public double Fitness
    {
        get => fitness;
        set => fitness = value;
    }

    public Individual(int depth) {

        this.cities = getRandomCities(depth); 
        FitnessHelper.setFitnessOfIndividual(this);
    }

    public Individual(int[] cities) {

        this.cities = cities;
        FitnessHelper.setFitnessOfIndividual(this);
    }

    private int[] getRandomCities(int depth) {

        int[] newCities = new int[depth];
        List<int> allPossibleCities = new();

        for(int i = 0; i < depth; i++)
            allPossibleCities.Add(i);

        for(int i = 0; i < depth; i++) {

            int randomIndex = random.Next(depth - i);
            newCities[i] = allPossibleCities[randomIndex];

            allPossibleCities.RemoveAt(randomIndex);
        }

        return newCities;
    }

    public void display() {
        
        for(int i = 0; i < cities.Length; i++) {
            
            Console.Write(cities[i] + 1);

            if(i < cities.Length - 1) {
                Console.Write(", ");
            }
            
        }

        Console.WriteLine();
    }
}