using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddObject : MonoBehaviour
{
    private Button button;
    private ProgrammManager _programmManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _programmManager = FindObjectOfType<ProgrammManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(AddObjectFunction);
    }

    // Update is called once per frame
    void AddObjectFunction()
    {
        _programmManager.ScrollView.SetActive(true);
    }
}
