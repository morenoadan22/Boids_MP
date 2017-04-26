using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityFlockController : MonoBehaviour {        
    public float speed = 100.0f;
    public int flockSize = 5;
             
    public UnityFlock prefab;
    
    internal Vector3 flockCenter;
    internal Vector3 flockVelocity;
    private Vector3 nextMovementPoint;
            
    public ArrayList flockList = new ArrayList();    

    // Use this for initialization
    void Start () {                       
        for (int i = 0; i < flockSize; i++)
        {
            UnityFlock flock = Instantiate(prefab, transform.position, transform.rotation) as UnityFlock;
            flock.transform.parent = transform;
            flock.controller = this;
            flockList.Add(flock);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 center = Vector3.zero;
        Vector3 velocity = Vector3.zero;

        foreach (UnityFlock flock in flockList)
        {
            center += flock.transform.localPosition;
            velocity += flock.rigidBody.velocity;
        }

        flockCenter = center / flockSize;
        flockVelocity = velocity / flockSize;

        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextMovementPoint - transform.position), 1.0f * Time.deltaTime);                   
	}

    public void MoveTowardsTarget(Transform targetTransform)
    {
        nextMovementPoint = targetTransform.position;
    }
}
