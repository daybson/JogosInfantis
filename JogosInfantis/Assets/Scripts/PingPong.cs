using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    public float speedPingPong;
    public float distancePingPong;
    public float speedMovement;

    [SerializeField]
    private Vector3 movementDirection;
    private Vector3 pingPontDirection;


    private void Awake()
    {
        CalculatePingPong();
    }

    private void Update()
    {
        CalculatePingPong();

        Debug.DrawRay(transform.position, movementDirection, Color.green);
        Debug.DrawRay(transform.position, pingPontDirection, Color.red);

        //transform.position = pingPontDirection * Mathf.PingPong(Time.time, distancePingPong);

        transform.right = movementDirection.normalized;
        transform.Translate(movementDirection.normalized * speedMovement * Time.deltaTime, Space.World);


        transform.position += (pingPontDirection * Mathf.PingPong(Time.time * speedPingPong, distancePingPong));
    }

    public void SetDirection(Vector3 dir)
    {
        movementDirection = dir.normalized;
        CalculatePingPong();
    }

    private void CalculatePingPong()
    {
        pingPontDirection = new Vector3(-movementDirection.y, movementDirection.x).normalized;
    }
}
