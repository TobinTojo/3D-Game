using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    [SerializeField] GameObject Sonic;
    [SerializeField] GameObject Modern;
    [SerializeField] GameObject Classic;
    public static int health;
    [SerializeField] GameObject shield;
    public AudioClip Hurt1;
    public AudioClip Hurt2;
    public AudioClip coinHurt;
    public AudioClip coinDeath;
    public AudioSource source;
    public AudioSource coinSource;
    [SerializeField] GameObject OrangeRing;
    public static int myCoin;
    bool isInvincible = false;
    // Start is called before the first frame update
    void Start()
    {
        health = 1;
        if (SaveCharacter.Character == 1)
        {
            Sonic = Modern;
            Classic.SetActive(false);
            Modern.SetActive(true);
        }
        else
        {
            Sonic = Classic;
            Classic.SetActive(true);
            Modern.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
     if (other.gameObject.tag.Equals ("Enemy") && GameObject.Find("Player").GetComponent<PlayerMovement>().isHoming == false) {
        if (!isInvincible)
            health -= 1;
        if (health == 0)
        {
            if (PlayerMovement.coins / 4 == 0)
            {
                if (PlayerMovement.coins == 0)
                {
                    Sonic.SetActive(false);
                    if (SaveCharacter.Character == 1)
                    {
                        source.clip = Hurt2;
                        source.Play();
                    }
                    coinSource.clip = coinDeath;
                    coinSource.Play();
                    GetComponent<PlayerMovement>().enabled = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    PlayerMovement.coins = 0;
                    Invoke("ReloadLevel", 1f);
                }
                else
                {
                    health = 1;
                    if (SaveCharacter.Character == 1)
                    {
                        source.clip = Hurt1;
                        source.Play();
                    }
                    coinSource.clip = coinHurt;
                    coinSource.Play();
                    PlayerMovement.coins = 0;
                    StartCoroutine("Invincible");
                }
            }
            else
            {
                myCoin = PlayerMovement.coins /= 4;
                PlayerMovement.coins = 0;
                if (SaveCharacter.Character == 1)
                {
                    source.clip = Hurt1;
                    source.Play();
                }
                coinSource.clip = coinHurt;
                coinSource.Play();
                health = 1;
                GameObject orangeCoin = Instantiate(OrangeRing, transform.position, Quaternion.identity);
                StartCoroutine("Invincible");
            }
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

   public void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
   IEnumerator Invincible()
   {
        isInvincible = true;
        Sonic.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Sonic.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Sonic.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Sonic.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Sonic.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Sonic.SetActive(true);
        isInvincible = false;
   }
}
