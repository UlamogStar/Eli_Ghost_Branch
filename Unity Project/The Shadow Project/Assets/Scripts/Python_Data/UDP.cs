using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDP : MonoBehaviour
{
     Thread receiveThread;
        UdpClient client; 
        [Tooltip("This this the port the data will be received on form the python script make sure the are the same.")]
        public int port = 5052;
        [Tooltip("When True will get position data from python when false will not receive data from python.")]
        public bool startRecieving = true;
        [Tooltip("When True will print the data to the console.")]
        public bool printToConsole = false;
        [Tooltip("This will show all incoming data from python.")] 
        public string data;
    
        
    [TextArea(3, 10)] public string UDP_Hands_notes =
            "This script will receive data from a python script that is sending data over UDP. " +
            "The data will be received on the port that is set in the port variable. " +
            "The data will be stored in the data variable. " +
            "The data can be printed to the console by setting the printToConsole variable to true. " +
            "The startRecieving variable can be set to false to stop receiving data. " +
            "The data can be accessed by other scripts by using the data variable ";
        [TextArea(3, 10)] public string note =
            "The default camera can only be changed in the python hand tracking script.";
        
        void Start()
        {
            StartReceiver();
        }
        void StartReceiver()
        {
            if (receiveThread != null)
            {
                StopReceiver();
            }

            receiveThread = new Thread(ReceiveData);
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        
        // receive thread
        private void ReceiveData()
        {
    
            client = new UdpClient(port);
            while (startRecieving)
            {
    
                try
                {
                    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] dataByte = client.Receive(ref anyIP);
                    data = Encoding.UTF8.GetString(dataByte);
    
                    if (printToConsole) { print(data); }
                }
                catch (Exception err)
                {
                    print(err.ToString());
                }
            }
        }
        
        public void StopReceiver()
        {
            startRecieving = false;

            if (client != null)
            {
                client.Close(); // <-- CRITICAL: closes socket so scene reload can reopen it
                client = null;
            }

            if (receiveThread != null)
            {
                receiveThread.Abort();
                receiveThread = null;
            }
        }

        void OnDestroy()
        {
            StopReceiver();
        }

        void OnApplicationQuit()
        {
            StopReceiver();
        }
    

       
}