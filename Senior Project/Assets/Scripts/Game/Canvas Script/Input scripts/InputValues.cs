
using UnityEngine;
using TMPro;


public class InputValues : MonoBehaviour
{
    int distance = -1;

    int mph = -1;

    int cars = -1;

    float timeBetween = -1;

    // for intersections
    int north = -1;

    int south = -1;

    int east = -1;

    int west = -1;

    public TextMeshProUGUI distanceInputFiled;

    public TextMeshProUGUI mphInputField;

    public TextMeshProUGUI carsInputField;

    public TextMeshProUGUI timeBetweenInputField;


    // for intersection
    public TextMeshProUGUI northInputField;

    public TextMeshProUGUI southInputField;

    public TextMeshProUGUI eastInputField;

    public TextMeshProUGUI westInputField;

    public void ClickedSubmitRegLane()
    {
        MPHandLength();
        

        gameObject.GetComponentInParent<PlaneControler>().RegularLane(distance,mph);

    }

    // spawn raod clicked button
    public void ClickedSubmitSpawnLane()
    {

        string tempCarCount = carsInputField.text;

        if (tempCarCount.Length > 1)
        {
            tempCarCount = tempCarCount.Remove(tempCarCount.Length - 1, 1);

            cars = int.Parse(tempCarCount);
        }

        string tempTime = timeBetweenInputField.text;

        if (tempTime.Length > 1)
        {
            tempTime = tempTime.Remove(tempTime.Length - 1, 1);

            timeBetween = float.Parse(tempTime);
        }

        gameObject.GetComponentInParent<PlaneControler>().SpawnLane(cars, timeBetween);

        MPHandLength();

        gameObject.GetComponentInParent<PlaneControler>().RegularLane(distance, mph);


    }

    // when submit is pressed on a intersection
    public void ClickedSubmitIntersection()
    {
        string tempNorth = northInputField.text;

        if(tempNorth.Length > 1)
        {
            tempNorth = tempNorth.Remove(tempNorth.Length - 1, 1);

            north = int.Parse(tempNorth);
        }

        string tempSouth = southInputField.text;

        if (tempSouth.Length > 1)
        {
            tempSouth = tempSouth.Remove(tempSouth.Length - 1, 1);

            south = int.Parse(tempSouth);
        }

        string tempEast = eastInputField.text;

        if(tempEast.Length > 1)
        {
            tempEast = tempEast.Remove(tempEast.Length - 1, 1);

            east = int.Parse(tempEast);
        }

        string tempWest = westInputField.text;

        if(tempWest.Length > 1)
        {
            tempWest = tempWest.Remove(tempWest.Length - 1, 1);

            west = int.Parse(tempWest);
        }

        gameObject.GetComponentInParent<PlaneControler>().Intersection(north, south, east, west);
    }

    

    void MPHandLength()
    {
        string tempDistance = distanceInputFiled.text;

        // makes sure the user enterd a value or it just defults to -1
        if (tempDistance.Length > 1)
        {
            // removes last mysterious charictor
            tempDistance = tempDistance.Remove(tempDistance.Length - 1, 1);

            // converts to intager
            distance = int.Parse(tempDistance);

        }

        string tempMPH = mphInputField.text;

        // makes sure the user enterd a value or it just defults to -1
        if (tempMPH.Length > 1)
        {
            tempMPH = tempMPH.Remove(tempMPH.Length - 1, 1);

            mph = int.Parse(tempMPH);
        }
    }
    
}
