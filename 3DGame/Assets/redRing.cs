using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class redRing : MonoBehaviour
{
    [SerializeField] AudioClip ring;
    [SerializeField] AudioSource source;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] MeshRenderer mesh2;
    [SerializeField] Color normalColor;
    [SerializeField] Image img;
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
            mesh.enabled = false;
            mesh2.enabled = false;
            img.color = normalColor;
            source.clip = ring;
            source.Play();
            Invoke("destroyObject", 0.8f);
        }
    }
}
