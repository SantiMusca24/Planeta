using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class POI : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform poiTransform;
    [SerializeField] private GameObject uiPanel;
    //[SerializeField] public Collider button, hideBox;
    [SerializeField] private bool hideTrigger = false;

    [SerializeField] private float detectionAngle = 15f; 

    void Update()
    {
        if (!hideTrigger)
        {
            Vector3 directionToPOI = (poiTransform.position - cameraTransform.position).normalized;
            float angle = Vector3.Angle(cameraTransform.forward, directionToPOI);

            if (angle < detectionAngle)
            {
                if (!uiPanel.activeSelf)
                    uiPanel.SetActive(true);
            }
            else
            {
                if (uiPanel.activeSelf)
                    uiPanel.SetActive(false);
            }
        }       
    }

    private void OnTriggerEnter(Collider other)
    {       
        if (other.name == "HIDEBOX") hideTrigger = true;
        Debug.Log("tocando collider");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "HIDEBOX") hideTrigger = false;
        Debug.Log("no tocando collider");
    }

}
