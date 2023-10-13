using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Instantiator : MonoBehaviour
{
    public GameObject prefab;
    public GameObject spikePrefab;
    public int count = 12;

    private int preFabInstances = 0;
    private float radius = 6f;
    private float minRadius = 1.0f;
    private Vector3 spin = new Vector3(0, 10, 0);
    private GameObject[] preFabArray;
    private Vector3[] StartPos;
    private float growTime = 2.0f;
    private float yLocale = 0.5f;
    private float zLocale;
    private float xLocale;
    private float angle;
    bool pulsing = false;

    // Start is called before the first frame update
    public int getInstanceNums() { return preFabInstances; }
    void Start()
    {
        StartPos = new Vector3[count];
        preFabArray = new GameObject[count];
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
            StartPos[i] = position;
            if (i % 2 == 0) {
                preFabArray[i] = GameObject.Instantiate<GameObject>(prefab, position, Quaternion.identity, GameObject.FindGameObjectWithTag("PickUpParent").transform);
                preFabInstances++;
            }
            else
            {
                preFabArray[i] = GameObject.Instantiate<GameObject>(spikePrefab, position, Quaternion.identity, GameObject.FindGameObjectWithTag("PickUpParent").transform);
            }
        }
    }

    void Update()
    {
        transform.Rotate(spin * Time.deltaTime);
        if(pulsing == true)
        {
            pulsing = false;
            Pulse();
        }
    }
    public void Pulse() {
        StartCoroutine(Pulse1());
    }

    private Vector3[] calcRad(GameObject[] P, float startRad, float minRad)
    {
        Vector3[] res = new Vector3[P.Length];
        for (int i = 0; i< P.Length; i++) {
            res[i] = new Vector3((P[i].transform.localPosition.x / startRad) * minRad, P[i].transform.localPosition.y, (P[i].transform.localPosition.z / startRad) * minRad);
        }
        return res;
    }
    private IEnumerator Pulse1() 
    {
        float timer = 0f;
        
        while(timer < growTime)
        {
            Vector3[] spkPos = calcRad(preFabArray, radius, minRadius);
            for(int i = 0; i< preFabArray.Length; i++)
            {
                preFabArray[i].transform.localPosition = Vector3.Lerp(preFabArray[i].transform.localPosition, spkPos[i], timer/growTime);
            }
            
            timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Pulse2());
    }
    private IEnumerator Pulse2()
    {
        float timer = 0f;

        while (timer < growTime)
        {
            for (int i = 0; i < preFabArray.Length; i++)
            {
                preFabArray[i].transform.localPosition = Vector3.Lerp(preFabArray[i].transform.localPosition, StartPos[i], timer / growTime);
            }
            timer += Time.deltaTime;
            yield return null;
        }
        pulsing = true;
    }
}
