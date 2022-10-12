using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] GameObject[]  wayPoints;
    [SerializeField] int currentWayPointIndex = 0;
    [SerializeField] Transform body;
    [SerializeField] float speed = 1f;
    [SerializeField] Transform player;
    public float distance;
    private bool isPatrol = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(player.position.x, transform.position.y, player.position.z);
        distance = (transform.position - player.position).magnitude;
        if (isPatrol == true) {
            if (Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].transform.position) < 0.1f)
            {
                currentWayPointIndex++;
                if (currentWayPointIndex >= wayPoints.Length)
                    currentWayPointIndex = 0;
                if (currentWayPointIndex == 1) {
                    if (this.gameObject.layer == 9) {
                        body.localScale = new Vector3(1f, 2f, 1f);
                    }
                }
                else {
                    if (this.gameObject.layer == 9) {
                         body.localScale = new Vector3(-1f, 2f, -1f);
                    }
                }
            }
             transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPointIndex].transform.position, speed * Time.deltaTime);
        }   
        if (distance <= 2.7f && this.gameObject.layer == 9) {
            isPatrol = false;
            transform.LookAt(direction);
            if (currentWayPointIndex == 0 && player.position.z > transform.position.z) {
                body.localScale = new Vector3(-1f, 2f, -1f);
            }
            else if (currentWayPointIndex == 0 && player.position.z < transform.position.z) {
                body.localScale = new Vector3(-1f, 2f, -1f);
            }
            else if (currentWayPointIndex == 1 && player.position.z < transform.position.z) {
                body.localScale = new Vector3(-1f, 2f, -1f);
            }
            else if (currentWayPointIndex == 1 && player.position.z > transform.position.z) {
                body.localScale = new Vector3(-1f, 2f, -1f);
            }
            Vector3 place = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.position = Vector3.MoveTowards(transform.position, place,  1.3f * speed * Time.deltaTime);
        }
        else {
            isPatrol = true; 
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            if (currentWayPointIndex == 1) {
                body.localScale = new Vector3(1f, 2f, 1f);
            }
            else {
                body.localScale = new Vector3(-1f, 2f, -1f);
            }       
        }
    }
}
