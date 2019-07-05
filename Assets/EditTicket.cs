using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditTicket : MonoBehaviour
{
    // Start is called before the first frame update

    public Ticket ticket;
    public InputField title;
    public InputField detail;

    void Start()
    {
        Debug.Log("Ticket Edit" + ticket);
        title.text = ticket.name;
    }

    public void onSaved(){
        Debug.Log("Saved");
        Debug.Log("Title: "+title.text);
        Debug.Log("Detail: "+detail.text);
        Destroy(this.gameObject);
    }

    public void onCancel(){
        Debug.Log("Cancel");
        Destroy(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
