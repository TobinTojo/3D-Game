using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    [SerializeField] AudioClip ring;
    [SerializeField] AudioSource source;
    GameObject orangeCoinPosition;
    // Start is called before the first frame update
    void Start()
    {
        orangeCoinPosition = GameObject.Find("OrangeCoinPos");
        orangeCoinPosition.transform.position =  new Vector3(transform.position.x + Random.Range(-3f, 3f), transform.position.y + 0, transform.position.z + Random.Range(-3f, 3f));
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Orange")
        {
            transform.position = Vector3.MoveTowards(transform.position, orangeCoinPosition.transform.position, 2f * Time.deltaTime);
            if (Vector3.Distance(transform.position, orangeCoinPosition.transform.position) < 0.1f)
            {
                GetComponent<BoxCollider>().enabled = true;
                Invoke("destroyObject", 5f);
            }
        }
    }
    void destroyObject() {
        Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            if (!source.isPlaying)
            {
                source.clip = ring;
                source.Play();
            }
            Invoke("destroyObject", 0.8f);
        }
    }
}
