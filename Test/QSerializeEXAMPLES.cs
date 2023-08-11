using Qistas.QAnalyzer;
using static Qistas.QFunctions;
namespace QExamples
{
    internal class QSerializeEXAMPLES
    {
        static QSerializer serializer = new QSerializer();

        public static void Init()
        {
            serializer = serializer = new QSerializer();

            serializer.AddPattern("GET #mode#:##values##", id: 1);
            serializer.AddPattern("#code# #key#:#value#", id: 2);
            serializer.AddPattern("GET :##pins##", id: 3);
            serializer.AddPattern("#opration# ##values##", id: 4);
            serializer.AddPattern("##values##", id: 5);

        }








        public static void Example1()
        {
            var ExampleRawData = "REQ ERROR:404";

            dynamic deserialized1 = serializer.Deserialize(ExampleRawData);

            Echo($"Example1:    Deserialize \"{ExampleRawData}\"");
            Echo($"----------");

            Echo("Pattern ID: " + deserialized1.patternsId);
            Echo($"Code: {deserialized1.code}");
            Echo($"Key: {deserialized1.key}");
            Echo($"Value: {deserialized1.value}");
            Echo("", 1);
        }





        public static void Example2()
        {
            var ExampleRawData = "36.2,36.0,37.4,36.8";
            dynamic deserialized = serializer.Deserialize(ExampleRawData);
            Echo("Pattern ID: " + deserialized.patternsId);
            Echo("Values:");
            Echo(string.Join(',', deserialized.values));
            Echo();

        }
        public static void Example3()
        {
            var ExampleRawData = "GET A5,D2,D1,D9";

            dynamic deserialized = serializer.Deserialize(ExampleRawData);

            Echo($"Example3:    Deserialize \"{ExampleRawData}\"");
            Echo($"----------");

            Echo("Pattern ID: " + deserialized.patternsId);
            Echo("Values:");
            Echo(string.Join(',', deserialized.values));
            Echo();

        }
        public static void Example4()
        {

            QSerializer serializer1 = new QSerializer();
            serializer1.AddPattern("#name#,#age#", id: 1);
            dynamic model = new System.Dynamic.ExpandoObject();
            model.name = "Khalid";
            model.age = "90";
            string serialized = serializer1.Serialize(model, 1);


            Echo($"Example4:    Serializing model as \"#name#,#age#\"");
            Echo($"----------");

            Echo(serialized);
            Echo();
        }
        public static void Example5()
        {

            dynamic model2 = new System.Dynamic.ExpandoObject();
            model2.code = "WRITE";
            model2.mode = "D7";
            model2.values = new string[] { "255", "0", "255" };
            string ser = serializer.Serialize(model2, 2);

            Echo($"Example5:    Serializing model as pattern ({2})");
            Echo($"----------");

            Echo(ser);
            Echo();


        }
        public static void Example6()
        {

            // Example 6: 

            var ExampleRawData = "1.5,2.7,3.9";

            dynamic deserialized = serializer.Deserialize(ExampleRawData);


            Echo($"Example5:    deserialized \"{ExampleRawData}\"");
            Echo($"----------");

            Echo("Values:");
            Echo(string.Join(',', deserialized.values));


        }
    }
}


