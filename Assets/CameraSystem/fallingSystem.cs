using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class fallingSystem : MonoBehaviour
{
    public CinemachineCameraOffset offset;
    public Player rb;
    public Player player;

    public float Yoffsetspeed;
    public float Yoffsetahead;
    private float Yoffset;
    public float YoffsetNotfalling;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.rb.velocityY > 0f)
        {
            Yoffset = Mathf.Lerp(Yoffset, YoffsetNotfalling, Yoffsetspeed * Time.deltaTime);
        }
        if(player.rb.velocityY < 0f)
        {
            Yoffset = Mathf.Lerp(Yoffset, Yoffsetahead, Yoffsetspeed * Time.deltaTime);
        }
        offset.m_Offset = new Vector3(0, 0 + Yoffset, 0);
    }
}
