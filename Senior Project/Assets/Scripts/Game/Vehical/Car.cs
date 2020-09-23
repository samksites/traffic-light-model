using UnityEngine;

public class Car : MonoBehaviour
{
    // speed of car
    public float carSpeed;

    // speed of the road
    private float roadSpeed;

    // aproaching light;
    private bool lightRedOrYellow = false;


    // how far it is from the light
    float lightBreakingSpeed;

    private void Start()
    {
        roadSpeed = carSpeed;
    }

    void Update()
    {
        MoveCar();
    }

    // sets the rotation of the car called from spawn lanes
    public void SetYrotation(float yAngle)
    {

        transform.eulerAngles = new Vector3(0, yAngle, 0);
    }


    // sets the cars speed in mph called from spawn lanes
    public void setCarSpeed(float mph)
    {
        this.roadSpeed = mph;
        this.carSpeed = this.roadSpeed;

    }

    void MoveCar()
    {

        // if the car is approachinga red light
        if (lightRedOrYellow && carSpeed > 0.09)
        {
                // car getting closer to the intersection
                BreakToLight();
            
        } else if (!lightRedOrYellow && carSpeed < roadSpeed)
        {

            Accelerate();
        }

        

        transform.position += transform.forward * carSpeed * Time.deltaTime;
        
    }


    //_____________________________________________________________________________________________
    // all sections below here have to do with slowing and speeding up the car

    // called when the car needs to break for aproaching a light
    void BreakToLight()
    {
        carSpeed -= lightBreakingSpeed * Time.deltaTime;
        transform.position += transform.forward * carSpeed * Time.deltaTime;
        if(carSpeed <= 0.09)
        {
            // sets the speed of the car to 0
            carSpeed = 0;
            
        }

    }



    // method to slow down cars
    public void ApproachingIntersection(float dinstace)
    {

        // sets the car to be aporaching a light
        lightRedOrYellow = true;
        // the car will take 5 seconds to break

      


        lightBreakingSpeed = dinstace / 25;

    }

    // called when car needs to start accelerating
    public void IntersectionAccelerate()
    {
        print("accelerate");
        // says their is a green light
        lightRedOrYellow = false;
    }

    void Accelerate()
    {
        // accelerates the car
        carSpeed += 3.5f * Time.deltaTime;
    }
}
