using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Variables")]
    public Player GroundCheck;
    public Player PlatformCheck;
    public GameObject PlayerChild;
    public Ladder isClimbing;
    private Vector3 TargetPoint;
    private Vector3 velocity = Vector3.zero;
    private float velocityFloat = 0.0f;
    private Rigidbody2D rb;
    [SerializeField]
    private float MoveSpeed = 1;


    [Header("Player Falling")]
    [SerializeField] private bool FallingCheck = false;
    [SerializeField] private float FallingTimer;
    [SerializeField] private float FallingTimerRecycle = 0.5f;

    [Header("Falling")]
    [SerializeField]
    private float FallingOffsetDistance;
    [SerializeField]
    private float FallingOffsetSpeed;
    [SerializeField]
    private float FallingOffset;


    [Header("Vertical Offset")]
    [SerializeField]
    private float VerticalOffset;


    [Header("Vertical Look Out")]
    [SerializeField]
    private float LookOutDistance;
    [SerializeField]
    private float LookOutSpeed;
    [SerializeField]
    private float LookOutTimerRecycle = 1f;
    private float VerticalLookOut;
    private float LookOutTimer;

    // Start is called before the first frame update
    void Start()
    {
        TargetPoint = new Vector3(PlayerChild.transform.position.x, PlayerChild.transform.position.y, -10);
        transform.position = TargetPoint;

        LookOutTimer = LookOutTimerRecycle;
        FallingTimer = FallingTimerRecycle;
    }
    private void LateUpdate()
    {
        //Vertical offset added
        TargetPoint.x = PlayerChild.transform.position.x;
        TargetPoint.y = PlayerChild.transform.position.y + VerticalOffset + VerticalLookOut + FallingOffset;
        transform.position = TargetPoint;

        //falling
        if (rb.velocity.y < 0)
        {
            Debug.Log("Velocity Check");
            FallingTimer -= Time.deltaTime;
            if (FallingTimer <= 0)
            {
                Debug.Log("Falling true");
                FallingCheck = true;
                FallingTimer = FallingTimerRecycle;
            }
            if (rb.velocity.y >= 0)
            {
                Debug.Log("falling true");
                FallingCheck = false;
                FallingTimer = FallingTimerRecycle;
            }
        }

        if (FallingCheck)
        {
            Debug.Log("falling works");
            // FallingOffset = Mathf.SmoothDamp(FallingOffset, FallingOffsetDistance, ref velocity, FallingOffsetSpeed);
        }
        if (FallingCheck == false)
        {
            Debug.Log("falling works is zero");
            FallingOffset = 0;
        }
    }
}
