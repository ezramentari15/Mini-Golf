using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speeder : MonoBehaviour
{
    [SerializeField] float force;

    bool isSpeeding;
    private void OnCollisionEnter(Collision other)
    {
        Speed(other.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        Speed(other);
    }

    private void Speed(Collider other)
    {
        if(isSpeeding == false && 
            other.transform.CompareTag(tag: "Ball") && 
            other.transform.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(force: force * this.transform.forward, mode: ForceMode.Impulse);
            isSpeeding = true;
            Invoke(methodName: "Reset", time: 0.3f);
        }
    }

    private void Reset()
    {
        isSpeeding = false;
    }
}