using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crabEnemy : MonoBehaviour
{
    [SerializeField] GameObject[]  wayPoints;
    [SerializeField] int currentWayPointIndex = 0;
    [SerializeField] float speed = 1f;
    [SerializeField] string enemyType;
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
                if (this.gameObject.layer == 10) {
                    if (enemyType == "Front-Back")
                        transform.localScale = new Vector3(1f, 0.5f, 1f);
                }
            }
            else {
                if (this.gameObject.layer == 10) {
                    if (enemyType == "Front-Back")
                        transform.localScale = new Vector3(-1f, 0.5f, -1f);
                }
            }
        }    
         transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPointIndex].transform.position, speed * Time.deltaTime);
    }
}
