
using UnityEngine;
using HighlightingSystem;

public class TrafficLight : MonoBehaviour
{

    GameObject[] lights = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        lights[0] = transform.GetChild(0).gameObject;

        lights[1] = transform.GetChild(1).gameObject;

        lights[2] = transform.GetChild(2).gameObject;

    }

    // sets the traffic lights to be red yellow or green
    public void ColorLight(int ryg)
    {
       
            var colorChange = lights[ryg].GetComponent<Highlighter>();
            colorChange.constant = true;

        if(ryg == 0)
        {
            colorChange.constantColor = Color.red;
        } else if(ryg == 1)
        {
            colorChange.constantColor = Color.yellow;
        }
        else
        {
            colorChange.constantColor = Color.green;
        }
        
    }

    // used to turn light bulbs off
    public void BulbOff(int bulb)
    {
        var singleBulb = lights[bulb].GetComponent<Highlighter>();

        singleBulb.constant = false;
    }

   
}
