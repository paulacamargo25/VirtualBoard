using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Clooseee");
        Destroy(transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
