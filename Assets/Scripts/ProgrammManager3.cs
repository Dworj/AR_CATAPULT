using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ProgrammManager3 : MonoBehaviour
{
    public GameObject ObjectTospawn;
    public bool Rotation;
    public bool ChooseObject = false;
    public GameObject ScrollView;

    List<ARRaycastHit> hits = new List<ARRaycastHit>(); 

    [SerializeField]private GameObject _markerPrefab;
    private Quaternion _yRotation;
    private GameObject _selectedObject;
    [SerializeField] private Camera _arCamera;
    private Vector2 _touchPosition;
    private ARRaycastManager _raycastManager;

    void Start()
    {
        _raycastManager = FindObjectOfType<ARRaycastManager>();
        _markerPrefab.SetActive(false);
        ScrollView.SetActive(false);
    }
    void Update()
    {
     if(ChooseObject)
        {
            ShowMarkerAndSetObject();
        }
        MoveAndRotationObject();
    }


    void ShowMarkerAndSetObject()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        _raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if(hits.Count > 0)
        {
            _markerPrefab.transform.position = hits[0].pose.position;
            _markerPrefab.SetActive(true);
        }

        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
          Instantiate(ObjectTospawn, hits[0].pose.position, ObjectTospawn.transform.rotation);
            ChooseObject = false;
            _markerPrefab.SetActive(false);
        }
    }

    void MoveAndRotationObject()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            _touchPosition =   touch.position;
           if (touch.phase == TouchPhase.Began)
            {
                Ray ray = _arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if(Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.collider.CompareTag("Unselected"))
                    {
                        hitObject.collider.gameObject.tag = "Selected";
                    }
                }
            }




            _selectedObject = GameObject.FindWithTag("Selected");
            if (touch.phase == TouchPhase.Moved && Input.touchCount == 1)
            {
                if (Rotation)
                {
                    _yRotation = Quaternion.Euler(0f, -touch.deltaPosition.x * 0.1f, 0f);
                    _selectedObject.transform.rotation = _yRotation * _selectedObject.transform.rotation;
                }
                else
                {
                    _raycastManager.Raycast(_touchPosition, hits, TrackableType.Planes);
                    _selectedObject.transform.position = hits[0].pose.position;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (_selectedObject.CompareTag("Selected"))
                {
                    _selectedObject.tag = "UnSelected";
                }
            }
        
        }

      
    }
}