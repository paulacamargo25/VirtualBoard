using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{
    #region Public Variables
    [Header("Network")]
    public string ipAddress = "192.168.43.82";
    public int port = 54010;
    // The id we use to identify our messages and register the handler
    short messageID = 1000;

    [Header("UI References")]
    public Button sendCloseButton;
    public InputField inputField; 
    public Text ClientLogger = null;

    #endregion

    NetworkClient client;
    public Material mat;

    //Set UI interactable properties
    private void Start()
    {
        CreateClient();
        sendCloseButton.interactable = false;
    }
    
    //Start client and stablish connection with server
    public void StartClient()
    {     
         CreateClient();
    }

    void CreateClient()
    {
        var config = new ConnectionConfig ();
        Debug.Log("Create Client");

        // Config the Channels we will use
        config.AddChannel (QosType.ReliableFragmented);
        config.AddChannel (QosType.UnreliableFragmented);

        // Create the client ant attach the configuration
        client = new NetworkClient();
        client.Configure(config,1);

        // Register the handlers for the different network messages
        RegisterHandlers();

        // Connect to the server
        Debug.Log(ipAddress + ":" + port);
        client.Connect(ipAddress, port);
    }

    void OnError(NetworkMessage netMsg)
    {
        // var errorMsg = netMsg.ReadMessage<ErrorMessage>();
        Debug.Log("Error:" + netMsg);
    }

    void RegisterHandlers () {
        // Unity have different Messages types defined in MsgType
        client.RegisterHandler (messageID, OnMessageReceived);
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
    }

    void OnConnected(NetworkMessage message) {        
        // Do stuff when connected to the server
        Debug.Log("onConnected");
        MyNetworkMessage messageContainer = new MyNetworkMessage();
        messageContainer.message = "Hello server!";
        Debug.Log("Client connected");
        Debug.Log("Send Message to server");

        // Say hi to the server when connected
        client.Send(messageID,messageContainer);
    }

    void OnDisconnected(NetworkMessage message) {
        // Do stuff when disconnected to the server
    }

    void OnMessageReceived(NetworkMessage netMessage)
    {
        // You can send any object that inherence from MessageBase
        // The client and server can be on different projects, as long as the MyNetworkMessage or the class you are using have the same implementation on both projects
        // The first thing we do is deserialize the message to our custom type
        var objectMessage = netMessage.ReadMessage<MyNetworkMessage>();
        Debug.Log("Received Message" + objectMessage.message);

        Debug.Log("Message received: " + objectMessage.message);

        string name_object = objectMessage.message.Split(':')[1];
        GameObject objectSelect = GameObject.Find(name_object);
        Debug.Log(objectSelect);
        objectSelect.GetComponent<Renderer>().material = mat;
    }

    //Check if the client has been recived something
    private void Update()
    {
    }


    public void SendMsgToServer() 
    {
        string sendMsg = inputField.text;
        MyNetworkMessage messageContainer = new MyNetworkMessage();
        messageContainer.message = sendMsg;
        Debug.Log("Send Message to server:" + sendMsg);

        // Say hi to the server when connected
        client.Send(messageID,messageContainer);
    }

    public void SendMsgToServer(string msg)
    {
        string sendMsg = msg;
        MyNetworkMessage messageContainer = new MyNetworkMessage();
        messageContainer.message = sendMsg;
        Debug.Log("Send Message to server:" + sendMsg);

        // Say hi to the server when connected
        client.Send(messageID, messageContainer);
    }

    //Close client connection
    private void CloseClient()
    {
    }


}
