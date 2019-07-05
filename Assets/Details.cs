using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Details : MonoBehaviour
{
    // Start is called before the first frame update
    public Ticket ticket;
    void Start()
    {
        Debug.Log("ticket Detail" + ticket);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnMouseDown()
    {
        Destroy(this.gameObject);
    }
}
