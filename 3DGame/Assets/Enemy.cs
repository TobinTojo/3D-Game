using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float angle;
    public float distance;
    public float distancex;
    public float distancez;
    public float distancey;
    public Transform player;
    public bool isTouching;
    public GameObject crossHair;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
       distance = (transform.position - player.position).magnitude;
       distancex = Mathf.Abs(transform.position.x - player.position.x);
       distancey = Mathf.Abs(transform.position.y - player.position.y);
       distancez = Mathf.Abs(transform.position.z - player.position.z);
       if (distancex <= 3.5 && distancey <= 3 && distancez <= 3.5)
       {
            isTouching = true;
            if (GameObject.Find("Player").GetComponent<PlayerMovement>().canHomingAttack == true && GameObject.Find("Player").GetComponent<PlayerMovement>().closestEnemy == this.gameObject && angle > 75f && GameObject.Find("Player").GetComponent<PlayerMovement>().onGroundAfterDash == true)
                crossHair.SetActive(true);
            else
                crossHair.SetActive(false);
       }
       else 
       {
            isTouching = false;
            crossHair.SetActive(false);
       }
       Vector3 enemyDir = (player.position - transform.position).normalized;
       angle = Vector3.Angle(player.forward, enemyDir);
    }
}
