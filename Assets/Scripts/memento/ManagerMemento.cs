using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMemento : MonoBehaviour
{
    public List<Rewind> allRewinds;

    //Tomo todos mis objetos que hereden de Rewind, que son los que voy a "recordar" cuando presione la tecla
    void Start()
    {
        allRewinds = new List<Rewind>();
        var listTemp = FindObjectsOfType<Rewind>();

        foreach (var item in listTemp)
        {
            allRewinds.Add(item);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var item in allRewinds)
            {
                item.Action();
            }
        }
    }
}
