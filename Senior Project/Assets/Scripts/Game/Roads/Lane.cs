using UnityEngine;
using HighlightingSystem;
using System;

public class Lane : MonoBehaviour
{

    // whats sim starts roads cant be changed
    bool simStart = false;

    // Start is called before the first frame update

    Vector3 dist;
    float posX;
    float posY;

    public float speed = 10;

    private string distance_values;

    // used only to switch cast direction of deleat lanes
    private int castDirection = 1;
    

    // bool if the lane is close enough to snap
    bool snappable = false;
    

    // used to detected if a peice is snapped
    bool snapped = false;

    // check if the peice is clicked on

    bool clicked = false;

    bool exited = false;

 

    // distance to the next peice
    float distance;
    // controls which direction the peice moves when it snaps
    int moveDirection;

    // if the mouse is down on a peice which activates the ray for snapping
    bool activeRay = false;

    float width;

    float xFix;

    float angle;

    Vector3 center_box;

    // type of lane
    public int type = 0;

    public GameObject panelControler;

    public void Start()
    {
        panelControler = GameObject.Find("Panel Controlers");
        
    }
 

    public void Update()
    {

        if (simStart)
        {
            return;
        }

        if (activeRay)
        {
            Cast();
        }

        if (clicked)
        {
            ColorHelper();
        }
        
    }

    public void ChangeSize(float langth)
    {
        transform.localScale = new Vector3(3.7f, .1f, langth);
    }

    void Cast()
    {
        FixAngles();

        

        Vector3 castFoward = new Vector3(transform.position.x + xFix, transform.position.y, transform.position.z + width);

        Vector3 castBackward = new Vector3(transform.position.x + xFix, transform.position.y,  transform.position.z - width );

        Ray ray = new Ray(castFoward, transform.forward);

        Ray rayTwo = new Ray(castBackward, transform.forward * -1);

        RaycastHit hitInfo;

        RaycastHit hitInfoTwo;

        // if its a regular lane
        if (type == 0)
        {
            bool hitOne = Physics.Raycast(ray, out hitInfo, 5);

            bool hitTwo = Physics.Raycast(rayTwo, out hitInfoTwo, 5);

            if (!hitOne && !hitTwo)
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 20, Color.green);

                Debug.DrawLine(rayTwo.origin, rayTwo.origin + rayTwo.direction * 20, Color.green);

                snappable = false;
                
                // used to snap the peice to the right or left if it is a spawn or deleate
            } 
            else
            {
                string objectHitName = "-1";

                string objectHitNameTwo = "-1";
                // checks to see if object can snap to a right lane by object name it is hiting
                try
                {
                    objectHitName = hitInfo.collider.gameObject.name;
                }
                catch (NullReferenceException)
                {
                    
                }

                try
                {
                    objectHitNameTwo = hitInfoTwo.collider.gameObject.name;
                }
                catch (NullReferenceException)
                {

                }
                // used to calculated distances to intersection
                float distanceOne = 20;

                float distanceTwo = 20;

                Vector3 center_one = new Vector3(0,0,0);

                Vector3 center_two = new Vector3(0, 0, 0); ;

                if (objectHitName == "inter")
                {

                    Debug.DrawLine(ray.origin, hitInfo.point, Color.red);

                    distanceOne = hitInfo.distance;

                    // gets the center of he hit box
                    center_one = hitInfo.collider.bounds.center;

                    snappable = true;

                } if(objectHitNameTwo == "inter")
                {
                    Debug.DrawLine(rayTwo.origin, hitInfoTwo.point, Color.red);

                    distanceTwo = hitInfoTwo.distance;

                    // gets the center of he hit box
                    center_two = hitInfoTwo.collider.bounds.center;

                    snappable = true;
                }
                if(distanceOne < distanceTwo)
                {
                    center_box = center_one;

                    distance = distanceOne;

                    ResetDirect();
                } else
                {
                    center_box = center_two;

                    distance = distanceTwo;

                    ChangeDirection();
                }


            }

        }
        // end of regualr lane code
        
        else if(type == 1)
        {
            if(!Physics.Raycast(ray, out hitInfo, 5))
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 20, Color.green);
                snappable = false;
            }
            else
            {
                if(hitInfo.collider.gameObject.name == "inter")
                {
                    
                    if(hitInfo.collider.gameObject.GetComponent<LaneByLane>().GetLaneType() < 4)
                    {
                        Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
                        center_box = hitInfo.collider.bounds.center;
                        distance = hitInfo.distance;
                        ResetDirect();
                        snappable = true;
                    }
                    else
                    {
                        snappable = false;
                    }
                   
                }
            }

        }
        else if (type == 2)
        {
            if (!Physics.Raycast(rayTwo, out hitInfoTwo, 5))
            {
                Debug.DrawLine(rayTwo.origin, rayTwo.origin + rayTwo.direction * 20, Color.green);
                snappable = false;
            }
            else
            {
                if (hitInfoTwo.collider.gameObject.name == "inter")
                {
                    if (hitInfoTwo.collider.gameObject.GetComponent<LaneByLane>().GetLaneType() > 3)
                    {
                        Debug.DrawLine(ray.origin, hitInfoTwo.point, Color.red);
                        center_box = hitInfoTwo.collider.bounds.center;
                        distance = hitInfoTwo.distance;
                        ChangeDirection();
                        snappable = true;
                    }
                    else
                    {
                        snappable = false;
                    }
                }
            }

        }






    }

    // method fixes the postion of the cast ray and which directon the peice is facing
    void FixAngles()
    {
        // following code controls where the lazer 
        width = transform.localScale.z / 2;
        xFix = 0;
        angle = Mathf.Abs(transform.eulerAngles.y);
        moveDirection = 0;

        if (angle >= 89.5 && angle < 90.1)
        {
            xFix = width;
            width = 0;

            // which direction the peice has to go to snap
            moveDirection = 1;
        }
        else if (angle >= 179.9 && angle < 180.1)
        {
            width = width * -1;

            // which direction the peice has to go to snap
            moveDirection = 2;

        }
        else if (angle >= 269.9 && angle < 270.1)
        {
            xFix = width * -1;
            width = 0;

            // which direction the peice has to go to snap
            moveDirection = 3;

        }
    }

    public Vector3 postion()
    {

        return transform.position;
    }

    public Vector3 LocalScale()
    {

        return transform.localScale;
    }

  
    /// <summary>
    /// Mouse controls
    /// Control of clicking 
    /// Draggin
    /// And camera angles
    /// </summary>
    /// 

    public void OnMouseOver()
    {
        if (simStart)
        {
            return;
        }

        ColorHelper();


        // below is both transform size and display values
        float feet = transform.localScale.z * 3.28f;
        float miles = transform.localScale.z * 0.000621371f;
        float speedMs = speed * 2.23694f;

        distance_values = "Length in feet: " + feet.ToString("F2") + "\n";
        distance_values += "Length in miles: " + miles.ToString("F2") + "\n";
        distance_values += "Speed of road MPH: " + speedMs.ToString("F0") + "\n";

        GameObject[] passDistance = GameObject.FindGameObjectsWithTag("canvas_distances");
        passDistance[0].GetComponent<Distance_Canvas>().ChangeText(distance_values);

        if (simStart)
        {
            return;
        }

        exited = false;

        // calles input panles
        if (Input.GetKey(KeyCode.Mouse1))
        {
           
            panelControler.GetComponent<PlaneControler>().OpenPanleOfLanes(gameObject);
        }

        // deleates object if deleate key is pressed
        if (Input.GetKey(KeyCode.Delete))
        {
            Destroy(gameObject);
            InfoBoxEmpty();
        }

    }

    void OnMouseExit()
    {
        ColorOff();

        exited = true;
        
        InfoBoxEmpty();
    }

    void InfoBoxEmpty()
    {

        // below is both transform size and display values
        distance_values = "Length in feet: " + "\n";
        distance_values += "Length in miles: " + "\n";
        distance_values += "Speed of road MPH: " + "\n";

        GameObject[] passDistance = GameObject.FindGameObjectsWithTag("canvas_distances");
        passDistance[0].GetComponent<Distance_Canvas>().ChangeText(distance_values);
    }

    void OnMouseDown()
    {

        if (simStart)
        {
            return;
        }
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;

        activeRay = true;
        clicked = true;

       



    }

    void ColorHelper()
    {
        var colorChange = GetComponent<Highlighter>();
        colorChange.constant = true;

        if (snapped || snappable)
        {
            colorChange.constantColor = Color.green;
        }
        else
        {
            colorChange.constantColor = Color.yellow;
        }
    }

    void ColorOff()
    {
        var colorChange = GetComponent<Highlighter>();
        colorChange.constant = false;
    }

    void OnMouseDrag()
    {
       
        if (simStart)
        {
            return;
        }

        snapped = false;
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


    void OnMouseUp()
    {
        if (simStart)
        {
            return;
        }

        activeRay = false;
        clicked = false;

        if (exited)
        {
            ColorOff();
        }
        

        if (snappable)
        {

            // uses the moveDirection varrible to chose which way it snaps
            switch (moveDirection)
            {
                case 0:
                    // finds the distance to the hit box and then moves the pices together
                    transform.position += Vector3.forward * distance * castDirection;

                    // makes sure the peice is senterd on the hit box
                    transform.position = new Vector3( center_box.x, transform.position.y, transform.position.z);
                    
                    break;
                case 1:
                    transform.position += Vector3.right * distance;

                    transform.position = new Vector3(transform.position.x, transform.position.y, center_box.z);
                    break;
                case 2:
                    transform.position += Vector3.back * distance;
                    transform.position = new Vector3(center_box.x, transform.position.y, transform.position.z);
                    break;

                case 3:
                    transform.position += Vector3.left * distance;
                    transform.position = new Vector3(transform.position.x, transform.position.y, center_box.z);
                    break;
            }

            snappable = false;
            snapped = true;


        }
    }

    public int FindDirection()
    {
        int direction = 0;

        if(Mathf.Abs(transform.eulerAngles.y) == 90)
        {
            direction = 1;
        }
        else if (Mathf.Abs(transform.eulerAngles.y) == 180)
        {
            direction = 2;
        }
        else if (Mathf.Abs(transform.eulerAngles.y) == 270)
        {
            direction = 3;
        }

        return direction;
    }

    public void ChangeLenght(int length)
    {
        // changes the meters to feet
        float fixedLength = length * 0.3048f;

        // new size
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y,fixedLength);
    }

    public void ChangeMPH(int mph)
    {
        speed = mph;
    }

    public void SimStarted()
    {

        
        simStart = true;

    }

    // returns what kind of lane this object is
    public int LaneType()
    {

        return type;
    }

    public void ChangeDirection()
    {
        castDirection = -1;
    }

    void ResetDirect()
    {
        castDirection = 1;
    }

    public float GetSpeed()
    {
        return this.speed;
    }

}
