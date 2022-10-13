using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCharacter : MonoBehaviour
{
    public static int Character;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Character = characterSelect.character;
    }
}
