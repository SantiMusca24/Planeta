using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class meteorFade : MonoBehaviour
{
    //private disolve = 1f;
    private float change = 0.1f;
    public Material myMaterial;
    public float someValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial.SetFloat(“speed”, someValue);
    }

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

    void Update()
    {
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

        while (myVariable > 0)
        {
            myVariable -= 1;
            yield return new WaitForSeconds(0.1f);
        }
    }

}


