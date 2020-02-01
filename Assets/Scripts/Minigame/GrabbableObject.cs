using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public GrabbableObjectType type;
    public Vector2 objectSize = Vector2.one;
}

public enum GrabbableObjectType {Trash, Part, Bug};
