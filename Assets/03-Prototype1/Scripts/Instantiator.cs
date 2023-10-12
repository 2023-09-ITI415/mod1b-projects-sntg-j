using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public GameObject prefab;
    public GameObject spikePrefab;
    public int count = 12;

    private int preFabInstances = 0;
    private float radius = 600f;
    private float maxRadius;
    private float minRadius = 1.0f;
    private Vector3 spin = new Vector3(0, 10, 0);
    private float growTime = 6.0f;
    private float yLocale = 0.5f;
    private float zLocale;
    private float xLocale;
    private float angle;
    private bool pulsing = false;

    // Start is called before the first frame update
    public int getInstanceNums() { return preFabInstances; }
    void Start()
    {
        maxRadius = radius;
        prefab.GetComponent<GameObject>();
        posFind();
    }
    private void posFind()
    {
        angle = Mathf.Deg2Rad * (360.0f / (float)count); // finds the sections among all instances around 360 degrees and preps for proper locations converts to radians
        for (int i = 0; i < count; i++)
        {

            xLocale = (radius) * Mathf.Cos(i * angle);
            zLocale = (radius) * Mathf.Sin(i * angle);
            Vector3 position = new Vector3(xLocale, yLocale, zLocale);
            if (i % 2 == 0) {
                GameObject.Instantiate<GameObject>(prefab, position, Quaternion.identity, GameObject.FindGameObjectWithTag("PickUpParent").transform);
                preFabInstances++;
            }
            else
            {
                GameObject.Instantiate<GameObject>(spikePrefab, position, Quaternion.identity, GameObject.FindGameObjectWithTag("PickUpParent").transform);
            }
        }
    }
    void Update()
    {
        transform.Rotate(spin * Time.deltaTime);
        if (pulsing == true)
        {
            StartCoroutine(Pulse1());
        }
    }
    public void Pulse()
    {
        pulsing = true;
    }
    private IEnumerator Pulse1() 
    {
        float timer = 0f;
        while(timer < growTime)
        {
            radius = math.lerp(maxRadius, minRadius, timer/growTime);
            timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Pulse2(pulsing));
    }
    private IEnumerator Pulse2()
    {
        float timer = 0f;

        while (timer < growTime)
        {
            radius = math.lerp(minRadius, maxRadius, timer / growTime);;
            timer += Time.deltaTime;
            yield return null;
        }
        pulsing = false;
    }
}
