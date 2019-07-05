using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Board : MonoBehaviour
{
    public int numLists;
    public float width;
    public float[] xList;
    public Dictionary<string, string>[] listData;
    public string id;
    public Client client;
    public GameObject listTitle;
    public GameObject menu;
    public Ticket ticketPrefab;

    // Start is called before the first frame update
    void Start()
    {
        id = "5ce32b3a2e381e060704ea8f";
        calculateNumList();

        width = 7;
        xList =  new float[numLists]; 

        for (int i = 0; i < numLists; i++)
        {
            xList[i] = 1.913f + (width/numLists)*(i+1)-(width/numLists)/2;
            var obj = Instantiate(listTitle, new Vector3(xList[i]-0.3f, 2.5f, 5f), transform.rotation);
            obj.transform.parent = this.transform;
            obj.GetComponent<TextMesh>().text = listData[i]["name"];

            createTickets(listData[i]["id"], xList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createTickets(string listId, float x)
    {
        string url = "https://api.trello.com/1/lists/" + listId + "/cards?key=" + client.apiKey + "&token=" + client.userToken;
        WWW www = new WWW(url);

        while(www.isDone != true) {}

        if (www.error == null)
        {
            JsonData jsonvale = JsonMapper.ToObject(www.text);

            for(int i=0; i < jsonvale.Count; i++)
            {
                Ticket instance = Ticket.Instantiate(ticketPrefab, new Vector3(x, 2.1f-0.2f*i, 5f), Quaternion.identity) as Ticket;
                instance.client = client;
                instance.board = this;
                instance.menu = menu;
                instance.name = "Ticket-" + jsonvale[i]["id"].ToString();
                instance.id = jsonvale[i]["id"].ToString();
                instance.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = jsonvale[i]["name"].ToString();
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }        
    }

    void calculateNumList()
    {
        string url = "https://api.trello.com/1/boards/" + id + "/lists?key=" + client.apiKey + "&token=" + client.userToken;
        WWW www = new WWW(url);
        
        while(www.isDone != true) {}

        if (www.error == null)
        {
            JsonData jsonvale = JsonMapper.ToObject(www.text);
            numLists = jsonvale.Count;

            listData = new Dictionary<string, string>[numLists];
            for(int i=0; i < numLists; i++)
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("name", jsonvale[i]["name"].ToString());
                data.Add("id", jsonvale[i]["id"].ToString());
                listData[i] = data;
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    }
}
