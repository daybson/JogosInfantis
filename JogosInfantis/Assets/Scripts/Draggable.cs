using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private float z;

    private void Start()
    {
        GameSystem.Instance.CheckForDragRequirements();
        this.z = GameSystem.Instance.MainCamera.nearClipPlane;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
    }


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        Vector3 worldPosition;

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
        Debug.Log("End drag");
    }
}
