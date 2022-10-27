using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float angle;
    public float distance;
    public Transform player;
    public bool isTouching;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
       distance = (transform.position - player.position).magnitude;
       if (distance <= 4)
        isTouching = true;
       else 
        isTouching = false;
       Vector3 enemyDir = (player.position - transform.position).normalized;
       angle = Vector3.Angle(player.forward, enemyDir);
    }
}
