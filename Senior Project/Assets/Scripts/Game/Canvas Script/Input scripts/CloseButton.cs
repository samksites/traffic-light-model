
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public void ClosePannel()
    {
        transform.parent.gameObject.SetActive(false);
    }
  
}
