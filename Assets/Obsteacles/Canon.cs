using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    // Canon Rotation
    public Transform RotationPoint;
    private float Vertical;
    private float RotateSpeedP;
    private float RotateSpeedM;
    private float Rotation = 1f;

    private bool isCanon;

    // Canon Fire
    public GameObject CanonBall;
    public float FireForce;
    public Transform FirePoint;
    private Rigidbody2D CanonBallrb;

    // Trajectory prediction
    [Header("Trajectory Line")]
    public LineRenderer lr;
    public Vector2[] Dots;
    public int NumberOfDots;
    public float TrajectoryCurve = 3.5f;


    // Start is called before the first frame update
    void Start()
    {
        CanonBallrb = CanonBall.GetComponent<Rigidbody2D>();
        Dots = new Vector2[NumberOfDots];

        lr.positionCount = NumberOfDots;
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        Vertical = Input.GetAxisRaw("Vertical");

        if (Rotation >= 60)
        {
            RotateSpeedP = 0;
        }
        else
        {
            RotateSpeedP = 1;
        }

        if (Rotation <= -40)
        {
            RotateSpeedM = 0;
        }
        else
        {
            RotateSpeedM = 1;
        }

        if (isCanon)
        {
            if (Vertical > 0)
            {
                Rotation += RotateSpeedP;
            }
            if (Vertical < 0)
            {
                Rotation -= RotateSpeedM;
            }

            RotationPoint.rotation = Quaternion.Euler(0, 0, Rotation);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Fire();
            }

            lr.enabled = true;
            TrajectoryLine();
        }
        else
        {
            lr.enabled = false;
        }

        
    } 
    public void Fire()
    {
        GameObject _CanonBall = Instantiate(CanonBall, FirePoint.position, FirePoint.rotation);
        _CanonBall.GetComponent<Rigidbody2D>().velocity = RotationPoint.transform.right * FireForce;
    }

    public void TrajectoryLine()
    {
        Vector2 StartPos = FirePoint.position;
        Dots[0] = StartPos;
        lr.SetPosition(0, StartPos);

        Vector2 StartVelocity = RotationPoint.transform.right * FireForce;

        for (int i = 1; i < NumberOfDots; i++)
        {
            float TimeOfSet = (i * Time.fixedDeltaTime * TrajectoryCurve);

            Vector2 GravityOffSet = 0.5f * Physics2D.gravity * CanonBallrb.gravityScale * Mathf.Pow(TimeOfSet, 2);

            Dots[i] = Dots[0] + StartVelocity * TimeOfSet + GravityOffSet;
            lr.SetPosition(i, Dots[i]);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isCanon = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCanon = false;
        }
    }
}
