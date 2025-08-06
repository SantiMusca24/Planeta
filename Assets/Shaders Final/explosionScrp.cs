using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScrp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explode());
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
