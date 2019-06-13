using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisbaleCulling : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
