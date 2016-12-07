using UnityEngine;
using System.IO.Ports;
using System.Threading;

//http://tips.hecomi.com/entry/2014/07/28/023525
public class SerialHandler : MonoBehaviour
{

    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    public string portName = "COM6";
    public int baudRate = 9600;

    private SerialPort _serialPort;
    private Thread _thread;
    private bool _isRunning = false;

    private string _message;
    private bool _isNewMessageReceived = false;

    void Awake()
    {
        Open();
    }

    void Update()
    {
        if (_isNewMessageReceived)
        {
            OnDataReceived(_message);
        }
    }

    void OnDestroy()
    {
        Close();
    }

    private void Open()
    {
        _serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        _serialPort.Open();

        _isRunning = true;

        _thread = new Thread(Read);
        _thread.Start();
    }

    private void Close()
    {
        _isRunning = false;

        if (_thread != null && _thread.IsAlive)
        {
            _thread.Join();
        }

        if (_serialPort != null && _serialPort.IsOpen)
        {
            _serialPort.Close();
            _serialPort.Dispose();
        }
    }

    private void Read()
    {
        while (_isRunning && _serialPort != null && _serialPort.IsOpen)
        {
            try
            {
                _message = _serialPort.ReadLine();
                _isNewMessageReceived = true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    public void Write(int t)
    {
        try
        {
            byte[] values = new byte[1];
            values[0] = (byte)t;
            _serialPort.Write(values, 0, 1);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
