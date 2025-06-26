using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering.Universal;


public class DragObject : MonoBehaviour

{
    [SerializeField] UniversalRendererData feature1;
    private Vector3 mOffset;


    private float mZCoord;


    void OnMouseDown()

    {

        mZCoord = Camera.main.WorldToScreenPoint(

            gameObject.transform.position).z;


        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

    }
    void Start()
    {
        feature1.rendererFeatures[4].SetActive(false);
        feature1.rendererFeatures[5].SetActive(false);
    }

    private Vector3 GetMouseAsWorldPoint()

    {

        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;


        // z coordinate of game object on screen

        mousePoint.z = mZCoord;


        // Convert it to world points

        return Camera.main.ScreenToWorldPoint(mousePoint);

    }


    void OnMouseDrag()

    {

        transform.position = GetMouseAsWorldPoint() + mOffset;

    }

}
