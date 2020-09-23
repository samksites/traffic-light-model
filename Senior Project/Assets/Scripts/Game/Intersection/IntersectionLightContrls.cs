using UnityEngine;

public class IntersectionLightContrls : MonoBehaviour
{
    // all the lanes that have lights
    GameObject[] lightLanes;

    // how long the north and south lights will stay green
    double northSouthLights = 10;

    // how long the east and west lights will stay green
    double eastWestLights = 10;

    // how long yellow lasts
    double yellow = 5;


    int checkLightOne;
    int checkLightTwo;
    double countDown;
    double yellowCountDown;

    // if its a zero north south lights are green
    // if its a one then east west lights are green
    int lightDirection = -1;

    bool startedLight = true;

    bool yellowPass = true;

    // when the lights should stop running
    bool runLights = false;

    // Update is called once per frame
    void Update()
    {

        if (runLights)
        {
            // calls the north south light lanes
                LightChanging();
            
        }

    }

    // sets the script to start running lights
     public void StartLights()
    {
        runLights = true;

        // gets the intersection lights
        lightLanes = gameObject.GetComponent<Intersection>().GetIntersectionLanes();
    }

    void LightChanging()
    {

        if (startedLight)
        {
            if (lightDirection == -1)
            {
                countDown = northSouthLights;

                checkLightOne = 0;

                checkLightTwo = 1;

                yellowCountDown = yellow;

                startedLight = false;
            }
            else
            {
                countDown = eastWestLights;

                checkLightOne = 2;

                checkLightTwo = 3;

                yellowCountDown = yellow;

                startedLight = false;
            }
        }
            // needs to be fixed
           

        
        // checks if the time is 
        if(countDown > 0)
        {

            if (countDown == 10)
            {
                foreach (GameObject light in lightLanes)
                {
                    int lightType = light.GetComponent<LaneByLane>().GetIntersectionType();

                    if (lightType == checkLightOne || lightType == checkLightTwo)
                    {
                        light.GetComponent<LaneByLane>().ChangeLight(2);
                        light.GetComponent<LaneByLane>().TurnLightOff(0);
                    }
                    else
                    {
                        light.GetComponent<LaneByLane>().ChangeLight(0);
                        light.GetComponent<LaneByLane>().TurnLightOff(1);
                    }
                }
            }


            // counts down the time
            countDown -= Time.deltaTime;


          
            
        }
        else
        {
            if (yellowPass)
            {
                foreach (GameObject light in lightLanes)
                {
                    int lightType = light.GetComponent<LaneByLane>().GetIntersectionType();

                    if (lightType == checkLightOne || lightType == checkLightTwo)
                    {
                        // sets north south lights to yellow
                        light.GetComponent<LaneByLane>().ChangeLight(1);
                        light.GetComponent<LaneByLane>().TurnLightOff(2);
                    }
                }
                // verrible declring the yellow has been set
                yellowPass = false;
            }

            yellowCountDown -= Time.deltaTime;
            if (yellowCountDown <= 0)
            {
                yellowPass = true;
                lightDirection *= -1;

                startedLight = true;

                foreach (GameObject light in lightLanes)
                {
                    int lightType = light.GetComponent<LaneByLane>().GetIntersectionType();

                    if (lightType == checkLightOne || lightType == checkLightTwo)
                    {
      
                        
                        light.GetComponent<LaneByLane>().TurnLightOff(1);
                    }
                }


            }

        }
        

        

    }
}
