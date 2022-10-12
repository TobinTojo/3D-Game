using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rail : MonoBehaviour
{
    [SerializeField] Transform player;
    public float xaxis;
    [SerializeField] AudioSource source;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (PlayerMovement.rail1 == true)
            //xaxis = -3.929106f;
        //else if (PlayerMovement.rail2 == true)
            //xaxis = -10.14f;
        //if (PlayerMovement.isRailGrinding == true)
        //{
             //player.position = new Vector3(xaxis, player.position.y, player.position.z);
        //}
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.name.Equals("Player"))
        {
            source.Play();
        }
    }

   void OnCollisionExit(Collision other)
   {
       if (other.gameObject.name.Equals("Player"))
        {
            source.Stop();
        } 
   }
}
