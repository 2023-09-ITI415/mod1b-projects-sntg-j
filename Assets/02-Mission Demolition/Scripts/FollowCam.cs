using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;
    
    [Header("Set Dynamically")]
    public float camZ;

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    
    void Awake()
    {
        camZ = this.transform.position.z;
    }
    void Start()
    {
        
    }

    void Update() { }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 destination;
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else 
        {
            destination = POI.transform.position;

            if(POI.tag == "Projectile") 
            {
                if(POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }
        }
            
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        destination.y = Mathf.Max(minXY.y, destination.x);
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }
}
