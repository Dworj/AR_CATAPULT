using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ProgrammManager1 : MonoBehaviour
{
    public GameObject ScrolView;
    public bool Rotation;
    public bool ChooseObject = false;
    public GameObject MaketShell;
    public GameObject ObjectToSPawn;

    [SerializeField] private GameObject _markerPrefab;
    [SerializeField] private Camera _arCamera;
    private Vector2 _touchPosition;
    private Quaternion _yRotation;
    private GameObject _selectedObject;
    private ARRaycastManager _raycastManager;
 
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
   
    
    void Start()
    {
     _raycastManager= GetComponent<ARRaycastManager>();
        _markerPrefab.SetActive(false);
        ScrolView.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    { 
     if (ChooseObject)

        {
            ShowMarkeAndSetObject();
        }  

     MoveAndRotateObject();
    }

    void ShowMarkeAndSetObject()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        _raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes); // чтобы объект появлялся ровно по центру экрана

        if (hits.Count > 0 ) // появление маркера на экране

        {
            _markerPrefab.transform.position = hits[0].pose.position;
            _markerPrefab.SetActive(true);
        }   

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) // установка объкстов

        {
            Instantiate(ObjectToSPawn, hits[0].pose.position, ObjectToSPawn.transform.rotation);
            ChooseObject = false;
            _markerPrefab.SetActive(false);
        }
        
    }
    void MoveAndRotateObject()
    {
       if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            _touchPosition= touch.position;
           
            if (touch.phase== TouchPhase.Began)
            {
                Ray ray = _arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;


                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.collider.CompareTag("Unselected"))
                    {
                        hitObject.collider.gameObject.tag = "Selecred";
                    }
                }
            }
            _selectedObject = GameObject.FindWithTag("Selected");

            if(touch.phase == TouchPhase.Moved && Input.touchCount == 1)
            {
                if (Rotation)
                    // вращение объекта с поиощью пальца
                {
                    _yRotation = Quaternion.Euler(0f, -touch.deltaPosition.x * 0.1f,0f);
                    _selectedObject.transform.rotation = _yRotation * _selectedObject.transform.rotation;
                }
                else
                {
                    _raycastManager.Raycast(_touchPosition, hits, TrackableType.Planes);
                    _selectedObject.transform.position = hits[0].pose.position;
                }
                if(touch.phase == TouchPhase.Ended)
                {
                    if (_selectedObject.CompareTag("Selected"))
                    {
                        _selectedObject.tag = "Unselected";
                    }
                }
            }
        }
        
    }

}
