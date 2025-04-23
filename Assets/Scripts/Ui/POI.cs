using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform poiTransform;
    [SerializeField] private GameObject uiPanel; 

    [SerializeField] private float detectionAngle = 15f; 

    void Update()
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
