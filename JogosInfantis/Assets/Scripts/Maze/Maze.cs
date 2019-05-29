using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    private void Awake()
    {
        foreach (var item in GetComponentsInChildren<SpriteRenderer>())
        {
            item.gameObject.AddComponent<BoxCollider2D>();
        }
    }
}
