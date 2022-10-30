using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crabEnemy : MonoBehaviour
{
    [SerializeField] GameObject[]  wayPoints;
    [SerializeField] int currentWayPointIndex = 0;
    [SerializeField] float speed = 1f;
    [SerializeField] string enemyType;
    public GameObject Claw1;
    public GameObject Claw2;
    public crosshair ch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerMovement>().isHoming == true)
        {
            this.gameObject.tag = "EnemyHead";
            Claw1.tag = "EnemyHead";
            Claw2.tag = "EnemyHead";
        }
        else 
        {
            this.gameObject.tag = "Enemy";
            Claw1.tag = "Enemy";
            Claw2.tag = "Enemy";
        }
        if (Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].transform.position) < 0.1f)
        {
            currentWayPointIndex++;
            if (currentWayPointIndex >= wayPoints.Length)
                currentWayPointIndex = 0;
            if (currentWayPointIndex == 1) {
                if (this.gameObject.layer == 10) {
                    if (enemyType == "Front-Back")
                    {
                        transform.localScale = new Vector3(1f, 0.5f, 1f); 
                        ch.switchPos();
                    }
                }
            }
            else {
                if (this.gameObject.layer == 10) {
                    if (enemyType == "Front-Back")
                    {
                        transform.localScale = new Vector3(-1f, 0.5f, -1f);
                        ch.switchPos();
                    }
                        
                }
            }
        }    
         transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPointIndex].transform.position, speed * Time.deltaTime);
    }
}
