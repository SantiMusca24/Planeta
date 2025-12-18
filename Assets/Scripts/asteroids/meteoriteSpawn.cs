using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class meteoriteSpawn : MonoBehaviour
{
    [SerializeField] TMP_Text timer;
    [SerializeField] GameObject meteor;
    [SerializeField] Transform metSpawner;
    [SerializeField] bool canSpawn = true;
    [SerializeField] float randomSeconds;
    [SerializeField] bool showerActive = false;
    [SerializeField] Transform canvas;
    public int seconds = 60;
    int check;
    public float x;
    //[SerializeField] float minSeconds = 3, maxSeconds = 10;


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
        if (canSpawn && cloud1.meteorsEnable)
        {
            canSpawn = false;
            StartCoroutine(Spawn());
        }
    }
    public void StartShower()
    {
        if (!showerActive)
        {
            seconds = 60;
            showerActive = true;
            StartCoroutine(Shower());
        }
    }

    public IEnumerator Spawn()
    {
        randomSeconds = Random.Range(cloud1.minMeteorWait,cloud1.maxMeteorWait);
        transform.position = new Vector3(x, Random.Range(cloud1.minAsteroidHeight + 5, cloud1.maxAsteroidHeight + 5), -3.53f);
        if (!showerActive) yield return new WaitForSeconds(randomSeconds);
        else yield return new WaitForSeconds(1);
        check = PlayerPrefs.GetInt("Tutorial2", 0);
        if (check != 0) Instantiate(meteor, metSpawner.transform.position, metSpawner.transform.rotation, canvas);
        canSpawn = true;
    }
    public IEnumerator Shower()
    {
        while (seconds >= 0)
        {
            yield return new WaitForSeconds(1);
            seconds--;
            timer.text = "" + seconds;
        }
        timer.text = "";
        showerActive = false;
    }

}
