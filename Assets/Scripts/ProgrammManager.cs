using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ProgrammManager : MonoBehaviour
{
    [Header("Put your planeMarker here")]

    [SerializeField] private GameObject PlaneMarkerPrefab;

    private Vector2 TouchPosition;

    [SerializeField] private Camera _arCamera;

    private GameObject _selectedObject;

    private ARRaycastManager ARRaycastManagerScript;

    private Quaternion _rotation;

    public bool ChooseObject = false;

    public GameObject ScrollView;

    public bool Rotation;

    public GameObject SpawnObject;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

   
   
    void Start()
    {
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
        ScrollView.SetActive(false);
        PlaneMarkerPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ChooseObject)
        {
            ShowMarker_SetObject();
        }
        MoveObject_Rotation();
    }

    void ShowMarker_SetObject()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        // set marker
        if (hits.Count > 0)
        {
            PlaneMarkerPrefab.transform.position = hits[0].pose.position;
            PlaneMarkerPrefab.SetActive(true);
        }

        // set  object
        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Instantiate(SpawnObject, hits[0].pose.position, SpawnObject.transform.rotation);
            ChooseObject = false;
            PlaneMarkerPrefab.SetActive(false);
        }
    }
    void MoveObject_Rotation()
    {
        if (Input.touchCount > 0)              // checking the screen touch check

        {
            Touch touch = Input.touches[0];
            TouchPosition = touch.position;

            if(touch.phase == TouchPhase.Began) // make Selected only of touch
            {
                Ray ray = _arCamera.ScreenPointToRay(touch.position);// take the position of finger on screen
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {                                                           //if the intersected object
                                                                            
                    if (hitObject.collider.CompareTag("Unselected"))         // has a tag Unselected   
                    {                                                      
                        hitObject.collider.gameObject.tag = "Selected";     // then put tag Selected
                    }
                }
        
            
            }
            _selectedObject = GameObject.FindWithTag("Selected");

            if (touch.phase == TouchPhase.Moved && Input.touchCount == 1)
            {
                if (Rotation)
                {
                    _rotation = Quaternion.Euler(0f, -touch.deltaPosition.x * 0.1f, 0f); //adding rotation with each swipe
                    _selectedObject.transform.rotation = _rotation;
                }
                else
                {
                    ARRaycastManagerScript.Raycast(TouchPosition, hits, TrackableType.Planes); // fix the points where the area intersects
                    _selectedObject = GameObject.FindWithTag("Selected"); // if object heve tag Selected  we can move it 
                }
              
            }

            if(touch.phase == TouchPhase.Ended) // If the finger is removed from the screen
            {                                               // If the finger is removed from the screen
                if (_selectedObject.CompareTag("Selected")) // the tag Selected
                {                                           //  changes to UnSelected  and it cannot be moved
                    _selectedObject.tag = "UnSelected"; //  changes to UnSelected  and it cannot be moved
                }
            }

        }

    }
}
