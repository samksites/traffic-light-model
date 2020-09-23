using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Distance_Canvas : MonoBehaviour
{

    private TextMeshProUGUI textDistances;
    // Start is called before the first frame update

    void Start()
    {
        textDistances = GetComponent<TextMeshProUGUI>();
    }
    
    public void ChangeText(string distances)
    {
        textDistances.text = distances;
    }
}
