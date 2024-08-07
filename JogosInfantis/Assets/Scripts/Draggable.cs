﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float z;

    private void Start()
    {
        GameSystem.Instance.CheckForDragRequirements();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin drag");
    }


    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        Vector3 worldPosition = Vector3.zero;

#if UNITY_EDITOR
        worldPosition = GameSystem.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
#endif 

#if UNITY_ANDROID
        worldPosition = GameSystem.Instance.MainCamera.ScreenToWorldPoint(eventData.position);
#endif 

        transform.position = new Vector3(worldPosition.x, worldPosition.y, this.z);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End drag");
    }
}
