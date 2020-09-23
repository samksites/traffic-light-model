using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneButton : MonoBehaviour
{
    [SerializeField]
    GameObject lane;

    public void Clicked()
    {
        gameObject.GetComponentInParent<ButtionPecies>().HideChoseButtion(1);

        Vector3 centerScreen = new Vector3(Camera.main.transform.position.x, .1f, Camera.main.transform.position.z);

        Instantiate(lane, centerScreen, Quaternion.identity);
    }
}
