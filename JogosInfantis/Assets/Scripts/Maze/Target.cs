using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform targetTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        collision.transform.position = targetTransform.position;
        collision.GetComponent<BallFollower>().enabled = false;        
        MazeUIController.Instance.SetUIStatus(true);
    }
}
