using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class fallingSystem : MonoBehaviour
{
    public CinemachineCameraOffset offset;
    public Rigidbody2D Player_rb;
    public GameObject player;

    [Header("flip")]
    public float ahead;
    public float aheadspeed;
    public float lookahead;

    [Header("yoffset")]
    public float Yoffsetspeed;
    public float Yoffsetahead;
    private float Yoffset;
    public float YoffsetNotfalling;

    // Start is called before the first frame update
    void Start()
    {
        //mr.octupos has ocd;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_rb.velocity.y >= 0f)
        {
            Yoffset = Mathf.Lerp(Yoffset, YoffsetNotfalling, Yoffsetspeed * Time.deltaTime);
        }
        
        if (Player_rb.velocity.y < 0f)
        {
            Yoffset = Mathf.Lerp(Yoffset, Yoffsetahead, Yoffsetspeed * Time.deltaTime);
        }
        offset.m_Offset = new Vector3(0, 0 + Yoffset, 0);

        //
        if (player.transform.rotation.y == -180)
        {
            ahead = Mathf.Lerp(ahead, lookahead, aheadspeed * Time.deltaTime);

        }

        if (player.transform.rotation.y == 0)
        {
            ahead = Mathf.Lerp(ahead, lookahead, aheadspeed * Time.deltaTime);
        }
    }
}