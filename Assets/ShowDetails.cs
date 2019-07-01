using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDetails : MonoBehaviour
{
    
    public GameObject details;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        Instantiate(details, new Vector3(transform.position.x+0.5f, transform.position.y-0.5f, 5), transform.rotation);
        Debug.Log("crear deta");
        Destroy(transform.parent.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
