using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 targetPoint = Vector3.zero;
    public Player player;
    public float movespeed;

    public float lookahead = 5f, lookaheadspeed = 3f;
    private float lookoffset;
    public float maxvertoffset = 5f;
    public float jumpoffset;



    private float falloffset;
    public float yoffset;
    public float lookdown = -2, lookdownspeed = 3f;

    public float ycameramovement;


    // Start is called before the first frame update
    void Start()
    {
        targetPoint = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
            //targetPoint.y = player.transform.position.y + yoffset;

        if (player.rb.velocity.x > 0f)
        {
            lookoffset = Mathf.Lerp(lookoffset, lookahead, lookaheadspeed * Time.deltaTime);
        }
        if (player.rb.velocity.x < 0f)
        {
            lookoffset = Mathf.Lerp(lookoffset, -lookahead, lookaheadspeed * Time.deltaTime);
        }
        targetPoint.x = player.transform.position.x + lookoffset;

        //falling
        if (player.GroundCheck == false)
        {

            falloffset = Mathf.Lerp(falloffset, lookdown, lookdownspeed * Time.deltaTime);
        }
        if (player.GroundCheck)
        {
            falloffset = Mathf.Lerp(falloffset, 0, lookdownspeed * Time.deltaTime);
        }

        targetPoint.y = player.transform.position.y + yoffset + falloffset;

        transform.position = Vector3.Lerp(transform.position, targetPoint, movespeed * Time.deltaTime);
    }
}