using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BirdAStart : MonoBehaviour
{
    public Transform Target;

    public float MoveSpeed;
    public float NextWayPointDistance = 3f;

    private int CurrentWayPoint = 0;
    private bool ReachedTarget = false;

    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        seeker = gameObject.GetComponent<Seeker>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void FixedUpdate()
    {
        if (path == null)
            return;

        if (CurrentWayPoint >= path.vectorPath.Count)
        {
            ReachedTarget = true;
            return;
        }
        else
        {
            ReachedTarget = false;
        }

        Vector2 Direction = ((Vector2)path.vectorPath[CurrentWayPoint] - rb.position).normalized;
        Vector2 Force = Direction * MoveSpeed * Time.deltaTime;

        float Distance = Vector2.Distance(rb.position, path.vectorPath[CurrentWayPoint]);

        if (Distance < NextWayPointDistance)
        {
            CurrentWayPoint += 1;
        }

        rb.AddForce(Force);
    }


    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, Target.transform.position, OnPathComplete);
        }
    }


    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            CurrentWayPoint = 0;
        }
    }
}
