using QExamples;
using QExamples.Common;

namespace Qistas
{

    public class Program
    {
        private static void Main(string[] args)
        {
            FunctionsExamples.EchoExample1();
            // QSerializeTEST();
            QSerializeEXAMPLES.Init();
            QSerializeEXAMPLES.Example1();
            QSerializeEXAMPLES.Example2();
            QSerializeEXAMPLES.Example3();
            QSerializeEXAMPLES.Example4();
            QSerializeEXAMPLES.Example5();
            QSerializeEXAMPLES.Example6();
            //

            // Qlink
            QLinksEXAMPLES.Example1();


        }






    }

    public class DefualtModel
    {

        //  unknown.var1 = "val";
        //   unknown.var2 = "va2";
        public readonly int patternsId = 1;
        public int id { get; set; }
        public string code { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public static DefualtModel Create(dynamic row)
        {

            return new DefualtModel()
            {
                id = row.id,
                code = row.code ?? null,
                key = row.key ?? null,

            };

        }

    }
    enum models { DefualtModel = 1, idk_ }
}