using UnityEngine;

public class Intersection : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject[] laneObjects;

    GameObject[] lanesWithLights;

    // simn started
    bool simStarted = false;

    // count of lanes in order of north south east west
    int[] laneValues = { 2, 2, 2, 2 };

    public GameObject panelControler;

    public GameObject lanePecies;

    Vector3 dist;

    float posX;

    float posY;


    [System.Obsolete]
    public void Start()
    {
        panelControler = GameObject.Find("Panel Controlers");

        Vector3 alterBoxSize = gameObject.GetComponent<BoxCollider>().size;

        // changes the box colider to be slightly smaller then the actual intersection
        gameObject.GetComponent<BoxCollider>().size = new Vector3(alterBoxSize.x * .9999f, alterBoxSize.y, alterBoxSize.z * .9999f);

        SetLanes(laneValues);

    }

    [System.Obsolete]
    void SetLanes(int[] lanes)
    {

        // destorys all child objects

        int numberOfChildren = transform.GetChildCount();

        for (int i = 0; i < numberOfChildren; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // number of north and south lanes
        int northSouthLanes = lanes[0] + lanes[1];

        
        

        // number of east and west lanes
        int eastWestLanes = lanes[2] + lanes[3];

        // sets lenghts of lanes with just lights
        lanesWithLights = new GameObject[northSouthLanes + eastWestLanes];

        // sets size of the intersection
        float xLength = 3.7f * northSouthLanes;

        float zLenght = 3.7f * eastWestLanes;


        // used to calcualte the size of each lane
        float northSouthScale = 1f / northSouthLanes;

        float eastWestScale = 1f / eastWestLanes;

        // where the lane pecices will be placed
        float northSouthPlacment = .5f - (northSouthScale / 2f);

        float eastWestPlacment = .5f - (eastWestScale / 2f);


        // new intersection size
        transform.localScale = new Vector3(xLength, .1f, zLenght);


        // number of lanes thier will be in a intersection. 
        laneObjects = new GameObject[(northSouthLanes + eastWestLanes) * 2];


        // used to set postion of intersection lanes
        int t = 0;

        int lightLaneIndex = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int z = 0; z < lanes[i]; z++)
            {
                // instates a new game object
                GameObject oneLane = Instantiate(lanePecies, gameObject.transform.localPosition, Quaternion.identity);
                // sets the parent
                oneLane.transform.parent = gameObject.transform;

                oneLane.gameObject.name = "inter";

                // instantates the left lane version
                GameObject oneLaneCopy = Instantiate(oneLane, gameObject.transform.localPosition, Quaternion.identity);
                oneLaneCopy.transform.parent = gameObject.transform;

                oneLaneCopy.gameObject.name = "inter";

                // if its a north our south lane
                if (i == 0 || i == 1)
                {
                    Vector3 demensions = new Vector3(.0001f, 1, northSouthScale);

                    // sets the local scale for both objects
                    oneLane.transform.localScale = demensions;

                    oneLaneCopy.transform.localScale = demensions;

                    Vector3 placment = new Vector3(FlipValues(i) * .5f, 0, northSouthPlacment);

                    Vector3 placmentCopy = new Vector3(FlipValues(i) * -.5f, 0, northSouthPlacment);

                    northSouthPlacment -= northSouthScale;

                    // sets the new postion
                    oneLane.transform.localPosition = placment;

                    oneLaneCopy.transform.localPosition = placmentCopy;


                }
                // same thing as above but for east west
                else
                {

                    Vector3 demensions = new Vector3(eastWestScale, 1, .0001f);

                    // sets the local scale for both objects
                    oneLane.transform.localScale = demensions;

                    oneLaneCopy.transform.localScale = demensions;

                    Vector3 placment = new Vector3(eastWestPlacment, 0, FlipValues(i) * .5f);

                    Vector3 placmentCopy = new Vector3(eastWestPlacment, 0, FlipValues(i) * -.5f);

                    eastWestPlacment -= eastWestScale;

                    // sets the new postion
                    oneLane.transform.localPosition = placment;

                    oneLaneCopy.transform.localPosition = placmentCopy;

                }

                // places the objects in the array of inter section pecices
                laneObjects[t] = oneLane;

                laneObjects[t + 1] = oneLaneCopy;

                // sets lane types of the lanes
                oneLane.GetComponent<LaneByLane>().SetLaneType(i);

                oneLane.GetComponent<LaneByLane>().CreateLight();

                oneLaneCopy.GetComponent<LaneByLane>().SetLaneType(i + 4);

                lanesWithLights[lightLaneIndex] = oneLane;

                // increments t
                t += 2;

                // increments the lights
                lightLaneIndex++;
            }
        }
    }

    public GameObject[] GetIntersectionLanes()
    {
        return lanesWithLights;
    }

    // for intersection lanes flips them in the right direction
    private float FlipValues(int direction)
    {
        if (direction == 0 || direction == 3)
        {
            return 1f;
        }

        return -1f;
    }
    public void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            panelControler.GetComponent<PlaneControler>().OpenIntersectionPanel(gameObject);

        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(gameObject);
        }
    }

    // talks to panel controler
    [System.Obsolete]
    public void NumberOfLanes(int north, int south, int east, int west)
    {
        // int array of how many of each lane their will be
        int[] laneNumbers = { north, south, east, west };

        for (int i = 0; i < 4; i++)
        {
            if (laneNumbers[i] == -1)
            {
                laneNumbers[i] = laneValues[i];
            }

        }

        laneValues = laneNumbers;

        SetLanes(laneNumbers);

    }
    


    void OnMouseDown()
    {
        if (simStarted)
        {
            return;
        }

        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;

    }

    void OnMouseDrag()
    {
        if (simStarted)
        {
            return;
        }
        Vector3 curPos =
        new Vector3(Input.mousePosition.x - posX,
        Input.mousePosition.y - posY, dist.z);



        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        // sets hight to be the same every time
        worldPos.y = .1f;
        // fixes ofset when rotated

        // roates lane when r button is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {



            transform.Rotate(0, 90f, 0);


        }



        // places the object where it should be
        transform.position = worldPos;

    }


    // Siginals the begging for the lanes to start
    public void StartLanes()
    {
        simStarted = true;
        foreach (GameObject lightLane in laneObjects)
        {
            lightLane.GetComponent<LaneByLane>().LightStartUp();
        }

        gameObject.GetComponent<IntersectionLightContrls>().StartLights();

    }

}

