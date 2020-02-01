using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableGrid : MonoBehaviour
{
    public GridBuilder underlyingGrid;

    public void PlaceObject(GrabbableObject grabbable, Vector3 position)
    {
        grabbable.transform.parent = this.transform;
        Vector3 newPosition = underlyingGrid.GetClosestPositionOnGrid(position);

        grabbable.transform.position = newPosition;
    }

    public int GetNumberOfGrabbableObjectsInGrid()
    {
        return GetNumberOfChildrenWithComponent<GrabbableObject>();
    }

    public int GetNumberOfBrokenObjectsInGrid()
    {
        return GetNumberOfChildrenWithComponent<GrabbableObject>();
    }

    public int GetNumberOfCorrectObjectsInGrid()
    {  
        return GetNumberOfChildrenWithComponent<GrabbableObject>();
    }
    
    public int GetNumberOfWiretapsInGrid()
    {
        return GetNumberOfChildrenWithComponent<GrabbableObject>();
    }

    int GetNumberOfChildrenWithComponent<T>() where T : MonoBehaviour
    {
        return GetComponentsInChildren<T>().Length;
    }

}
