using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInAndOutOfCar : MonoBehaviour
{

    private GameObject human;
    TitanfallMovement titanfall;
    // Start is called before the first frame update
    void Start()
    {
        titanfall = GetComponent<TitanfallMovement>();
        human = titanfall.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            titanfall.EnterVehicle();
        }
    }
}
