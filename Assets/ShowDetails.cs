using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDetails : MonoBehaviour
{
    
    public Details details;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnMouseDown()
    {
        Details instance = Instantiate(details, new Vector3(transform.position.x+0.5f, transform.position.y-0.5f, 5), transform.rotation) as Details;
        instance.ticket  = transform.parent.gameObject.GetComponent<Menu>().ticket;
        Destroy(transform.parent.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
