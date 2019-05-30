using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(TargetJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BallFollower : MonoBehaviour
{
    private TargetJoint2D target2D;
    private new Rigidbody2D rigidbody2D;


    private void Awake()
    {
        this.target2D = GetComponent<TargetJoint2D>();
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    private void LateUpdate()
    {
#if UNITY_EDITOR
        this.target2D.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#endif

#if UNITY_ANDROID        
        if (Input.touchCount > 0)
        {
            var t = Input.GetTouch(0);
            var pw = Camera.main.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, 0));
            this.target2D.target = pw;
        }
#endif
    }
}
