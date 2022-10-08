using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugEnemy : MonoBehaviour
{
    [SerializeField] Transform player;
    public float distance;
    public GameObject bullet;
    public Vector3 attackPos;
    [SerializeField] GameObject currentBullet;
    [SerializeField] Vector3 playerPos;
    [SerializeField] bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         distance = (transform.position - player.position).magnitude;
        if (distance <= 4.9f && canShoot == true)
        {
            createBullet();
        }
        currentBullet.transform.position = Vector3.MoveTowards(currentBullet.transform.position, playerPos, 5f * Time.deltaTime);
        if (Vector3.Distance(currentBullet.transform.position, playerPos) < 0.2f)
        {
            Destroy(currentBullet);
            Invoke("Shoot", 2f);
        }
    }

    void createBullet()
    {
        currentBullet = Instantiate(bullet, attackPos, Quaternion.identity);
        playerPos = player.transform.position;
        canShoot = false;
    }
    void Shoot() 
    {
        canShoot = true;
    }
}
