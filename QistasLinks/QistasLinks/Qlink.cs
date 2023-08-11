using Qistas.QAnalyzer;
using Qistas.QistasLinks.Base;
using System.IO.Ports;

namespace Qistas.QistasLinks
{
    public class Qlink : QlinkBase
    {


        QSerializer? serializer;
        public QSerializer SerializerInit()
        {
            UseSerializer = true;
            serializer = new QSerializer();
            return serializer;
        }
        public void SerializerAdd(string Pattern, int id)
        {
            serializer?.AddPattern(Pattern, id);
        }
        public List<QLinksPattern>? linksPatterns { get; set; }
        public int Buffer { get; set; } = 1000;
        public ReceiveMode receiveMode { get; set; } = ReceiveMode.Byline;
        public enum ReceiveMode
        {
            Byline,
            OnlyAvailable,
            UseBuffer,
            UseBufferLogOnlyAvailable,
        }


        public void SetPatterns(List<QLinksPattern> patterns)
        {
            linksPatterns = patterns;
        }


        #region data in 

        string _data = "";
        private string incomingData = "";
        //     private string _value;


        public override void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            if (linkInterface.IsOpen)
            {
                if (receiveMode == ReceiveMode.Byline)
                    incomingData = linkInterface.ReadLine();

                else
                {
                    _data = linkInterface.ReadExisting();

                    if (receiveMode == ReceiveMode.UseBuffer || receiveMode == ReceiveMode.UseBufferLogOnlyAvailable)
                    {
                        incomingData += _data;
                        if (incomingData.Length > Buffer)
                        {
                            incomingData = incomingData.Substring(incomingData.Length - Buffer);
                        }
                    }
                    else if (receiveMode == ReceiveMode.OnlyAvailable)
                        incomingData = _data;

                }







                var id = QLinksPattern.TryParse(ref incomingData, linksPatterns);

                if (id == -1)
                    if (receiveMode == ReceiveMode.UseBufferLogOnlyAvailable)
                        JunkData(_data);
                    else
                        JunkData(incomingData);
                else
                {
                    try
                    {

                        if (UseSerializer)
                            DeserializedCallBack(serializer?.Deserialize(incomingData));
                    }
                    catch
                    {


                    }

                    receivedCallBack(incomingData, id);
                    incomingData = "";
                }

            }
        }


        #endregion



        public bool UseSerializer { get; set; } = false;





        public void JunkDataCallBack(string _JunkData)
        {
            if (JunkData != null)
                JunkData(_JunkData);
        }
        public delegate void JunkDataEventHandler(string JunkData);
        public event JunkDataEventHandler? JunkData;


        public void receivedCallBack(string DataReceived, int patternId)
        {
            if (Received != null)
                Received(DataReceived, patternId);
        }
        public delegate void dataReceivedEventHandler(string DataReceived, int patternId);
        public event dataReceivedEventHandler? Received;


        public void DeserializedCallBack(dynamic data)
        {
            if (Deserialized != null)
                Deserialized(data);
        }
        public delegate void DeserializedCallBackEventHandler(dynamic data);
        public event DeserializedCallBackEventHandler? Deserialized;


    }
}
