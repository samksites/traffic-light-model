using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaneByLane : MonoBehaviour
{
   
    public int interSectionLaneType = -1;

    private bool start = false;

    //____________________________________________________
    // everything bellow here is light contrl related

    // array of light peices 0 = red 1 = yellow 2 = green
    

    public GameObject lightPeice;

    private GameObject light;

    // if lane is a light lane
    private bool lightLane = false;


    // if the first car has passed the light
    private bool firstCar = false;

    // distance that the car is from the light
    private float mphOfRoad;

    // value of light can be red yellow or green
    private int lightValue;

    // ray lane will shoot out
    Ray ray;

    // ray that hits the cars 
    Ray carHitray;

    RaycastHit hit;

    // sets lane type

    public void LightStartUp()
    {
        if (interSectionLaneType == 0 || interSectionLaneType == 1 || interSectionLaneType == 2 || interSectionLaneType == 3)
        {
            lightLane = true;
            SetLightDistance();
            start = true;
            Vector3 carRayPostion = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
            carHitray = new Ray(carRayPostion, ray.direction);
        }

        

    }

    // meathod to put up the traffic lights
    public void CreateLight()
    {
        // instantiates the traffic light
        light = Instantiate(lightPeice, new Vector3(transform.position.x, transform.position.y + 5.4864f, transform.position.z), Quaternion.identity);

        if(interSectionLaneType == 0)
        {
            light.transform.Rotate(new Vector3(0, 270, 0));

        } else if(interSectionLaneType == 1)
        {
            light.transform.Rotate(new Vector3(0, 90, 0));
            
        } else if(interSectionLaneType == 3)
        {
            light.transform.Rotate(new Vector3(0, 180, 0));
        }

        light.transform.parent = gameObject.transform;

    }

    private void Update()
    {
        if(lightLane && start)
        {
            // talks to the car if they shoud go or not
            TalkTocar();
            
            
        }
        
    }

    public void SetLaneType(int laneType)
    {
        interSectionLaneType = laneType;
    }

    public int GetLaneType()
    {
        return interSectionLaneType;
    }

    // talks to the first car infront of the light
    void TalkTocar()
    {
        
        
        if (!firstCar && (lightValue == 0 || lightValue == 1))
        {
            
            if (Physics.Raycast(carHitray.origin,carHitray.direction, out hit))
            {
                // **************************** continue work here
                hit.collider.gameObject.GetComponent<Car>().ApproachingIntersection(hit.distance);

                // the first car has been hit
                firstCar = true;
            }
           

            // ***************  needs to work on calculation of distance
            Debug.DrawLine(carHitray.origin, carHitray.origin + carHitray.direction * 10, Color.red);
        } else if (!firstCar)
        {
            Debug.DrawLine(carHitray.origin, carHitray.origin + carHitray.direction * 10, Color.blue);

            if (Physics.Raycast(carHitray.origin, carHitray.direction, out hit))
            {
                // **************************** continue work here
                print("here");
                hit.collider.gameObject.GetComponent<Car>().IntersectionAccelerate();

                // the first car has been hit
                firstCar = true;

            }
        }

    }

    // method sets how far away the light will comunicate with cars when its red or yellow based on speed of road
    public void SetLightDistance()
    {
        // used to get lane speed
        RaycastHit hitInfo;

        if (!lightLane)
        {
            return;
        }

            if(interSectionLaneType == 0)
            {   
                // temp ray to find the lane infront of it
                Ray tempRay = new Ray(transform.position, transform.right);

                // puts out a ray infront of lane
                Physics.Raycast(tempRay, out hitInfo, 1);

                // need to find the conversion here
                float speedOfRoad = hitInfo.collider.gameObject.GetComponent<Lane>().GetSpeed();
                mphOfRoad = speedOfRoad;


            Vector3 lightCast = new Vector3(transform.position.x + speedOfRoad, transform.position.y, transform.position.z );
                    ray = new Ray(lightCast, transform.right);

            } else if (interSectionLaneType == 1)
            {

                // temp ray to find the lane infront of it
                Ray tempRay = new Ray(transform.position, transform.right * -1);

                // puts out a ray infront of lane
                Physics.Raycast(tempRay, out hitInfo, 1);

                // need to find the conversion here
                float distanceOfLight = hitInfo.collider.gameObject.GetComponent<Lane>().GetSpeed();
                mphOfRoad = distanceOfLight;

                Vector3 lightCast = new Vector3(transform.position.x - distanceOfLight, transform.position.y, transform.position.z);
                
                ray = new Ray(lightCast, transform.right * -1);
            }
            else if (interSectionLaneType == 2)
            {
                // temp ray to find the lane infront of it
                Ray tempRay = new Ray(transform.position, transform.forward * -1);

                // puts out a ray infront of lane
                Physics.Raycast(tempRay, out hitInfo, 1);

                // need to find the conversion here
                float distanceOfLight = hitInfo.collider.gameObject.GetComponent<Lane>().GetSpeed();
                mphOfRoad = distanceOfLight;

                Vector3 lightCast = new Vector3(transform.position.x, transform.position.y, transform.position.z - distanceOfLight);

                ray = new Ray(lightCast, transform.forward * -1);

            } else
            {

                // temp ray to find the lane infront of it
                Ray tempRay = new Ray(transform.position, transform.forward);

                // puts out a ray infront of lane
                Physics.Raycast(tempRay, out hitInfo, 1);

                // need to find the conversion here
                float distanceOfLight = hitInfo.collider.gameObject.GetComponent<Lane>().GetSpeed();
                mphOfRoad = distanceOfLight;

                Vector3 lightCast = new Vector3(transform.position.x, transform.position.y, transform.position.z + distanceOfLight);

                ray = new Ray(lightCast, transform.forward);
            }
           
            
        
    }

    // is called to turn the light and turn on casting
    public void ChangeLight(int lightType)
    {
        
        lightValue = lightType;

        if(lightValue == 2 || lightValue == 1)
        {
            firstCar = false;
        }
        SetLight(lightType);
    }


    // sets the color of the light on a 0 red to 2 green scale
    void SetLight(int lightColor)
    {
        gameObject.transform.GetChild(0).GetComponent<TrafficLight>().ColorLight(lightColor);
    }

    public void TurnLightOff(int bulb)
    {
        gameObject.transform.GetChild(0).GetComponent<TrafficLight>().BulbOff(bulb);
    }

    // returns the intersection type
    public int GetIntersectionType()
    {
        return interSectionLaneType;
    }



}
