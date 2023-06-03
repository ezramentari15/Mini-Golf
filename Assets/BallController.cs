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
    // [SerializeField] LineRenderer aimLine;
    [SerializeField] Transform aimWorld;
    bool shoot;
    bool shootingMode;
    float forceFactor;

    public bool ShootingMode { get => shootingMode; }

    private void Update()
    {
        if(shootingMode)
        {
            if(Input.GetMouseButtonDown(button: 0))
            {
                // aimLine.gameObject.SetActive(true);
                aimWorld.gameObject.SetActive(value: true);
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
                aimWorld.transform.position = this.transform.position;
                var aimDirection = new Vector3(pointerDirection.x, 0, pointerDirection.y);
                aimDirection = Camera.main.transform.localToWorldMatrix * aimDirection;
                aimDirection.y = 0;
                aimWorld.transform.forward = aimDirection.normalized;

                // forc factor 
                forceFactor = pointerDirection.magnitude * 2;
                Debug.Log(message: forceFactor);

                // force factor
                Debug.Log(message: pointerDirection.normalized); 
            }
            else if(Input.GetMouseButtonUp(button: 0))
            {
                shoot = true;
                shootingMode = false;
                // aimLine.gameObject.SetActive(false);
                aimWorld.gameObject.SetActive(value: false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (shoot)
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
