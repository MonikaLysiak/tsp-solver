class ChromosomesOperations {

    public static Random random = new Random();
    public static Individual[] OnePointCrossover(Individual firstParent, Individual secondParent) {

        int totalDepth = firstParent.Cities.Length;

        int cutPoint = random.Next(totalDepth + 1);
        List<int> firstParentFreeNodes = firstParent.Cities.ToList();
        List<int> secondParentFreeNodes = secondParent.Cities.ToList();

        int[] newCities1 = new int[totalDepth];
        int[] newCities2 = new int[totalDepth];

        // copy the first array
        Array.Copy(firstParent.Cities, 0, newCities1, 0, cutPoint);
        
        // delete used nodes from free nodes
        for(int i = 0; i < cutPoint; i++)
            secondParentFreeNodes.Remove(firstParent.Cities[i]);

        // copy the second array
        for(int i = cutPoint; i < totalDepth; i++)
            newCities1[i] = secondParentFreeNodes[i - cutPoint];

        // the same for the secend child :
        // ------

        Array.Copy(secondParent.Cities, 0, newCities2, 0, cutPoint);
        
        for(int i = 0; i < cutPoint; i++)
            firstParentFreeNodes.Remove(secondParent.Cities[i]);

        for(int i = cutPoint; i < totalDepth; i++)
            newCities2[i] = firstParentFreeNodes[i - cutPoint];

        // -----

        Individual offspring1 = new Individual(newCities1);
        Individual offspring2 = new Individual(newCities2);

        Individual[] children = {offspring1, offspring2};

        return children;
    }

    public static Individual[] New_onePointCrossover(Individual firstParent, Individual secondParent)
    {

        int totalDepth = firstParent.Cities.Length;

        int cutPoint = random.Next(totalDepth + 1);
        List<int> firstParentFreeNodes = new List<int>();
        List<int> secondParentFreeNodes = new List<int>();

        int[] newCities1 = new int[totalDepth];
        int[] newCities2 = new int[totalDepth];

        // copy the first array
        Array.Copy(firstParent.Cities, 0, newCities1, 0, cutPoint);

        //check if there was copied city 0, becouse in arrays there are zeros in empty spaces
        bool hasCity0 = newCities1.Where(x => x == 0).Count() != totalDepth - cutPoint;

        // delete used nodes from free nodes

        for (int i = cutPoint; i < totalDepth; i++)
            if (!newCities1.Contains(secondParent.Cities[i]) || (!hasCity0 && secondParent.Cities[i] == 0))
                secondParentFreeNodes.Add(secondParent.Cities[i]);

        for (int i = 0; i < cutPoint; i++)
            if (!newCities1.Contains(secondParent.Cities[i]) || (!hasCity0 && secondParent.Cities[i] == 0))
                secondParentFreeNodes.Add(secondParent.Cities[i]);

        // copy the second array
        for (int i = cutPoint; i < totalDepth; i++)
            newCities1[i] = secondParentFreeNodes[i - cutPoint];

        // the same for the secend child :
        // ------

        Array.Copy(secondParent.Cities, 0, newCities2, 0, cutPoint);

        hasCity0 = newCities2.Where(x => x == 0).Count() != totalDepth - cutPoint;

        for (int i = cutPoint; i < totalDepth; i++)
            if (!newCities2.Contains(firstParent.Cities[i]) || (!hasCity0 && firstParent.Cities[i] == 0))
                firstParentFreeNodes.Add(firstParent.Cities[i]);

        for (int i = 0; i < cutPoint; i++)
            if (!newCities2.Contains(firstParent.Cities[i]) || (!hasCity0 && firstParent.Cities[i] == 0))
                firstParentFreeNodes.Add(firstParent.Cities[i]);

        for (int i = cutPoint; i < totalDepth; i++)
            newCities2[i] = firstParentFreeNodes[i - cutPoint];

        // -----

        Individual offspring1 = new(newCities1);
        Individual offspring2 = new(newCities2);

        Individual[] children = [offspring1, offspring2];

        return children;
    }

    public static Individual[] TwoPointCrossover(Individual firstParent, Individual secondParent) {

        int totalDepth = firstParent.Cities.Length;

        int firstCutPoint = random.Next(totalDepth);
        int secondCutPoint = random.Next(firstCutPoint + 1, totalDepth + 1);


        List<int> firstParentFreeNodes = firstParent.Cities.ToList();
        List<int> secondParentFreeNodes = secondParent.Cities.ToList();

        int[] newCities1 = new int[totalDepth];
        int[] newCities2 = new int[totalDepth];

        // copy the first array
        Array.Copy(firstParent.Cities, 0, newCities1, 0, firstCutPoint);
        Array.Copy(firstParent.Cities, secondCutPoint, newCities1, secondCutPoint, totalDepth - secondCutPoint);

        // add only unused nodes to free nodes
        for (int i = 0; i < firstCutPoint; i++)
            secondParentFreeNodes.Remove(firstParent.Cities[i]);

        for(int i = secondCutPoint; i < totalDepth; i++)
            secondParentFreeNodes.Remove(firstParent.Cities[i]);

        //copy the second array
         for(int i = firstCutPoint; i < secondCutPoint; i++)
            newCities1[i] = secondParentFreeNodes[i - firstCutPoint];

        
        // the same for the second child :
        // -----

        Array.Copy(secondParent.Cities, 0, newCities2, 0, firstCutPoint);
        Array.Copy(secondParent.Cities, secondCutPoint, newCities2, secondCutPoint, totalDepth - secondCutPoint);

        for(int i = 0; i < firstCutPoint; i++)
            firstParentFreeNodes.Remove(secondParent.Cities[i]);

        for(int i = secondCutPoint; i < totalDepth; i++)
            firstParentFreeNodes.Remove(secondParent.Cities[i]);

         for(int i = firstCutPoint; i < secondCutPoint; i++)
            newCities2[i] = firstParentFreeNodes[i - firstCutPoint];

        // -----

        Individual offspring1 = new Individual(newCities1);
        Individual offspring2 = new Individual(newCities2);

        Individual[] children = {offspring1, offspring2};

        return children;
    }

    public static Individual[] New_twoPointCrossover(Individual firstParent, Individual secondParent)
    {

        int totalDepth = firstParent.Cities.Length;

        int firstCutPoint = random.Next(totalDepth);
        int secondCutPoint = random.Next(firstCutPoint + 1, totalDepth + 1);


        List<int> firstParentFreeNodes = new List<int>();
        List<int> secondParentFreeNodes = new List<int>();

        int[] newCities1 = new int[totalDepth];
        int[] newCities2 = new int[totalDepth];

        // copy the first array
        Array.Copy(firstParent.Cities, firstCutPoint, newCities1, firstCutPoint, secondCutPoint - firstCutPoint);

        //check if there was copied city 0, becouse in arrays there are zeros in empty spaces
        bool hasCity0 = newCities1.Where(x => x == 0).Count() != totalDepth - (secondCutPoint - firstCutPoint);

        // add only unused nodes to free nodes
        for (int i = secondCutPoint; i < totalDepth; i++)
            if(!newCities1.Contains(secondParent.Cities[i]) || (!hasCity0 && secondParent.Cities[i] == 0))
                secondParentFreeNodes.Add(secondParent.Cities[i]);

        for (int i = 0; i < secondCutPoint; i++)
            if (!newCities1.Contains(secondParent.Cities[i]) || (!hasCity0 && secondParent.Cities[i] == 0))
                secondParentFreeNodes.Add(secondParent.Cities[i]);

        //copy the second array
        for (int i = secondCutPoint; i < totalDepth; i++)
            newCities1[i] = secondParentFreeNodes[i - secondCutPoint];

        for (int i = 0; i < firstCutPoint; i++)
            newCities1[i] = secondParentFreeNodes[i + (totalDepth - secondCutPoint)];


        // the same for the second child :
        // -----

        Array.Copy(secondParent.Cities, firstCutPoint, newCities2, firstCutPoint, secondCutPoint - firstCutPoint);

        hasCity0 = newCities2.Where(x => x == 0).Count() != totalDepth - (secondCutPoint - firstCutPoint);

        for (int i = secondCutPoint; i < totalDepth; i++)
            if (!newCities2.Contains(firstParent.Cities[i]) || (!hasCity0 && firstParent.Cities[i] == 0))
                firstParentFreeNodes.Add(firstParent.Cities[i]);

        for (int i = 0; i < secondCutPoint; i++)
            if (!newCities2.Contains(firstParent.Cities[i]) || (!hasCity0 && firstParent.Cities[i] == 0))
                firstParentFreeNodes.Add(firstParent.Cities[i]);

        for (int i = secondCutPoint; i < totalDepth; i++)
            newCities2[i] = firstParentFreeNodes[i - secondCutPoint];

        for (int i = 0; i < firstCutPoint; i++)
            newCities2[i] = firstParentFreeNodes[i + (totalDepth - secondCutPoint)];

        // -----

        Individual offspring1 = new Individual(newCities1);
        Individual offspring2 = new Individual(newCities2);

        Individual[] children = { offspring1, offspring2 }; //

        return children;
    }

    public static Individual Mutation(Individual parent, double chanceOfNodeMutating) {

        int totalDepth = parent.Cities.Length;

        int[] newCities = new int[totalDepth];
        List<int> parentCopy = parent.Cities.ToList();

        for(int i = 0; i < totalDepth; i++) {
            
            if(random.NextDouble() <= chanceOfNodeMutating) {
                
                int randomIndex = random.Next(totalDepth - i);
                newCities[i] = parentCopy[randomIndex];

                parentCopy.RemoveAt(randomIndex);
            }
            else {

                newCities[i] = parentCopy[0];
                parentCopy.RemoveAt(0);            
            }
        }

        Individual offspring = new Individual(newCities);

        return offspring;
    }
}