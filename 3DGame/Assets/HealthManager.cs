using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    [SerializeField] GameObject Sonic;
    public static int health;
    [SerializeField] GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        health = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
     if (other.gameObject.tag.Equals ("Enemy")) {
        health -= 1;
        if (health == 0)
        {
               Sonic.SetActive(false);
               GetComponent<PlayerMovement>().enabled = false;
               GetComponent<Rigidbody>().isKinematic = true;
               Invoke("Respawn", 1f);
        }
        else
        {
               shield.SetActive(false);
        }
     }
   }
   void Respawn() {
        health = 1;
        transform.position = new Vector3(0f, 0.55f, -1.7f);
        Sonic.SetActive(true);
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
   }

   void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}
