using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation2 : MonoBehaviour
{
    private Button _button;
    private ProgrammManager1 _programmManager1;
    // Start is called before the first frame update
    void Start()
    {
        _programmManager1 = FindObjectOfType<ProgrammManager1>();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(Rotate);
    }
    void Rotate()
    {
        if (_programmManager1.Rotation)
        {
            _programmManager1.Rotation = false; // если вращение отключенно цвет кнопки меняется на красный
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            _programmManager1.Rotation = true; // если вращение включено то цвет кнопки меняется на зелёный
            GetComponent<Image>().color = Color.green;
        }
    }
}
