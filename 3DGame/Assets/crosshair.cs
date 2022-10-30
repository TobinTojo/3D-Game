using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    int counter = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }
    public void switchPos()
    {
        Debug.Log(transform.position);
        if (counter % 2 == 0)
            transform.position = new Vector3(0.0f, 5.1f, 39.4f);
        if (counter % 2 == 1) 
            transform.position = new Vector3(0.0f, 5.1f, 45.1f);
        counter++;
    }
}
