using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] GameObject[]  wayPoints;
    int currentWayPointIndex = 0;
    [SerializeField] Transform body;
    [SerializeField] float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].transform.position) < 0.1f)
        {
            currentWayPointIndex++;
            if (currentWayPointIndex >= wayPoints.Length)
                currentWayPointIndex = 0;
            if (currentWayPointIndex == 1) {
                if (this.gameObject.layer == 9) {
                    body.eulerAngles = new Vector3(0f, 210f, 0f);
                }
            }
            else {
                if (this.gameObject.layer == 9) {
                    body.eulerAngles = new Vector3(0f, 30f, 0f);
                }
            }
        } 
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPointIndex].transform.position, speed * Time.deltaTime);

    }
}
