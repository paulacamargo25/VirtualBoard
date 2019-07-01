using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTicket : MonoBehaviour
{
    public Client client;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Click");
        int id = Random.Range(1,10000);
        client.SendMsgToServer("A|" + "Ticket "+ id);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
