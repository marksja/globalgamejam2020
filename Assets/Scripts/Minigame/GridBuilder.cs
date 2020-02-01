﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    public Vector2 gridLayout;
    public GameObject gridBoxParent;
    public BoxCollider2D gridBoxBounds;
    Vector2 gridSize;
    Vector2 gridOffset;
    Vector2 gridBoxSize;

    List<GameObject> gridBoxes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> gridBoxes = new List<GameObject>();
        CreateGrid();
    }

    public void ClearGrid()
    {
        //Clear the old grid
        for(int i = 0; i < gridBoxes.Count; ++i)
        {
            Destroy(gridBoxes[i]);
        }
        gridBoxes = new List<GameObject>();
    }

    [ContextMenu("Create Grid")]
    public void CreateGrid()
    {
        ClearGrid();

        gridSize = gridBoxBounds.size;
        gridOffset = gridBoxBounds.offset;

        gridBoxSize = new Vector2(gridSize.x / gridLayout.x, gridSize.y / gridLayout.y);
        int numGridItems = (int)gridLayout.x * (int)gridLayout.y;
        for(int i = 0; i < numGridItems; ++i)
        {
            GameObject obj = new GameObject();
            obj.transform.parent = gridBoxParent.transform;

            //Silly Jacob, we don't need a collider
            //BoxCollider2D collider = obj.AddComponent<BoxCollider2D>();
            //collider.size = gridBoxSize;

            obj.transform.localPosition = GetPositionFromGridCoordinates(i % (int)gridLayout.x, Mathf.FloorToInt(i / gridLayout.x));
            obj.name = "(" + i % (int)gridLayout.x + ", " + Mathf.FloorToInt(i / gridLayout.x) + ")";
            gridBoxes.Add(obj);
        } 
    }

    public Vector3 GetPositionFromGridCoordinates(int x, int y)
    {
        Vector3 topLeftPosition = new Vector3(gridOffset.x - (gridSize.x / 2), gridOffset.y + (gridSize.y / 2));
        return new Vector3(topLeftPosition.x + (x + 0.5f) * gridBoxSize.x, topLeftPosition.y - (y + 0.5f) * gridBoxSize.y);
    }

    public Vector3 GetClosestPositionOnGrid(Vector3 position)
    {
        int index = GetGridIndexFromPosition(position);

        return gridBoxes[index].transform.position + Vector3.back * 1;
    }

    public Vector2 GetClosestGridCoordinatesFromPosition(Vector3 position)
    {
        int index = GetGridIndexFromPosition(position);

        return new Vector2(index % gridLayout.x, Mathf.FloorToInt(index / gridLayout.x));
    }

    int GetGridIndexFromPosition(Vector3 position)
    {
        float closestDistance = Mathf.Infinity;
        int closestIndex = -1;
        for(int i = 0; i < gridBoxes.Count; ++i)
        {
            float distance = Vector2.Distance(position, gridBoxes[i].transform.position);
            if(distance < closestDistance)
            {
                closestIndex = i;
                closestDistance = distance;
            }
        }

        return closestIndex;
    }

    public void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        Debug.Log(GetClosestGridCoordinatesFromPosition(mousePosition));
    }

    public void OnMouseUp()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        Debug.Log(GetClosestGridCoordinatesFromPosition(mousePosition));
    }
}