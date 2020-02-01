using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent onMouseOver;
    public UnityEvent onMouseExit;
    public UnityEvent onMouseDown;
    public UnityEvent onMouseClick;

    public void OnMouseEnter()
    {
        if(onMouseOver != null) onMouseOver.Invoke();
    } 

    public void OnMouseExit()
    {
        if(onMouseExit != null) onMouseExit.Invoke();
    }

    public void OnMouseDown()
    {
        if(onMouseDown != null) onMouseDown.Invoke();
    }    

    public void OnMouseUpAsButton()
    {
        if(onMouseClick != null) onMouseClick.Invoke();
    }    
}
