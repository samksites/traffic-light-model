
using UnityEngine;

public class DeleatLane : Lane
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        base.ChangeDirection();
        base.type = 2;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    // destorys the car when it leaves
    void OnTriggerEnter(Collider car)
    {

        if (car.tag == "Cars")
        {

            Destroy(car.gameObject);
        }
    }

    // method is called when game begins
    public void SimBegin()
    {
        base.SimStarted();
    }

  


}
