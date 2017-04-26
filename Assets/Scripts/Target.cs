using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public Vector3 bound;
    public float speed = 100.0f;

    private Vector3 initialPosition;

    private Queue<Vector3> shapeQueue = new Queue<Vector3>();

    // Use this for initialization
    void Start () {
        initialPosition = transform.position;                
	}
	
	public void MoveToNewPosition()
    {
        float posX = Random.Range(initialPosition.x - bound.x, initialPosition.x + bound.x);
        float posY = Random.Range(initialPosition.y - bound.y, initialPosition.y + bound.y);
        float posZ = Random.Range(initialPosition.z - bound.z, initialPosition.z + bound.z);

        transform.position = new Vector3(posX, posY, posZ);
    }

    public void MoveToPoint(Vector3 position)
    {
        float xMin = initialPosition.x - bound.x;
        float xMax = initialPosition.x + bound.x;
        position.x = Mathf.Clamp(position.x, xMin, xMax);

        float yMin = initialPosition.y - bound.y;
        float yMax = initialPosition.y + bound.y;
        position.y = Mathf.Clamp(position.y, yMin, yMax);

        float zMin = initialPosition.z - bound.z;
        float zMax = initialPosition.z + bound.z;
        position.z = Mathf.Clamp(position.z, yMin, zMax);

        transform.position = position;
    }

    public void MoveToNextShapePosition()
    {        
        if( shapeQueue.Count == 0 )
        {
            populateShapeQueue();
        }
        
        transform.position = shapeQueue.Dequeue();
    }


    private void populateShapeQueue()
    {
        float posZ = 150.0f;
        float xMin = initialPosition.x - bound.x;
        float xMax = initialPosition.x + bound.x;        

        float yMin = initialPosition.y - bound.y;
        float yMax = initialPosition.y + bound.y;
        
        Vector3 p1 = new Vector3(xMin, (yMax - yMin) / 2, posZ);
        Vector3 p2 = new Vector3(xMin + 20, (yMax - yMin) / 2, posZ);
        Vector3 p3 = new Vector3(xMin + 40, (yMax - yMin) / 2, posZ);
        Vector3 p4 = new Vector3(xMin + 60, (yMax - yMin) / 2, posZ);
        Vector3 p5 = new Vector3(xMin + 80, (yMax - yMin) / 2, posZ);
        Vector3 p6 = new Vector3(xMin + 100, (yMax - yMin) / 2, posZ);
        Vector3 p7 = new Vector3(xMin + 120, (yMax - yMin) / 2, posZ);

        shapeQueue.Enqueue(p1);
        shapeQueue.Enqueue(p2);
        shapeQueue.Enqueue(p3);
        shapeQueue.Enqueue(p4);
        shapeQueue.Enqueue(p5);
        shapeQueue.Enqueue(p6);
        shapeQueue.Enqueue(p7);

    }
}
