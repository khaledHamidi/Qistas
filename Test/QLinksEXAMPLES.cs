
using Qistas;
using Qistas.QAnalyzer;
using Qistas.QistasLinks;
using System.Dynamic;
using static Qistas.QFunctions;

namespace QExamples
{
    public class QLinksEXAMPLES
    {

        enum msg { router = 0, internte = 1, idk = -1 }
        public static void Example1()
        {


            Qlink link = new Qlink
            {
                port = "COM5",
                rate = 115200,


                receiveMode = Qlink.ReceiveMode.UseBufferLogOnlyAvailable,
                Buffer = 30,

                linksPatterns = QLinksPattern.Create(

                               QLinksPattern.Between("<", ">", (int)msg.router),
                                 QLinksPattern.EndsWith("\n", (int)msg.internte)
                               ),
            };
            link.Init();
            link.open();

            Echo(link.GetStatus());

            link.SerializerAdd(< pattren >,< id >)

            // get data directery if no using Serializer.
            link.JunkData += Link_JunkData;
            link.Received += Link_Received;

            void Link_Received(string DataReceived, int patternId)
            {
                Echo($"by applying ({patternId}) >> received: {DataReceived}");
            }

            void Link_JunkData(string JunkData)
            {
                Echo("Junk: " + JunkData);

            }


            link.SerializerInit()
                   .AddPattern("#code# #key#:##values##", id: 1)
                   .AddPattern("#code# #key#:#value#", id: 2)

                   .AddPattern("#operation# ##type##", id: 3)
                   .AddPattern("##temps##", id: 4);


            link.Deserialized += Link_Deserialized;

            void Link_Deserialized(dynamic data)
            {

                Echo($"Deserialized data as pattern ({data.pattrenId}).");


            }

            dynamic unknown = new ExpandoObject();
            unknown.patternId = 1;
            unknown.id = 4121;
            unknown.code = "SET";
            unknown.key = "D3";
            unknown.value = "OFF";

            switch ((int)unknown.patternId)
            {
                case (int)models.DefualtModel:
                    {
                        var obj = DefualtModel.Create(unknown);
                        Echo(obj.id);
                        break;
                    }

            }

            while (true)
            {

            }

        }
    }
}
