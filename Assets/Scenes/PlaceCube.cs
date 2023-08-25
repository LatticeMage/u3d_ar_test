using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjects : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject rabbitPrefab;
    public GameObject spherePrefab;
    public string websiteURL = "https://xxxx.com";  // Added this line for the URL

    private GameObject rabbitInstance;  // To store a reference to the instantiated rabbit
    private int objectCount = 0;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (objectCount < 2 && touch.phase == TouchPhase.Began)
            {
                CustomLogger.Log("objectCount < 2 && touch.phase == TouchPhase.Began");

                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    if (objectCount == 0)
                    {
                        rabbitInstance = Instantiate(rabbitPrefab, hitPose.position, hitPose.rotation);
                        rabbitInstance.transform.Rotate(-90f, 0f, 180f, Space.Self);
                    }
                    else if (objectCount == 1)
                    {
                        Instantiate(spherePrefab, hitPose.position, hitPose.rotation);
                    }

                    objectCount++;
                }
            }

            if (objectCount == 2 && touch.phase == TouchPhase.Began)
            {
                CustomLogger.Log("objectCount == 2 && touch.phase == TouchPhase.Began");

                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {                                            
                    CustomLogger.Log("Hit: " + hitInfo.transform.parent.gameObject.name);
                    if (hitInfo.transform.parent.gameObject == rabbitInstance)
                    {
                        Application.OpenURL(websiteURL);
                    }
                }
                else
                {
                    CustomLogger.Log("No hit detected");

                }
            }

        }
    }
}
