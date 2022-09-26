using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    [SerializeField] GameObject Sonic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
     if (other.gameObject.tag.Equals ("Enemy")) {
        Sonic.SetActive(false);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        Invoke("Respawn", 1f);
     }
   }
   void Respawn() {
        transform.position = new Vector3(0f, 0.55f, -1.7f);
        Sonic.SetActive(true);
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
   }

   void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}
