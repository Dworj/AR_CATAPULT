using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseObject : MonoBehaviour
{
    // Start is called before the first frame update

    private ProgrammManager _programmManager;
    private Button _button;
    public GameObject ChoosedObject;
    void Start()
    {
        _programmManager = FindObjectOfType<ProgrammManager>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SelectedObject);
    }

  void SelectedObject()
    {
        _programmManager.SpawnObject = ChoosedObject;
        _programmManager.ChooseObject = true;
        _programmManager.ScrollView.SetActive(false);
    }
}
