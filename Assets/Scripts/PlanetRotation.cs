using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    static public bool touch = false;

    void Update()
    {
        
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (touch)
        {
            touch = false;
            rotationSpeed = 0;
            StartCoroutine(IStop());
            Debug.Log("llamado");
        }
    }

    public IEnumerator IStop()
    {
        Debug.Log("skibidi");
        yield return new WaitForSeconds(3);
        while (rotationSpeed < 10)
        {
            rotationSpeed = rotationSpeed + 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("speed" + rotationSpeed);
        
    }

}
