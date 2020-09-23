using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtionPecies : MonoBehaviour
{

    GameObject choseButtion;

    GameObject[] objectButtions;
    
    void Start()
    {
        choseButtion = GameObject.FindGameObjectsWithTag("intialButtion")[0];

        objectButtions = GameObject.FindGameObjectsWithTag("Pecices");

        hideButtions(false);

    }

    // sets buttons to active or not active
    void hideButtions(bool buttonAction)
    {
        foreach(GameObject but in objectButtions)
        {
            but.SetActive(buttonAction);
        }
    }

    public void HideChoseButtion(int but)
    {
        // if the CHoose Objects button is pressed
        if (but == 0)
        {
            choseButtion.SetActive(false);
            hideButtions(true);
        }
        else
        {
            choseButtion.SetActive(true);
            hideButtions(false);
        }
    }

    public void SimStarted()
    {
        choseButtion.SetActive(false);
    }

  
}
