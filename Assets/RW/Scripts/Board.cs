using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int numLists;
    public float width;
    public float[] xList;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start!!!!!");
        this.numLists = 4;
        width = 7;
        xList =  new float[numLists]; 

        for (int i = 0; i < numLists; i++)
        {
            xList[i] = 1.913f + (width/numLists)*(i+1)-(width/numLists)/2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
