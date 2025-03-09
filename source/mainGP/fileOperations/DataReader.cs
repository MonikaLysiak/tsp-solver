class DataReader {

    private double[,] distanceMatrix;
    private int numberOfCities;


    public int NumberOfCities 
    {
        get => numberOfCities;
    }

    public double[,] DistanceMatrix
    {
        get => distanceMatrix;
    }

    public DataReader(string dataFile) {

        if (File.Exists(dataFile)) { 
            
            StreamReader Textfile = new StreamReader(dataFile); 

            string? header = Textfile.ReadLine(); 

            if(header == null)
                throw new Exception("There is no header in your file");  

            setDepth(header);

            string? line; 
            while ((line = Textfile.ReadLine()) != null) { 
                
                string[] values = line.Split(";");
                setDistancesForOneCity(values);
            } 
  
            Textfile.Close(); 
        } 
        else {
            throw new Exception("Provide appriopriate file name");
        }
    }

    private void setDepth(string header) {

        string[] arrayOfCities = header.Split(";");

        numberOfCities = Int32.Parse(arrayOfCities[arrayOfCities.Length - 1]);

        distanceMatrix = new double[numberOfCities, numberOfCities];
    }

    private void setDistancesForOneCity(string[] values) {
        
        int city = Int32.Parse(values[0]);

        for(int i = 1; i < values.Length; i++) {
            
            string distance = values[i];

            if(distance == "")
                distance = "0";

            distanceMatrix[city - 1, i - 1] = Double.Parse(distance);
        }
    }

    public void displayMatrix() {

        int depth = distanceMatrix.GetLength(0);

        for(int i = 0; i < depth; i++) {

            Console.Write(i + ": ");

            for(int j = 0; j < depth; j++) {

                Console.Write(distanceMatrix[i, j] + ";");
            }

            Console.WriteLine();
        }
    }
}