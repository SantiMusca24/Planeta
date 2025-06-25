using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class meteorFade : MonoBehaviour
{
    
    //private disolve = 1f;
    private float change = 0.1f;
    public Material disolve;
    public float someValue = 0;
    public GameObject stigma;
    public bool begin = true;

    // Start is called before the first frame update

    private void Start()
    {
        begin = true;
        someValue = 0;
    }

    /*
    void Changeshader()
    {
        if (shaderValue <= 0f)
        {
            CancelInvoke();
        }

        else if (door.GetComponent<MeshRenderer>().material = changeDisolve)
        {
            shaderValue -= change;
            changeDisolve.SetFloat("Visibility", shaderValue);
        }
    }
    */
    void Update()
    {

        if (someValue >= 1)
        {
            someValue = 0;
        }

        stigma.GetComponent<Renderer>().material.SetFloat("_disolver", someValue);

        //coroutines aren't exclusive, so this makes sure it is only run one time
        if (begin == true)
        {
            //calls the coroutine
            StartCoroutine("Decrease");
            begin = false;
        }
    }

    //coroutine decleration
    IEnumerator Decrease()
    {

        while (someValue < 1)
        {
            someValue += 00.05f;
            yield return new WaitForSeconds(0.05f);
        }
    }
    
}


