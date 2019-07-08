using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEdit : MonoBehaviour
{
    // Start is called before the first frame update
    public EditTicket edit;

    void Start()
    {
        
    }

    void OnMouseDown()
    {
        EditTicket instance = Instantiate(edit, new Vector3(transform.position.x+0.5f, transform.position.y-0.2f, 5), transform.rotation) as EditTicket;
        instance.ticket  = transform.parent.gameObject.GetComponent<Menu>().ticket;
        instance.client = transform.parent.gameObject.GetComponent<Menu>().client;
        Destroy(transform.parent.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
