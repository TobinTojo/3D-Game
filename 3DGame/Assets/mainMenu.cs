using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject characterSelect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        menu.SetActive(false);
        characterSelect.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
