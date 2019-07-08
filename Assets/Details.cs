using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using LitJson;
using System;

public class Details : MonoBehaviour
{
    // Start is called before the first frame update
    public Ticket ticket;
    public Client client;
    public GameObject description;
    public GameObject title;
    public GameObject list;
    public GameObject members;

    void Start()
    {
        Debug.Log("SSSSSSSSSSSSSSSSsssssssssss: " + client.apiKey);
        Debug.Log("ticket desde el detalle " + ticket.id);
        string url = "https://api.trello.com/1/cards/" + ticket.id + "?key=" + client.apiKey + "&token=" + client.userToken;

        Debug.Log(url);
        
        WWW www = new WWW(url);

        while(www.isDone != true) {}

        if (www.error == null)
        {
            JsonData jsonvale = JsonMapper.ToObject(www.text);
            
            description = this.transform.GetChild(3).gameObject.transform.Find("Description").gameObject;
            title = this.transform.GetChild(3).gameObject.transform.Find("Title").gameObject;
            Debug.Log("despues");
            if(description != null)
            {
                description.GetComponent<Text>().text = jsonvale["desc"].ToString();
            }
            if(title != null)
            {
                title.GetComponent<Text>().text = jsonvale["name"].ToString();
            }

            url = "https://api.trello.com/1/lists/" + jsonvale["idList"].ToString() + 
            "?key=" + client.apiKey + "&token=" + client.userToken;
            
            www = new WWW(url);
            while(www.isDone != true) {}
            if (www.error == null)
            {
                JsonData jsonList = JsonMapper.ToObject(www.text);
                list = this.transform.GetChild(3).gameObject.transform.Find("List").gameObject;
                if(list != null)
                {
                    list.GetComponent<Text>().text = jsonList["name"].ToString();
                }
            }

            string strMembers = "";
            for (int i = 0; i < jsonvale["idMembers"].Count; i++)
            {
                url = "https://api.trello.com/1/members/" + jsonvale["idMembers"][i].ToString() + "?key=" + client.apiKey + "&token=" + client.userToken;       
                www = new WWW(url);
                while(www.isDone != true) {}
                if(www.error == null)
                {
                    JsonData jsonMember = JsonMapper.ToObject(www.text);
                    strMembers = jsonMember["fullName"].ToString() + " ";
                }
            }
            members = this.transform.GetChild(3).gameObject.transform.Find("Members").gameObject;
            if(members != null)
            {
                members.GetComponent<Text>().text = strMembers;
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        } 

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
