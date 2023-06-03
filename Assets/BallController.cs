using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour , IPointerDownHandler
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
    [SerializeField] LineRenderer aimLine;
    [SerializeField] Transform aimWorld;
    bool shoot;
    bool shootingMode;
    float forceFactor;
    Vector3 forceDirection;
    Ray ray;
    Plane plane;

    public bool ShootingMode { get => shootingMode; }

    private void Update()
    {
        if(shootingMode)
        {
            if(Input.GetMouseButtonDown(button: 0))
            {
                aimLine.gameObject.SetActive(value: true);
                aimWorld.gameObject.SetActive(value: true);
                plane = new Plane(inNormal: Vector3.up,inPoint: this.transform.position);
            }
            else if(Input.GetMouseButton(button: 0))
            {
                var mouseViewportPos = Camera.main.ScreenToViewportPoint(position: Input.mousePosition);
                var ballViewportPos = Camera.main.WorldToViewportPoint(position: this.transform.position);
                var ballScreenPos = Camera.main.WorldToScreenPoint(position: this.transform.position);
                var pointerDirection = ballViewportPos = mouseViewportPos;
                pointerDirection.z = 0;

                // // draw aim
                // aimLine.transform.position = ballScreenPos;
                // var positions = new Vector3[]{ballScreenPos,Input.mousePosition};
                // aimLine.SetPositions(positions);

                // force direction
                ray = Camera.main.ScreenPointToRay(pos: Input.mousePosition);
                plane.Raycast(ray: ray,enter: out var distance);
                forceDirection = (this.transform.position - ray.GetPoint(distance: distance));
                forceDirection.Normalize();

                // forc factor 
                forceFactor = pointerDirection.magnitude * 2;

                // aim visual
                aimWorld.transform.position = this.transform.position;
                aimWorld.forward = forceDirection;
                aimWorld.localScale = new Vector3(x: 1,y: 1,z: 0.05f + forceFactor);

            }
            else if(Input.GetMouseButtonUp(button: 0))
            {
                shoot = true;
                shootingMode = false;
                aimLine.gameObject.SetActive(value: false);
                aimWorld.gameObject.SetActive(value: false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (shoot)
        {
            shoot = false;
            rb.AddForce(force: forceDirection * force * forceFactor, mode: ForceMode.Impulse);
        }

        if(rb.velocity.sqrMagnitude < 0.01f && rb.velocity.sqrMagnitude > 0)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public bool IsMove()
    {
        return rb.velocity != Vector3.zero;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(this.IsMove())
            return;

        shootingMode = true;
    }
}
