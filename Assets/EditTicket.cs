using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System;
using UnityEngine.Networking;

public class EditTicket : MonoBehaviour
{
    // Start is called before the first frame update

    public Ticket ticket;
    public InputField title;
    public InputField detail;
    public Client client;

    void Start()
    {
        getTicketData();
    }

    void getTicketData() 
    {
        string url = "https://api.trello.com/1/cards/" + ticket.id + "?key=" + client.apiKey + "&token=" + client.userToken;
        WWW www = new WWW(url);
        while(www.isDone != true) {}
        if (www.error == null)
        {
            JsonData jsonvale = JsonMapper.ToObject(www.text);

            title.text = jsonvale["name"].ToString();
            detail.text = jsonvale["desc"].ToString();
        }
    }

    public void onSaved(){
        StartCoroutine(Upload());
        System.Threading.Thread.Sleep(1000);
        client.SendMsgToServer("U|" + "Ticket-"+ticket.id);
        Destroy(this.gameObject);
    }

    IEnumerator Upload()
    {
        string url = "https://api.trello.com/1/card/"+ticket.id+"?name="+title.text+"&desc="+detail.text+"&key="+client.apiKey+"&token="+client.userToken;
        Debug.Log("url: "+url);
        UnityWebRequest www = UnityWebRequest.Put(url, "dummy");
        yield return www.Send();
        Debug.Log("========EEnd======");
        if (www.isNetworkError || www.isHttpError)
         {
             Debug.Log(www.error);
         }
         else
         {
             Debug.Log("Success");
         }
    }

    public void onCancel(){
        Destroy(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
