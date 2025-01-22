using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed, sensitivity, maxForce;

    private float lookRot;
    private Vector2 move, look;
    private Rigidbody rb;
    private Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");

        look.x = Input.GetAxis("Mouse X");
        look.y = Input.GetAxis("Mouse Y");
    }

    private void FixedUpdate()
    {
        Vector3 currVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y) * speed;

        targetVelocity = transform.TransformDirection(targetVelocity);
        Vector3 velocityChange = targetVelocity - currVelocity;

        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(new Vector3(velocityChange.x, 0, velocityChange.z), ForceMode.VelocityChange);
    }

    private void LateUpdate()
    {
        transform.Rotate(Vector3.up * look.x * sensitivity);

        lookRot += (-look.y * sensitivity);
        lookRot = Mathf.Clamp(lookRot, -90, 90);

        cam.transform.eulerAngles = new Vector3(lookRot,
            cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
    }
}
