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
            if(grabbedObject == null)
            {
                GrabObjectUnderMouse();
            }
            else
            {
                DropObjectUnderMouse();
            }
        }
    }

    public void GrabObjectUnderMouse()
    {
        //Raycast under this shit
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector3.forward);
        for(int i = 0; i < hits.Length; ++i)
        {
            GameObject objectUnderMouse = hits[i].collider.gameObject;
            GrabbableObject grabbable = objectUnderMouse.GetComponent<GrabbableObject>();
            if(grabbable != null)
            {
                grabbedObject = grabbable;
                grabbedObject.transform.parent = transform;
                grabbedObject.transform.position = new Vector3(grabbedObject.transform.position.x, grabbedObject.transform.position.y, this.transform.position.z + 0.1f);
                break;
            }
        }  
    }

    public void DropObjectUnderMouse()
    {
        //Raycast under this shit
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector3.forward);
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
