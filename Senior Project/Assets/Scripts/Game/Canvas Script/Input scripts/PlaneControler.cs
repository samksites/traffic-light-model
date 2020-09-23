using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControler : MonoBehaviour
{
    GameObject[] panels = new GameObject[2];

    GameObject laneObject;

    GameObject newIntersection;

    void Start()
    {
        panels = GameObject.FindGameObjectsWithTag("Panles");


        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
            
        }
    }
  
    public void OpenPanleOfLanes(GameObject typeOfLane)
    {
        // type of lane
        int laneType = typeOfLane.GetComponent<Lane>().LaneType();

        laneObject = typeOfLane;

        if(laneType == 0 || laneType == 2)
        {
            panels[0].SetActive(true);
        }
        if(laneType == 1)
        {
            panels[1].SetActive(true);
        }

    }

    public void OpenIntersectionPanel(GameObject intersection)
    {
        panels[2].SetActive(true);

        newIntersection = intersection;
    }

    public void RegularLane(int feet, int mph)
    {

        
        // if the value passed is -1 then the lenght does not chance
        if(feet != -1)
        {
            laneObject.GetComponent<Lane>().ChangeLenght(feet);
        }
        
        // if the value passed is -1 then the mph does not change
        if(mph != -1)
        {
            laneObject.GetComponent<Lane>().ChangeMPH(mph);
        }

        gameObject.GetComponentInChildren<CloseButton>().ClosePannel();
    }

    // spawn lane controler
    public void SpawnLane(int carCount, float tween)
    {
        if(carCount != -1)
        {
            laneObject.GetComponent<SpawnRoad>().Count(carCount);
        }

        if (tween != -1)
        {
            laneObject.GetComponent<SpawnRoad>().Tween(tween);
        }
    }

    public void Intersection(int north, int south, int east, int west)
    {
        newIntersection.GetComponent<Intersection>().NumberOfLanes(north, south, east, west);

        gameObject.GetComponentInChildren<CloseButton>().ClosePannel();
    }


}
