using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private UnityFlockController flockController;

    public Vector3 targetPosition;

    public Target targetPrefab;
    private Target targetObject;

    public enum FlockMode
    {
        LAZY,
        CIRCLE,
        FOLLOW
    }

    public static FlockMode currentFlockMode;

    private void Awake()
    {
        if( !instance )
        {
            instance = this;
        }
        else if( instance != this )
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        flockController = GetComponent<UnityFlockController>();
        targetObject = Instantiate(targetPrefab, transform.position, transform.rotation) as Target;

        InitSimulation(FlockMode.LAZY);
    }    

    public void onClick(string buttonTag)
    {
        targetObject.MoveToNewPosition();
        FlockMode flockFromTag = (FlockMode)System.Enum.Parse(typeof(FlockMode), buttonTag, true);
        InitSimulation(flockFromTag);
    }

    void InitSimulation(FlockMode flockMode)
    {
        if (flockMode == currentFlockMode) return;

        currentFlockMode = flockMode;
        
        switch( flockMode )
        {
            case FlockMode.LAZY:
                Debug.Log("Lazy Flock Mode");
                targetObject.MoveToNewPosition();             
                break;
            case FlockMode.FOLLOW:
                Debug.Log("Follow Flock Mode");                
                break;
            case FlockMode.CIRCLE:
                Debug.Log("Circle Flock Mode");
                targetObject.MoveToNextShapePosition();
                break;
        }       
    }

    private void Update()
    {
        FlockTowardsTarget();

        if ( currentFlockMode == FlockMode.LAZY )
        {            
            if ( Vector3.Distance(targetObject.transform.position, flockController.flockCenter) <= 10.0f )
            {                
                targetObject.MoveToNewPosition();
            }
        }
        else if( currentFlockMode == FlockMode.CIRCLE )
        {
            if( Vector3.Distance(targetObject.transform.position, flockController.flockCenter) <= 10.0f )
            {
                targetObject.MoveToNextShapePosition();
            }
        }
        else if( currentFlockMode == FlockMode.FOLLOW )
        {
            if ( Input.GetMouseButtonDown(0) )
            {
                Vector3 v3 = Input.mousePosition;
                v3.z = 150.0f;
                v3 = Camera.main.ScreenToWorldPoint(v3);
                targetObject.MoveToPoint(v3);
            }
        }
    }

    private void FlockTowardsTarget()
    {
        targetPosition = targetObject.transform.position;
        flockController.MoveTowardsTarget(targetObject.transform);
    }
}
