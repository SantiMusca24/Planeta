using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class shieldToggle : MonoBehaviour
{
    [SerializeField] UniversalRendererData feature1;
    //public Camera _camData;
    public bool shieldOn = true;
    public GameObject shieldObj;
    // Start is called before the first frame update
    void Start()
    {
        shieldOn = true;
        feature1.rendererFeatures[2].SetActive(true);
        feature1.rendererFeatures[3].SetActive(false);
        feature1.rendererFeatures[4].SetActive(false);
        shieldObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            shieldOn = !shieldOn;
            if (shieldOn)
            {
                feature1.rendererFeatures[4].SetActive(true);
                feature1.rendererFeatures[5].SetActive(false);
                shieldObj.SetActive(true);
                //_camData = YourCamera.GetUniversalAdditionalCameraData();
                //_camData.SetRenderer(4); // switch to renderer without/with render feature.
            }
            else
            {
                feature1.rendererFeatures[5].SetActive(true);
                feature1.rendererFeatures[4].SetActive(false);
                shieldObj.SetActive(false);
                //_camData = YourCamera.GetUniversalAdditionalCameraData();
                //_camData.SetRenderer(5); // switch to renderer without/with render feature.
            }
        }
    }
}
