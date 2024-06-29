using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Player PlayerScript;
    public Camera mainCam;
    private Vector3 MousePos;
    public Transform RotationPoint;
    public GameObject Bullet;
    public Transform BulletPoint;
    private bool CanFire = true;
    private float Timer;
    public float FireCoolDown;
    public float Ammo = 8;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation
        MousePos = (Input.mousePosition);
        Vector3 Rotation = MousePos - RotationPoint.position;
        float rotZ = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg;

        RotationPoint.rotation = Quaternion.Euler(0, 0, rotZ);
        Debug.Log(rotZ);

        //Shooting

        if (!CanFire)
        {
            Timer += Time.deltaTime;
            if (Timer >= FireCoolDown)
            {
                CanFire = true;
                Timer = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && CanFire && Ammo > 0)
        {
            Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
            CanFire = false;
            Ammo -= 1;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Ammo = 8;
        }

        //Flip

        if (PlayerScript.isFacingRight && rotZ >= 90)
        {
            PlayerScript.InstantFlipe();
            Debug.Log("Flipe");
        }
        else if (PlayerScript.isFacingRight == false && rotZ <= 90)
        {
            PlayerScript.InstantFlipe();
            Debug.Log("Flipe");
        }
    }
}
