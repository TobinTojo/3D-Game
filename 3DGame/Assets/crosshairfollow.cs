using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshairfollow : MonoBehaviour
{
    public Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(enemy.position.x, enemy.position.y + 0.2f, enemy.position.z - 0.3f);
    }
}
