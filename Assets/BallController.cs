using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour , IPointerDownHandler
{
    [SerializeField] Collider col;
    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
    [SerializeField] LineRenderer aimLine;
    bool shoot;
    bool shootingMode;
    float forceFactor;

    public bool ShootingMode { get => shootingMode;}

    private void Update()
    {
        if(shootingMode)
        {
            if(Input.GetMouseButtonDown(button: 0))
            {

            }
            else if(Input.GetMouseButton(button: 0))
            {
                var mouseViewportPos = Camera.main.ScreenToViewportPoint(position: Input.mousePosition);
                var ballViewportPos = Camera.main.WorldToViewportPoint(position: this.transform.position);
                var pointerDirection = ballViewportPos = mouseViewportPos;
                pointerDirection.z = 0;

                // forc factor 
                forceFactor = Vector2.Distance(a: ballViewportPos,b: mouseViewportPos) * 2;
                Debug.Log(message: forceFactor);

                // force factor
                Debug.Log(message: pointerDirection.normalized); 

            }
            else if(Input.GetMouseButtonUp(button: 0))
            {
                shoot = true;
                shootingMode = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if(shoot)
        {
            shoot = false;
            Vector3 direction = Camera.main.transform.forward;
            direction.y = 0;
            rb.AddForce(force: direction * force * forceFactor, mode: ForceMode.Impulse);
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
        shootingMode = true;
    }
}
