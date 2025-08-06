using System.Collections;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;


public class DragObject : MonoBehaviour

{
    public float nukeValue = 0;
    public Material nukeMaterial;
    [SerializeField] UniversalRendererData feature1;
    private Vector3 mOffset;
    public bool hasExploded = false;
    public GameObject explosion;
    public Transform explosionPoint;

    private float mZCoord;

    private void Start()
    {
        hasExploded = false;
        nukeValue = 0;
    }
    private void Update()
    {
        nukeMaterial.SetFloat("_disolver", nukeValue);
        if (transform.position.y < -3.8f && !hasExploded)
        {
            hasExploded = true;
            StartCoroutine(Explode());
        }
    }
    public IEnumerator Explode()
    {
        // change nuke shader _disolver
        Instantiate(explosion, explosionPoint);
        while (nukeValue < 1)
        {
            yield return new WaitForSeconds(0.1f);
            nukeValue += 0.1f;
        }
        // spawn explosion prefab at explosion point
        
        //feature1.rendererFeatures[2].SetActive(false);
        feature1.rendererFeatures[4].SetActive(true);
    }

    void OnMouseDown()

    {

        mZCoord = Camera.main.WorldToScreenPoint(

            gameObject.transform.position).z;


        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

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
