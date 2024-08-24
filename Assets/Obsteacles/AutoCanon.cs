using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCanon : MonoBehaviour
{
    [Header("Fire CannonBall")]
    public Transform RotationPoint;
    public Transform BottomCannon;
    public GameObject CanonBall;
    public float FireForce;
    public Transform FirePoint;
    public float FireCoolDown;
    private float FireTimer;

    [Header("Line Of Sight")]
    private Transform Target;
    public float LineOfSight;
    private float TargetDistance;
    private bool PlayerInSight;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        FireTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FireTimer -= Time.deltaTime;
        FindTarget();

        if (PlayerInSight)
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (FireTimer <= 0)
        {
            GameObject _CanonBall = Instantiate(CanonBall, FirePoint.position, FirePoint.rotation);
            _CanonBall.GetComponent<Rigidbody2D>().velocity = new Vector2(FirePoint.transform.right.x * FireForce, 0f);
            FireTimer = FireCoolDown;
        }
    }

    private void FindTarget()
    {
        TargetDistance = Vector3.Distance(transform.position, Target.transform.position);

        if (TargetDistance < LineOfSight)
        {
            PlayerInSight = true;
        }
        else
        {
            PlayerInSight = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(new Vector3(BottomCannon.transform.position.x + (LineOfSight / 2), BottomCannon.transform.position.y, BottomCannon.transform.position.z), new Vector3(LineOfSight, 40f, 0));
    }
}
