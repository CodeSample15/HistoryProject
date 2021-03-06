﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour
{
    public GameObject infoBoxObject;
    public GameObject canvas;
    public bool infoShowing;

    void Start()
    {
        infoBoxObject.transform.position = new Vector2(10, 10);
    }

    public void Show_Markers()
    {
        int infoboxes = 8; //IMPORTANT!!!! ----> PUT THE NUMBER OF INFOBOXES HERE OR THE PROGRAM WON'T RUN CORRECTLY
        infoShowing = false;

        for(int i=0; i<infoboxes; i++)
        {
            GameObject holder = Instantiate(infoBoxObject, new Vector2(0, 0), Quaternion.identity);
            holder.transform.SetParent(canvas.transform, false);
            holder.GetComponent<InfoBox>().type = i;
        }
    }
}
