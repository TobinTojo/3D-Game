using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    [SerializeField] AudioClip ring;
    [SerializeField] AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void destroyObject() {
        Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            source.clip = ring;
            source.Play();
            Invoke("destroyObject", 0.8f);
        }
    }
}
