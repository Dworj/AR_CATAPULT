using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddObject1 : MonoBehaviour
{
    private ProgrammManager1 _programmManager1;
    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        _button= GetComponent<Button>();
        _programmManager1= FindObjectOfType<ProgrammManager1>();
        _button.onClick.AddListener(AddObject);
    }

    void AddObject()
    {
        _programmManager1.ScrolView.SetActive(true);
    }
}
