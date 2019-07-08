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
        Debug.Log("Saved");
        Debug.Log("Title: "+title.text);
        Debug.Log("Detail: "+detail.text);
        StartCoroutine(Upload());
        //Upload();
        Destroy(this.gameObject);
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "caiisac");
        form.AddField("desc", "eweverv");
        form.AddField("key", "ee88b5af8d4961bf5373a3fece25d638");
        form.AddField("token", "b280cd1388f45f72bdc4e50246985a7e05706ae7761858d1b7c5325f3ee4c080");

        Debug.Log(form.data);
        string url = "https://api.trello.com/1/card/5d1e6f048235a12a3bd1f9a5?&key=ee88b5af8d4961bf5373a3fece25d638&token=b280cd1388f45f72bdc4e50246985a7e05706ae7761858d1b7c5325f3ee4c080";
        string url = "https://api.trello.com/1/card/"+ticket.id+"?name=NombreePaula&desc=DescCaif&key="+client.apiKey+"&token="+client.userToken;
        
        byte[] myData = System.Text.Encoding.UTF8.GetBytes("{name:Carif, desc:DescCairrf, key:ee88b5af8d4961bf5373a3fece25d638, token:b280cd1388f45f72bdc4e50246985a7e05706ae7761858d1b7c5325f3ee4c080}");
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("X-HTTP-Method-Override", "PUT");
        
        UnityWebRequest www = UnityWebRequest.Put(url, form.data);
        //WWW www = new WWW(url, form.data, headers);

            Debug.Log(" First");
            yield return www.Send();
            Debug.Log("===========ddewwe");

//                    while(www.isDone != true) {}

                                Debug.Log("===========done");


            
     //       if (www.isNetworkError || www.isHttpError)
       //     {
         //       Debug.Log("EEEEEEEEEERO" + www.error);
           // }
            //else
            //{
            //    Debug.Log("Upload complete!");
            //}
        
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
