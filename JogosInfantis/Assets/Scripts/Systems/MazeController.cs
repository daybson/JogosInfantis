using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class MazeController : MonoBehaviour
{
    public Tilemap[] Mazes;
    public int First;
    BallFollower BallFollower;
    Target Target;

    private void Awake()
    {
        BallFollower = FindObjectOfType<BallFollower>();
        Target = FindObjectOfType<Target>();

        for (int i = 0; i < Mazes.Length; i++)
            Mazes[i].gameObject.SetActive(false);

        Mazes[First].gameObject.SetActive(true);
    }


    public void OnCompleteMaze()
    {
        if (First + 1 < Mazes.Length)
        {
            Mazes[First].gameObject.SetActive(false);
            Mazes[++First].gameObject.SetActive(true);
        }

        BallFollower.ResetPosition();
        BallFollower.enabled = true;
        Target.SetUIStatus(false);
    }
}
