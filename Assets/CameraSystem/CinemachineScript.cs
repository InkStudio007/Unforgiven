using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineScript : MonoBehaviour
{
    [SerializeField]
    private CinemachineFramingTransposer CinemachineBody;
    [SerializeField]
    private CinemachineVirtualCamera Camera;

    [Header("Objects")]
    public bool GroundCheck;
    public Rigidbody2D rb;
    public bool isClimbing;
    private Vector3 velocity = Vector3.zero;
    private float velocityFloat = 0.0f;


    [Header("Vertical Look Out")]
    [SerializeField]
    private float LookUpDistance;
    [SerializeField]
    private float LookDownDistance;
    [SerializeField]
    private float LookOutSpeed;
    [SerializeField]
    private float YScreen = 0.5f;
    [SerializeField]
    private float LookOutTimerRecycle = 1f;
    private float LookOutTimer;


    [Header("Falling")]
    [SerializeField]
    private float YDamping;


    [Header("Player Falling")]
    [SerializeField]
    private bool FallingCheck = false;
    [SerializeField]
    private float FallingTimer;
    [SerializeField]
    private float FallingTimerRecycle = 0.5f;

    [Header("On Standing")]
    [SerializeField]
    private float StaidOffset;
    [SerializeField]
    private float XScreen;
    [SerializeField]
    private bool StaidStill = false;

    [Header("Dashing")]
    [SerializeField]
    private bool isDashing;
    [SerializeField]
    private float XDamping;

    // Start is called before the first frame update
    void Start()
    {
        CinemachineBody = Camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        GroundCheck = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        GroundCheck = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isClimbing = GameObject.FindGameObjectWithTag("Ladder").GetComponent<Ladder>();
        isDashing = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        LookOutTimer = LookOutTimerRecycle;
        FallingTimer = FallingTimerRecycle;

        //Ladder
        if (GameObject.Find("ladder") == null)
        {
            isClimbing = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //ladder
        if(isClimbing == true)
        {
            CinemachineBody.m_YDamping = Mathf.SmoothDamp(CinemachineBody.m_YDamping, 0, ref velocityFloat, 0);
        }
        if (isClimbing == false)
        {
            CinemachineBody.m_YDamping = Mathf.SmoothDamp(CinemachineBody.m_YDamping, XScreen, ref velocityFloat, 0);
        }

        //Staid 
        if (StaidStill)
        {
            CinemachineBody.m_ScreenX = Mathf.SmoothDamp(CinemachineBody.m_ScreenX, StaidOffset, ref velocityFloat, 0);
        }
        if(StaidStill == false)
        {
            CinemachineBody.m_ScreenX = Mathf.SmoothDamp(CinemachineBody.m_ScreenX, XScreen, ref velocityFloat, 0);
        }
        //falling check
        if (GroundCheck == false)
        {
            FallingTimer -= Time.deltaTime;
            if(FallingTimer <= 0)
            {
                Debug.Log("Velocity Check");
                FallingCheck = true;
                FallingTimer = FallingTimerRecycle;
            }
        }
        if(GroundCheck)
        {
            FallingCheck = false;
        }

        //falling
        if(FallingCheck)
        {
            CinemachineBody.m_YDamping = Mathf.SmoothDamp(CinemachineBody.m_YDamping, 0, ref velocityFloat, 0);
        }
        if(FallingCheck == false)
        {
            CinemachineBody.m_YDamping = Mathf.SmoothDamp(CinemachineBody.m_YDamping, YDamping, ref velocityFloat, 0);
        }

        //Dashing
        if (isDashing)
        {
            CinemachineBody.m_YDamping = Mathf.SmoothDamp(CinemachineBody.m_YDamping, 0, ref velocityFloat, 0);
            CinemachineBody.m_XDamping = Mathf.SmoothDamp(CinemachineBody.m_XDamping, 0, ref velocityFloat, 0);
        }
        if (isDashing == false)
        {
            CinemachineBody.m_YDamping = Mathf.SmoothDamp(CinemachineBody.m_YDamping, YDamping, ref velocityFloat, 0);
            CinemachineBody.m_XDamping = Mathf.SmoothDamp(CinemachineBody.m_XDamping, XDamping, ref velocityFloat, 0);
        }


        //Vertical Lookout
        if (GroundCheck & isClimbing == false)
        {
            //up
            if (Input.GetKey(KeyCode.W))
            {
                LookOutTimer -= Time.deltaTime;
                if (LookOutTimer <= 0)
                {
                    CinemachineBody.m_ScreenY = Mathf.SmoothDamp(CinemachineBody.m_ScreenY, LookUpDistance, ref velocityFloat, LookOutSpeed);
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                CinemachineBody.m_ScreenY = Mathf.SmoothDamp(CinemachineBody.m_ScreenY, YScreen, ref velocityFloat, LookOutSpeed);
                LookOutTimer = LookOutTimerRecycle;
            }

            //down
            if (Input.GetKey(KeyCode.S))
            {
                LookOutTimer -= Time.deltaTime;
                if (LookOutTimer <= 0)
                {
                    CinemachineBody.m_ScreenY = Mathf.SmoothDamp(CinemachineBody.m_ScreenY, LookDownDistance, ref velocityFloat, LookOutSpeed);
                }
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                CinemachineBody.m_ScreenY = Mathf.SmoothDamp(CinemachineBody.m_ScreenY, YScreen, ref velocityFloat, LookOutSpeed);
                LookOutTimer = LookOutTimerRecycle;
            }
        }
    }

}

