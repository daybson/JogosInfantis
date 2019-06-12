using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        MazeUIController.Instance.SetUIStatus(true);

        collision.transform.position = transform.position;
        collision.GetComponent<BallFollower>().enabled = false;        
    }
}
