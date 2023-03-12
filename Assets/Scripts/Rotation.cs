using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    private Button _button;
    private ProgrammManager _programmManager;
    // Start is called before the first frame update
    void Start()
    {
        _programmManager = FindObjectOfType<ProgrammManager>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(RotateObject);
    }

    // Update is called once per frame
    private void RotateObject()
    {
     if (_programmManager.Rotation)
        {
            _programmManager.Rotation = false;
            GetComponent<Image>().color = Color.red; // if rotation disable button will be red
        }

     else 

        {
           _programmManager.Rotation = true;
            GetComponent<Image>().color = Color.green;  // if rotation active button will be red
        }
    } 
}
