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
    public string userToken;
    public string apiKey;
    public string ipAddress = "192.168.43.82";
    public int port = 54010;
    // The id we use to identify our messages and register the handler
    short messageID = 1000;

    [Header("UI References")]
    public Text ClientLogger = null;

    #endregion
    public Color myColor;

    NetworkClient client;
    public Material selectedMat;
    public Ticket ticketPrefab;
    public Board board;
    public GameObject menu;

    //Set UI interactable properties
    private void Start()
    {
        userToken = "b280cd1388f45f72bdc4e50246985a7e05706ae7761858d1b7c5325f3ee4c080";
        apiKey = "ee88b5af8d4961bf5373a3fece25d638";
        CreateClient();
        myColor = new Color(
            UnityEngine.Random.Range(0f, 1f), 
            UnityEngine.Random.Range(0f, 1f), 
            UnityEngine.Random.Range(0f, 1f)
        );
        Debug.Log("Colooor: "+ myColor.ToString());
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

        Debug.Log("Message received: " + objectMessage.message);
        var values = objectMessage.message.Split('|');
        GameObject objectSelect;
        Ticket ticket;
        if (values.Length > 1){
            switch (values[0])
            {
                case "S":
                    Debug.Log("Case S");
                    string color = values[2];
                    objectSelect = GameObject.Find(values[1]);
                    ticket = objectSelect.GetComponent<Ticket>();
                    Debug.Log(ticket);
                    selectedMat.color = new Color(float.Parse(values[2]),float.Parse(values[3]), float.Parse(values[4]));
                    objectSelect.GetComponent<Renderer>().material = selectedMat;
                    Debug.Log("=======coloor===");
                    Debug.Log(myColor);
                    Debug.Log(selectedMat.color);
                    if (myColor == selectedMat.color){
                        Debug.Log("==mi ticket");
                    } else {
                        ticket.canEdit = false;
                        Debug.Log("==other ticket");
                    }
                    break;
                case "R":
                    Debug.Log("Case R"); 
                    objectSelect = GameObject.Find(values[1]);
                    ticket = objectSelect.GetComponent<Ticket>();
                    ticket.GetComponent<Renderer>().material = ticket.baseMat;
                    Debug.Log(values[2] + values[3] + values[4]);
                    float x = float.Parse(values[2]);
                    float y = float.Parse(values[3]);    
                    float z = float.Parse(values[4]);
                    ticket.transform.position = new Vector3(x, y, z);
                    ticket.canEdit = true;
                    break;
                case "A":
                    Ticket instance = Ticket.Instantiate(ticketPrefab, new Vector3(2.788f, 2.1f, 5f), Quaternion.identity) as Ticket;
                    instance.client = this;
                    instance.board = board;
                    instance.menu = menu;
                    instance.name = values[1];
                    break;
                default:
                    Debug.Log("Case Other");
                    break;
            }
        }
    }

    //Check if the client has been recived something
    private void Update()
    {
    }


    // public void SendMsgToServer() 
    // {
    //     string sendMsg = inputField.text;
    //     MyNetworkMessage messageContainer = new MyNetworkMessage();
    //     messageContainer.message = sendMsg;
    //     Debug.Log("Send Message to server:" + sendMsg);

    //     // Say hi to the server when connected
    //     client.Send(messageID,messageContainer);
    // }

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
