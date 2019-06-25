﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    [Header("UI References")]
    public Board board;
    float max;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    { 
    }

    void OnMouseDrag()
    {  
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5.075f);
        Vector3 STWP = Camera.main.ScreenToWorldPoint(curScreenPoint);
        
        Debug.Log("x " + STWP.x);
        Debug.Log("y " + STWP.y);
        Debug.Log("z " + STWP.z);        
        
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
 
        Debug.Log("init " + STWP.y.ToString());
        if(STWP.y > 2.1f)
        {
            STWP.y = 2.1f;
        } 
        else if(STWP.y < 1.3f) {
            STWP.y = 1.3f;
        }
        Debug.Log("end " + STWP.y.ToString());

        transform.position = STWP;
    }
}
