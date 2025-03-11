public class Individual
{
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
    
    // Constructor for random initialization
    public Individual(int numCities)
    {
        this.cities = GenerateRandomRoute(numCities);
        FitnessHelper.SetFitnessOfIndividual(this);
    }

    // Constructor for predefined city order
    public Individual(int[] cities)
    {
        this.cities = (int[])cities.Clone();
        FitnessHelper.SetFitnessOfIndividual(this);
    }

    // Copy constructor
    public Individual(Individual other)
    {
        this.cities = (int[])other.cities.Clone();
        this.fitness = other.fitness;
    }

    // Generate a random permutation of cities
    private static int[] GenerateRandomRoute(int numCities)
    {
        List<int> availableCities = Enumerable.Range(0, numCities).ToList();
        int[] route = new int[numCities];

        for (int i = 0; i < numCities; i++)
        {
            int index = random.Next(availableCities.Count);
            route[i] = availableCities[index];
            availableCities.RemoveAt(index);
        }

        return route;
    }

    // Return the route as a list
    public List<int> GetRoute()
    {
        return cities.ToList();
    }

    // Override ToString instead of using display()
    public override string ToString()
    {
        return string.Join(" -> ", cities.Select(c => c + 1)) + $" | Fitness: {fitness}";
    }
}
