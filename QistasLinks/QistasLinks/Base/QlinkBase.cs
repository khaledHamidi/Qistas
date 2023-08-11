//using Qistas.QException;

using System.IO.Ports;

namespace Qistas.QistasLinks.Base;

public class QlinkBase : IQlinkBase
{



    #region Connection
    protected SerialPort linkInterface { get; set; }


    public int rate;
    public string port;




    public QlinkBase(int rate = 9600, string port = "Auto")
    {
        this.rate = rate;
        this.port = port;

        linkInterface = new SerialPort();
    }





    //  public QException exception { get; set; }













    public int GetRate() => rate;
    public int SetRate(int value) => rate = value;




    public string GetPort() => port;
    public void SetPort(string value) => port = value;
    public static string[] GetPorts => SerialPort.GetPortNames();

    public string GetStatus()
    {
        if (isOpen())
            return $"  connection state ( open ) , via {port} with rate {rate} bits per second";
        else
        {
            return $"  connection state ( close )";

        }
    }


    public virtual void DataReceived(object sender, SerialDataReceivedEventArgs e) { }





    #region Connection & Init



    public bool Init(Parity parity = Parity.None, int DataBit = 8, StopBits stopBits = StopBits.One, Handshake handshake = Handshake.XOnXOff, int ReadTimeout = 100000, int WriteTimeout = 100000)
    {
        try
        {
            rate = rate > 0 ? rate : 115200;
            port = "Auto" != port ? port : GetPorts.Last();
            linkInterface = new SerialPort(port, rate, parity, DataBit, stopBits);
            linkInterface.Handshake = handshake;


            linkInterface.ReadTimeout = ReadTimeout;
            linkInterface.WriteTimeout = WriteTimeout;


            linkInterface.DataReceived += new SerialDataReceivedEventHandler(DataReceived);

            return true;
        }

        catch
        {
            /*        exception = new QException(ex.Message, ExceptionLevel.Error, ex.Source, "Link to Interface failed");*/
            return false;
        }
    }




    public bool open()
    {
        if (linkInterface == null) return false;
        if (linkInterface.IsOpen) return true;
        linkInterface.Open();
        return linkInterface.IsOpen;
    }


    public bool isOpen()
    {
        return linkInterface.IsOpen;
    }

    public void Close()
    {
        linkInterface.Close();
    }

    #endregion



    #endregion



    #region Send


    public bool Send(string text)
    {
        try
        {
            linkInterface.Write(text);

            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
    public bool SendLine(string text)
    {
        linkInterface.WriteLine(text);
        return true;

    }



    #endregion






}
