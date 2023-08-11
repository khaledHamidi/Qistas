using System.IO.Ports;

namespace Qistas.QistasLinks.Base
{
    public interface IQlinkBase
    {
        public int GetRate();
        public int SetRate(int value);


        public string GetPort();
        public void SetPort(string value);




        public bool Init(Parity parity = Parity.None, int DataBit = 8, StopBits stopBits = StopBits.One, Handshake handshake = Handshake.XOnXOff, int ReadTimeout = 100000, int WriteTimeout = 100000);



        public bool open();
        public void Close();

        public bool isOpen();
        void DataReceived(object sender, SerialDataReceivedEventArgs e);

        public enum Ports { COM0, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, COM10, Auto = -1 }

    }
}
