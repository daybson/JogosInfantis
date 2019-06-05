using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapItem : MonoBehaviour
{
    public float SnapThereshold = 0.75f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Vector3.Distance(collision.transform.position, transform.position) < SnapThereshold)
        {
            var dragg = collision.GetComponent<Draggable>();
            if (dragg != null)
            {
                dragg.transform.position = new Vector3(transform.position.x, transform.position.y, dragg.transform.position.z);
            }
        }
    }
}
