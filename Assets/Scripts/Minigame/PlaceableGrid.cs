using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableGrid : MonoBehaviour
{
    public GridBuilder underlyingGrid;

    public bool onlyAllowMatchingPieces;

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

    public bool PlaceObjectAt<T>(T obj, int index) where T : Component
    {
        GameObject newParent = underlyingGrid.GetGridLocatorObject(index);

        if(newParent == null)
        {
            return false;
        }

        obj.transform.parent = newParent.transform;
        obj.transform.localPosition = new Vector3(0, 0, obj.transform.localPosition.z);
        return true;
    }

    public bool PlaceObject(CircuitPiece grabbable, Vector3 position)
    {
        GameObject newParent = underlyingGrid.GetClosestGridLocatorObject(position);

        if(newParent == null)
        {
            return false;
        }

        //Check that nothing's already here
        CircuitPiece[] grabbablesInGrid = newParent.GetComponentsInChildren<CircuitPiece>();

        foreach(CircuitPiece piece in grabbablesInGrid)
        {
            if(piece.type == GrabbableObjectType.Missing && (piece.partType == grabbable.partType || !onlyAllowMatchingPieces))
            {
                continue;
            }
            return false;
        }

        grabbable.transform.parent = newParent.transform;
        grabbable.transform.localPosition = new Vector3(0,0, grabbable.transform.localPosition.z);
        return true;
    }

    public int GetNumberOfGrabbableObjectsInGrid()
    {
        return GetNumberOfChildrenWithComponent<CircuitPiece>();
    }

    public int GetNumberOfBrokenObjectsInGrid()
    {
        return GetNumberOfGrabbablesWithType(GrabbableObjectType.Broken);
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
        List<CircuitPiece> grabbablesInThisGrid = GetChildrenWithComponent<CircuitPiece>();
        int count = 0;
        foreach(CircuitPiece obj in grabbablesInThisGrid)
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
