using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called twice per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        this.transform.position = cursorPosition;
    }
}
