using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airSpring : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            anim.SetTrigger("Jump");
            source.Play();
        }
    }
}
