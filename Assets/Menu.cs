using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public Ticket ticket;
    public Client client;

    void Start()
    {
        Debug.Log("SSSSSSSSSSSSSSSSs: " + client.apiKey);
        
        Debug.Log("MEnu ticket: " + ticket);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
