using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    #region Public Variables
    [Header("Network variables")]
    int maxConnections = 10;
    // The id we use to identify our messages and register the handler
    short messageID = 1000;
    public Button startServerButton;
    public InputField port; 
    public Text ServerLogger = null;
    #endregion

    //Set UI interactable properties
    private void Start()
    {
        startServerButton.interactable = true;  //Enable button to let users start the server
    }

    public void StartServer(){
        // Register handlers for the types of messages we can receive
        
        RegisterHandlers ();
        var currentPort = Int32.Parse(port.text);
        var config = new ConnectionConfig ();
        // There are different types of channels you can use, check the official documentation
        config.AddChannel (QosType.ReliableFragmented);
        config.AddChannel (QosType.UnreliableFragmented);

        var ht = new HostTopology (config, maxConnections);

        if (!NetworkServer.Configure (ht)) {
            Debug.Log ("No server created, error on the configuration definition");
            return;
        } else {
            // Start listening on the defined port
            if(NetworkServer.Listen(currentPort)){
                ServerLog("Server created, listening on port: " + currentPort, Color.green);
                Debug.Log ("Server created, listening on port: " + currentPort);   
            } 
            else
                Debug.Log ("No server created, could not listen to the port: " + currentPort);    
        }
    }

    private void RegisterHandlers () {
        // Unity have different Messages types defined in MsgType
        NetworkServer.RegisterHandler (MsgType.Connect, OnClientConnected);
        NetworkServer.RegisterHandler (MsgType.Disconnect, OnClientDisconnected);

        // Our message use his own message type.
        NetworkServer.RegisterHandler (messageID, OnMessageReceived);
    }


    private void RegisterHandler(short t, NetworkMessageDelegate handler) {
        NetworkServer.RegisterHandler (t, handler);
    }

    //Check if any client trys to connect
    private void Update()
    {   
    }    

    void OnClientConnected(NetworkMessage netMessage)
    {
        // Do stuff when a client connects to this server

        // Send a thank you message to the client that just connected
        MyNetworkMessage messageContainer = new MyNetworkMessage();
        messageContainer.message = "Thanks for joining!";

        // This sends a message to a specific client, using the connectionId
        NetworkServer.SendToClient(netMessage.conn.connectionId,messageID,messageContainer);
        ServerLog("Send message to clien: Thanks for joining!", Color.green);

        // Send a message to all the clients connected
        messageContainer = new MyNetworkMessage();
        messageContainer.message = "A new player has conencted to the server";

        // Broadcast a message a to everyone connected
        NetworkServer.SendToAll(messageID,messageContainer);
    }

    void OnClientDisconnected(NetworkMessage netMessage)
    {
        // Do stuff when a client dissconnects
    }

    void OnMessageReceived(NetworkMessage netMessage)
    {
        // You can send any object that inherence from MessageBase
        // The client and server can be on different projects, as long as the MyNetworkMessage or the class you are using have the same implementation on both projects
        // The first thing we do is deserialize the message to our custom type
        var objectMessage = netMessage.ReadMessage<MyNetworkMessage>();
        ServerLog("Message received: " + objectMessage.message);
        Debug.Log("Message received: " + objectMessage.message);

         //Send to all the message that i received
        MyNetworkMessage messageContainer = new MyNetworkMessage();
        messageContainer.message = objectMessage.message;
        ServerLog("Sent to all the message: " + objectMessage.message);
        NetworkServer.SendToAll(messageID, messageContainer);
    }

    //Custom Server Log
    #region ServerLog
    //With Text Color
    private void ServerLog(string msg, Color color)
    {
        ServerLogger.text += '\n' + "<color=#"+ColorUtility.ToHtmlStringRGBA(color)+">- " + msg + "</color>";
        Debug.Log("Server: " + msg);
    }
    //Without Text Color
    private void ServerLog(string msg)
    {
        ServerLogger.text += '\n' + "- " + msg;
        Debug.Log("Server: " + msg);
    }
    #endregion

}