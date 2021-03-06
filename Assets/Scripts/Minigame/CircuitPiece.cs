﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitPiece : MonoBehaviour
{
    public GrabbableObjectType type;
    public int partType;
    public bool disableGrabbing;

    public Sprite brokenSprite;
    public Sprite missingSprite;
    public Sprite fixedSprite;
    public SpriteRenderer rendererToAffect;

    public void SetGameObjectSprite()
    {
        switch(type)
        {
            case GrabbableObjectType.Missing:
                rendererToAffect.sortingOrder = 1;
                rendererToAffect.sprite = missingSprite;
                break;
            case GrabbableObjectType.Broken:
                rendererToAffect.sortingOrder = 2;
                rendererToAffect.sprite = brokenSprite;
                break;
            case GrabbableObjectType.Part:
                rendererToAffect.sortingOrder = 3;
                rendererToAffect.sprite = fixedSprite;
                break;
            case GrabbableObjectType.Bug:
                break;
        }
    }
}

public enum GrabbableObjectType {Missing, Broken, Part, Bug};
