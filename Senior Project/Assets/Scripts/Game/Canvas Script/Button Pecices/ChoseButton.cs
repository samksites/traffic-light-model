using UnityEngine;

public class ChoseButton : MonoBehaviour
{
  public void Clicked()
    {
        gameObject.GetComponentInParent<ButtionPecies>().HideChoseButtion(0);
    }
}
