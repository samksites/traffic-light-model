using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoad : Lane
{

   
    private int carCount = 0;

    [SerializeField]
    private GameObject Car;

    
    private float timeBetween;
    private float time;

    private bool startCars = true;

    Vector3 carStart;

    void Start()
    {
        base.Start();
        base.type = 1;
        time = timeBetween;
    }

    void Update()
    {
        
        if (!startCars)
        {
            if(carCount == 0)
            {
                return;
            }
            Countdown();
            return;
        }
        base.Update();
        
       

        
    }

    void Countdown()
    {
        int directin = base.FindDirection();



        carStart = new Vector3(transform.position.x, 0.84f, transform.position.z);

        if(directin == 0)
        {
            carStart.z -= transform.localScale.z / 2;
        } else if (directin == 1)
        {
            carStart.x -= transform.localScale.z / 2;
        }
        else if (directin == 2)
        {
            carStart.z += transform.localScale.z / 2;
        }
        else if (directin == 3)
        {
            carStart.x += transform.localScale.z / 2;
        }

        if (time <= 0)
        {
            time = timeBetween;
            

            GameObject car = Instantiate(Car, carStart, Quaternion.identity);

            car.GetComponent<Car>().SetYrotation(transform.eulerAngles.y);
            car.GetComponent<Car>().setCarSpeed(base.speed);
            time -= Time.deltaTime;
            carCount -= 1;
        }
        else
        {
            time -= Time.deltaTime;
        }

    }

    // method is called when game begins
    public void StartSpawning()
    {
        this.startCars = false;

        base.SimStarted();
    }

    void OnMouseOver()
    {
        base.OnMouseOver();
        if (Input.GetKey(KeyCode.Mouse1))
        {
            base.panelControler.GetComponent<PlaneControler>().OpenPanleOfLanes(gameObject);
        }
        
    }

    public void Count(int carNumber)
    {
        carCount = carNumber;

    }

    public void Tween(float timeTweenCars)
    {
        timeBetween = timeTweenCars;
    }
    
    }
