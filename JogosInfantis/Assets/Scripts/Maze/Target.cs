using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Text text;
    MazeController MazeController;
    public Button Next;

    private void Awake()
    {
        MazeController = FindObjectOfType<MazeController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        SetUIStatus(true);

        collision.transform.position = transform.position;
        collision.GetComponent<BallFollower>().enabled = false;        
    }

    public void SetUIStatus(bool status)
    {
        Next.gameObject.SetActive(status);
        text.gameObject.SetActive(status);
    }
}
