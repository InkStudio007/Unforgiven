using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling_Gun : MonoBehaviour
{
    public float GrappleLength;
    public LayerMask GrapplingLayer;
    public LineRenderer GrappleRope;

    private Vector3 GrapplePoint;
    private DistanceJoint2D Joint;
    public bool GrapplingArea = false;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Joint = gameObject.GetComponent<DistanceJoint2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Joint.enabled = false;
        GrappleRope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                origin: Camera.main.ScreenToWorldPoint(Input.mousePosition),
                direction: Vector2.zero,
                distance: 5,
                layerMask: GrapplingLayer); 

            if (GrapplingArea)
            {
                if (hit.collider)
                {
                    GrapplePoint = hit.point;
                    GrapplePoint.z = 0;
                    Joint.connectedAnchor = GrapplePoint;
                    Joint.distance = GrappleLength;
                    Joint.enabled = true;
                    GrappleRope.SetPosition(0, GrapplePoint);
                    GrappleRope.SetPosition(1, transform.position);
                    GrappleRope.enabled = true;
                }
            }
        }

        if (GrappleRope.enabled == true)
        {
            GrappleRope.SetPosition(1, transform.position);
        }

        if (Input.GetMouseButtonUp(1))
        {
            Joint.enabled = false;
            GrappleRope.enabled = false;
        }
    }
}

