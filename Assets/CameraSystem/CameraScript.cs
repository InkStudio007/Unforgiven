using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Player Player;
    private Vector3 TargetPoint = Vector3.zero;

    public float movespeed;

    public float lookAheadDistance = 30f ,lookAheadSpeed = 3f; 

    private float LookOffSet;

    private bool isFalling;

    public float VerticalOffSet = 33;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        TargetPoint = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Player.GroundCheck)
        {
            TargetPoint.y = Player.transform.position.y + VerticalOffSet;
        }

        if(transform.position.y - Player.transform.position.y > VerticalOffSet)
        {
            isFalling = true;
        }

        if (isFalling)
        {
            TargetPoint.y = Player.transform.position.y;
            if (Player.GroundCheck)
            {
                isFalling = false;
            }
        }


        if (Player.rb.velocity.x > 0)
        {
            LookOffSet = Mathf.Lerp(LookOffSet, lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }
        if (Player.rb.velocity.x < 0)
        {
            LookOffSet = Mathf.Lerp(LookOffSet, -lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }

        TargetPoint.x = Player.transform.position.x + LookOffSet;


        transform.position = Vector3.Lerp(transform.position, TargetPoint, movespeed * Time.deltaTime);
    }
}
