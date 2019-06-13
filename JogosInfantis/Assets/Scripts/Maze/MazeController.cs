using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using System;

public class MazeController : Singleton<MazeController>
{
    public List<Tilemap> Mazes;
    public int currentMaze;
    BallFollower BallFollower;

    public Camera MainCamera { get; private set; }

    public bool IsLastLevel => currentMaze == Mazes.Count - 1;


    private void Awake()
    {
        MainCamera = Camera.main;

        Mazes = FindObjectsOfType<Tilemap>().OrderBy(t => t.gameObject.name).ToList();
        BallFollower = FindObjectOfType<BallFollower>();

        Mazes.ForEach(m => m.gameObject.SetActive(false));
        Mazes[currentMaze].gameObject.SetActive(true);
    }

    internal void PlayPause()
    {

    }

    internal void ShowConfigPanel()
    {

    }

    public void CheckForDragRequirements()
    {
        if (FindObjectOfType<EventSystem>() == null)
            Debug.LogError("There's no EventSystem in scene. Drag events will not work!");

        if (FindObjectOfType<StandaloneInputModule>() == null)
            Debug.LogError("There's no StandaloneInputModule in scene. Drag events will not work!");

        if (FindObjectOfType<Physics2DRaycaster>() == null)
            Debug.LogError("There's no Physics2DRaycaster in scene. Drag events will not work!");
    }


    public void LoadNextMaze()
    {
        if (!IsLastLevel)
            LoadMaze(currentMaze + 1);
    }

    public void LoadCurrentMaze()
    {
        LoadMaze(currentMaze);
    }


    public void LoadMaze(int mazeIndex)
    {
        if (this.currentMaze > this.Mazes.Count)
            return;

        this.Mazes[this.currentMaze].gameObject.SetActive(false);
        currentMaze = mazeIndex;
        BallFollower.Respawn();
        MazeUIController.Instance.SetUIStatus(false);
        this.Mazes[this.currentMaze].gameObject.SetActive(true);
    }
}
