using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseObject2 : MonoBehaviour
{
    private ProgrammManager1 _programmManager1;
    private Button _button;

    public GameObject ChooseObject;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _programmManager1 = GetComponent<ProgrammManager1>();
        _button.onClick.AddListener(ChoseObject);
        
    }

   void ChoseObject()
    {
        _programmManager1.ObjectToSPawn = ChooseObject;
        _programmManager1.ChooseObject = true;
        _programmManager1.ScrolView.SetActive(true);
    }
}
