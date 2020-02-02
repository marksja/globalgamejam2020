using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    CircuitPiece grabbedObject;
    

    // Update is called thrice per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(grabbedObject == null)
            {
                GrabObjectUnderMouse();
            }
            else
            {
                DropObjectUnderMouse();
            }
        }
        if(grabbedObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            for(int i = 0; i < hits.Length; ++i)
            {
                GameObject objectUnderMouse = hits[i].collider.gameObject;
                if(objectUnderMouse.name == "Just a big plane")
                {
                    grabbedObject.transform.position = hits[i].point;
                }
            }
        }
    }

    public void GrabObjectUnderMouse()
    {
        //Raycast under this shit
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        for(int i = 0; i < hits.Length; ++i)
        {
            GameObject objectUnderMouse = hits[i].collider.gameObject;
            CircuitPiece grabbable = objectUnderMouse.GetComponent<CircuitPiece>();
            if(grabbable != null && !grabbable.disableGrabbing)
            {
                grabbedObject = grabbable;
                grabbedObject.transform.parent = transform;
                //grabbedObject.transform.position = new Vector3(grabbedObject.transform.position.x, grabbedObject.transform.position.y, this.transform.position.z + 0.1f);
                break;
            }
        }  
    }

    public void DropObjectUnderMouse()
    {
        //Raycast under this shit
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        for(int i = 0; i < hits.Length; ++i)
        {
            GameObject objectUnderMouse = hits[i].collider.gameObject;
            PlaceableGrid placeableGrid = objectUnderMouse.GetComponent<PlaceableGrid>();
            if(placeableGrid != null)
            {
                if(placeableGrid.PlaceObject(grabbedObject, grabbedObject.transform.position))
                {            
                    grabbedObject = null;
                    break;
                }
                else
                {
                    Debug.Log("Placement failed");
                }
            }
        }  
    }
}
