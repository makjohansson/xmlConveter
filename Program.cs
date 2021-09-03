namespace xmlConveter
{
    class Program
    {
        /**
        * Main program for converting rowbased fileformat file to a xml file.
        */
        static void Main(string[] args)
        {
            // Defult path and out put file
            string xmlOutputFileName = "people.xml";
            string textFilePath = "people.txt";

            switch(args.Length)
            {
                case 1:
                    textFilePath = args[0];
                    break;
                case 2:
                    textFilePath = args[0];
                    xmlOutputFileName = args[1];
                    break;
            }
            new ConvertToXml(textFilePath, xmlOutputFileName);
        }
    }
}


