using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class characterSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public static int character = 1;
    [SerializeField] GameObject modern;
    [SerializeField] GameObject classic;
    public GameObject save;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            character++;
                
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            character--;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DontDestroyOnLoad(save);
            SceneManager.LoadScene("SampleScene");
            
        }
        if (character % 2 != 0)
        {
            character = 1;
            modern.SetActive(true);
            classic.SetActive(false);
        }
        else {
            character = 2;
            modern.SetActive(false);
            classic.SetActive(true);
        }
    }
}
