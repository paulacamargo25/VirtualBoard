using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;


public class Ticket : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    public string id;
    [Header("UI References")]
    public Board board;
    public Material baseMat;
    public Client client;
    public Menu menu;
    public bool canEdit;

    float max;

    // Start is called before the first frame update
    void Start()
    {
        baseMat = GetComponent<Renderer>().material;
        canEdit = true;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateName(){
        string url = "https://api.trello.com/1/cards/" + this.id + "?key=" + client.apiKey + "&token=" + client.userToken;
        Debug.Log(url);
        WWW www = new WWW(url);
        while(www.isDone != true) {}
        if (www.error == null)
        {
            JsonData jsonvale = JsonMapper.ToObject(www.text);            
            Debug.Log("despues");
            Debug.Log(jsonvale["name"].ToString());
            transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = jsonvale["name"].ToString();

        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        } 

    }

    void OnMouseDown()
    {
        if (canEdit){
            Debug.Log(name);
            Debug.Log(client.myColor);
            client.SendMsgToServer("S|" + this.name+'|'+client.myColor[0]+'|'+client.myColor[1]+'|'+client.myColor[2]);
        }
    }

    void OnMouseUp()
    { 
        if (canEdit){
            Debug.Log("========= UUPPPP ======");
            client.SendMsgToServer("R|" + this.name+'|'+transform.position.x+'|'+transform.position.y+'|'+transform.position.z);
        }
    }


    void OnMouseOver() 
    {
        if (canEdit){
            if(Input.GetMouseButtonDown(1))
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5.075f);
                Vector3 STWP = Camera.main.ScreenToWorldPoint(curScreenPoint);
                STWP.z = 4.9f;
                Debug.Log(transform.position);

                Menu instance = Menu.Instantiate(menu, new Vector3(transform.position.x+0.9f,transform.position.y, 5), transform.rotation) as Menu;
                instance.ticket  = this;
                instance.client = client;
                Debug.Log("Right click on this object");
            }    
        }
    }

    void OnMouseDrag()
    {  
        if (canEdit){
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5.075f);
            Vector3 STWP = Camera.main.ScreenToWorldPoint(curScreenPoint);     
            STWP.z = 5.075f;

            int j = 0;
            max = board.width;
            for (int i = 0; i < board.numLists; i++)
            {
                if(Mathf.Abs(board.xList[i] - STWP.x) < max)
                {
                    max = Mathf.Abs(board.xList[i] - STWP.x);
                    j = i;
                }
            }
            STWP.x = board.xList[j];
    
            if(STWP.y > 2.1f)
            {
                STWP.y = 2.1f;
            } 
            else if(STWP.y < 1.3f) {
                STWP.y = 1.3f;
            }
            transform.position = STWP;
        }
    }
}
