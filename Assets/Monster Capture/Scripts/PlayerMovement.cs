using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 450f;
    [SerializeField] private float moveSpeed = 50f;

    private bool isGrounded;

    Vector2 input;
    Rigidbody rb;

    
    
    //-------------------------------------------------------------------\\


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnJump()
    {
        if (!isGrounded) return;

        rb.AddForce(Vector3.up * jumpSpeed);
    }

    void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        if(input.magnitude > 1)
        {
            input.Normalize();
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement3D = new Vector3(input.x, 0f, input.y);
        rb.MovePosition(rb.position + movement3D * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 floorNormal = collision.contacts[0].normal.normalized;

        if(Vector3.Dot(floorNormal, Vector3.up) > 0.75)
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
