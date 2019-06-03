using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(TargetJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BallFollower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TargetJoint2D target2D;
    private new Rigidbody2D rigidbody2D;
    private bool hover;

    private void Awake()
    {
        this.target2D = GetComponent<TargetJoint2D>();
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Start()
    {
        GameSystem.Instance.CheckForDragRequirements();
    }


    private void LateUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && hover)
        {
            var pw = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //if ((pw - transform.position).sqrMagnitude < 102)
            this.target2D.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
#endif

#if UNITY_ANDROID        
        if (Input.touchCount > 0 && hover)
        {
            var t = Input.GetTouch(0);
            var pw = Camera.main.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, 0));

            //if ((pw - transform.position).sqrMagnitude < 102)
            this.target2D.target = pw;
        }
#endif
    }

    public void OnPointerEnter(PointerEventData eventData) => hover = true;

    public void OnPointerExit(PointerEventData eventData) => hover = false;
}
