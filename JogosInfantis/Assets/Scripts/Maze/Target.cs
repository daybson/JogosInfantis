using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Text text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.gameObject.SetActive(true);
        collision.transform.position = transform.position;
        collision.GetComponent<BallFollower>().enabled = false;
    }
}
