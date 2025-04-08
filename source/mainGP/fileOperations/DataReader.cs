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

    public DataReader(Stream dataFile) {

        if (dataFile != null && dataFile.Length > 0) {

            string header;
            string? line;

            using (var reader = new StreamReader(dataFile))
            {
                header = reader.ReadLine() ?? throw new Exception("There is no header in your file");
            
                SetDepth(header);

                while ((line = reader.ReadLine()) != null) {
                    string[] values = line.Split(";");
                    SetDistancesForOneCity(values);
                } 
            }
        } 
        else {
            throw new Exception("Provide appriopriate file name");
        }
    }

    private void SetDepth(string header) {

        string[] arrayOfCities = header.Split(";");

        numberOfCities = Int32.Parse(arrayOfCities[arrayOfCities.Length - 1]);

        distanceMatrix = new double[numberOfCities, numberOfCities];
    }

    private void SetDistancesForOneCity(string[] values) {
        
        int city = Int32.Parse(values[0]);

        for(int i = 1; i < values.Length; i++) {
            
            string distance = values[i];

            if(distance == "")
                distance = "0";

            distanceMatrix[city - 1, i - 1] = Double.Parse(distance);
        }
    }

    public void DisplayMatrix() {

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