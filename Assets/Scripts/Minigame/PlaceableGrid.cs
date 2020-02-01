using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableGrid : MonoBehaviour
{
    public GridBuilder underlyingGrid;

    public bool PlaceObject<T>(T grabbable, bool checkForEmpty) where T : Component
    {
        GameObject newParent = underlyingGrid.GetRandomGridLocatorForType<T>(checkForEmpty);
        
        if(newParent == null)
        {
            return false;
        }

        grabbable.transform.parent = newParent.transform;
        grabbable.transform.localPosition = Vector3.back * 0.1f;
        return true;
    }

    public bool PlaceObject(GrabbableObject grabbable, Vector3 position)
    {
        GameObject newParent = underlyingGrid.GetClosestGridLocatorObject(position);

        if(newParent == null)
        {
            return false;
        }

        //Check that nothing's already here
        GrabbableObject[] grabbablesInGrid = newParent.GetComponentsInChildren<GrabbableObject>();

        if(grabbablesInGrid.Length > 0)
        {
            return false;
        }

        grabbable.transform.parent = newParent.transform;
        grabbable.transform.localPosition = Vector3.back * 0.1f;
        return true;
    }

    public int GetNumberOfGrabbableObjectsInGrid()
    {
        return GetNumberOfChildrenWithComponent<GrabbableObject>();
    }

    public int GetNumberOfBrokenObjectsInGrid()
    {
        return GetNumberOfGrabbablesWithType(GrabbableObjectType.Trash);
    }

    public int GetNumberOfCorrectObjectsInGrid()
    {  
        return GetNumberOfGrabbablesWithType(GrabbableObjectType.Part);
    }
    
    public int GetNumberOfBugsInGrid()
    {
        return GetNumberOfGrabbablesWithType(GrabbableObjectType.Bug);
    }

    int GetNumberOfGrabbablesWithType(GrabbableObjectType type)
    {
        List<GrabbableObject> grabbablesInThisGrid = GetChildrenWithComponent<GrabbableObject>();
        int count = 0;
        foreach(GrabbableObject obj in grabbablesInThisGrid)
        {
            if(obj.type == type)
            {
                count++;
            }
        }

        return count;
    }

    List<T> GetChildrenWithComponent<T>() where T : Component
    {
        return new List<T>(GetComponentsInChildren<T>());
    }

    int GetNumberOfChildrenWithComponent<T>() where T : Component
    {
        return GetComponentsInChildren<T>().Length;
    }

}
