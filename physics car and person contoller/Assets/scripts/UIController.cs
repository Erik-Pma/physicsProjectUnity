using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]TMP_Text timer;
    [SerializeField] Image centerdot;
    [SerializeField] Image[] imageLine;
    [SerializeField] Material enemyMat;
    [SerializeField] Material defaultMat;
    [SerializeField] Material friendlyMat;
    // Start is called before the first frame update
    void Start()
    {
      timer = GetComponent<TMP_Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < imageLine.Length; x++) 
        {
            
        }
    }
}
