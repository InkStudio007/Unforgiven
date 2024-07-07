using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform Platform;
    public Transform StartPoint;
    public Transform EndPoint;

    private int Direction = 1;
    public float MoveSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector2 Target = CurrentTarget();

        Platform.position = Vector2.MoveTowards(Platform.position, Target, MoveSpeed * Time.deltaTime);

        float Distance = (Target - (Vector2)Platform.position).magnitude;
        if (Distance <= 0.1f)
        {
            Direction *= -1;
        }
    }

    Vector2 CurrentTarget()
    {
        if (Direction == 1)
        {
            return StartPoint.position;
        }
        else
        {
            return EndPoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        if (Platform != null && StartPoint != null && EndPoint != null)
        {
            Gizmos.DrawLine(Platform.transform.position, StartPoint.transform.position);
            Gizmos.DrawLine(Platform.transform.position, EndPoint.transform.position);
        }
    }
}
