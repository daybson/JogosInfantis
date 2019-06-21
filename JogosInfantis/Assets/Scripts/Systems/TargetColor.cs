using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TargetColor : MonoBehaviour
{
    public Text Text;
    public Color C { get; private set; }
    public bool Filled;

    private void Awake()
    {
        Text = GetComponentInChildren<Text>();
    }

    public void SetName(string name, Color color)
    {
        C = color;
        Text.text = name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Filled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Filled = false;
    }
}
