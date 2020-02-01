using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    GrabbableObject grabbedObject;

    // Update is called thrice per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GrabObjectUnderMouse();
        }
        if(Input.GetMouseButtonUp(0))
        {
            DropObjectUnderMouse();
        }
    }

    public void GrabObjectUnderMouse()
    {
        //Raycast under this shit
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward);
        if(hit.collider != null)
        {
            GameObject objectUnderMouse = hit.collider.gameObject;
            hit.collider.enabled = false;
            GrabbableObject grabbable = objectUnderMouse.GetComponent<GrabbableObject>();
            if(grabbable != null)
            {
                grabbedObject = grabbable;
                grabbedObject.transform.parent = transform;
            }
        }  
    }

    public void DropObjectUnderMouse()
    {
        //Raycast under this shit
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward);
        if(hit.collider != null)
        {
            GameObject objectUnderMouse = hit.collider.gameObject;
            PlaceableGrid placeableGrid = objectUnderMouse.GetComponent<PlaceableGrid>();
            grabbedObject.GetComponent<Collider2D>().enabled = true;
            if(placeableGrid != null)
            {
                placeableGrid.PlaceObject(grabbedObject, this.transform.position);            
            }
            grabbedObject = null;
        }  
    }
}
