using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteoriteSpawn : MonoBehaviour
{

    [SerializeField] GameObject meteor;
    [SerializeField] Transform metSpawner;
    [SerializeField] bool canSpawn = true;
    [SerializeField] float randomSeconds;
    [SerializeField] float maxSeconds = 10;


    // Start is called before the first frame update
    void Start()
    {
        randomSeconds = 3;
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(randomSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        if (canSpawn)
        {
            canSpawn = false;
            StartCoroutine(Spawn());
        }
    }

    public IEnumerator Spawn()
    {
        randomSeconds = Random.Range(3,maxSeconds);
        transform.position = new Vector3(3, Random.Range(-2f, 7f), -3);
        yield return new WaitForSeconds(randomSeconds);
        Instantiate(meteor, metSpawner.transform.position, metSpawner.transform.rotation, metSpawner.transform);
        canSpawn = true;
    }

}
