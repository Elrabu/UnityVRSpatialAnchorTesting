using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTesting : MonoBehaviour
{

    public GameObject markerPrefab; //create space for the prefab to be put here
    public GameObject visualIndicatorPrefab;

    private GameObject floatingIndicator;
    
    void Start()
    {
        
        Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch); //create controller Position and Rotation
        Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

        // Create the floating indicator to follow the controller's position
        floatingIndicator = Instantiate(visualIndicatorPrefab, controllerPosition, controllerRotation); //create a 3d Object at the position/rotation
        floatingIndicator.transform.localScale = new Vector3( 1, 1, 1); //scale it to 1:1:1
    }

    void Update()
    {
        Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch); // Get controller position
        Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch); // Get controller rotation

        // Update the floating indicator's position and rotation
        floatingIndicator.transform.position = controllerPosition;
        floatingIndicator.transform.rotation = controllerRotation;
    }

    public void CreateTestMessage()
    {
        print("This is a test message");
    }

    public void CreateSpatialAnchorMethod()
    {
         StartCoroutine(CreateSpatialAnchor());
    }

    IEnumerator CreateSpatialAnchor()
{
    // Get the position and rotation of the right-hand controller (in world space)
    Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch); // Right controller
    Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
    
    var go = new GameObject(); // New GameObject
    go.transform.position = controllerPosition;
    go.transform.rotation = controllerRotation;

    
    var anchor = go.AddComponent<OVRSpatialAnchor>(); //initialize the spatial anchor

    // Wait for the async creation of the anchor
    yield return new WaitUntil(() => anchor.Created);

    //check if prefab ist there (not null)
    if (markerPrefab != null)
    {
        
        GameObject marker = Instantiate(markerPrefab, anchor.transform.position, anchor.transform.rotation);

        marker.transform.localScale = new Vector3(1, 1, 1);

        marker.transform.SetParent(anchor.transform, false);

        // Reset local position and rotation to avoid offset issues
        marker.transform.localPosition = Vector3.zero; //reset local position, 
        marker.transform.localRotation = Quaternion.identity;

    }
}

}
