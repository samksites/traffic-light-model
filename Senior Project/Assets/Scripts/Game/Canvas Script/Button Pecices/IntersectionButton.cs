using UnityEngine;

public class IntersectionButton : MonoBehaviour
{
    [SerializeField]
    GameObject intersection;

    // if the spawn lawn button is pressed
    public void Clicked()
    {
        gameObject.GetComponentInParent<ButtionPecies>().HideChoseButtion(1);

        Vector3 centerScreen = new Vector3(Camera.main.transform.position.x, .1f, Camera.main.transform.position.z);

        Instantiate(intersection, centerScreen, Quaternion.identity);
    }
}
