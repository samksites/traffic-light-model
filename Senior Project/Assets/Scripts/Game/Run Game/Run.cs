using UnityEngine;

public class Run : MonoBehaviour

{
    

   public void StartSimulation()
    {

        // sets up the intersection interactions
        GameObject[] allIntersections = GameObject.FindGameObjectsWithTag("Intersection");

        foreach (GameObject intersections in allIntersections)
        {
            intersections.GetComponent<Intersection>().StartLanes();
        }

        GameObject[] allLanes = GameObject.FindGameObjectsWithTag("RegLanes");

        foreach (GameObject lanes in allLanes)
        {
            lanes.GetComponent<Lane>().SimStarted();
        }


            // all spawn lane roads
            GameObject[] allSpawnRoads = GameObject.FindGameObjectsWithTag("spawnLanes");

        

        foreach (GameObject spRoads in allSpawnRoads)
        {
            spRoads.GetComponent<SpawnRoad>().StartSpawning();
        }

        GameObject[] allDeleatLanes = GameObject.FindGameObjectsWithTag("DeleatLanes");



        foreach (GameObject dRoads in allDeleatLanes)
        {
            dRoads.GetComponent<DeleatLane>().SimBegin();
        }

        


        gameObject.SetActive(false);

        // hides the add objects buttons
        GameObject.FindGameObjectsWithTag("buttonControler")[0].GetComponent<ButtionPecies>().SimStarted();

        // changes camera view
        Destroy(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TwoDCameraControler>());
        GameObject.FindGameObjectWithTag("MainCamera").AddComponent<ThreeDCameraControler>();
    } 


   
}
