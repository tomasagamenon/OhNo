using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    public CharacterController cc;

    [SerializeField] private float surfaceCheck = 1.2f;
    public float gravity;
    public Vector3 velocity;

    public bool isGrounded;
    public bool headHit;

    private void LateUpdate()
    {
        headHit = Physics.Raycast(transform.position, Vector3.up, surfaceCheck);
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        if (headHit && velocity.y > 0) velocity.y = -0.5f;
        RaycastGroundCheck();
    }
    void RaycastGroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, surfaceCheck);
        headHit = Physics.Raycast(transform.position, Vector3.up, surfaceCheck);
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        if (headHit && velocity.y > 0) velocity.y = -0.5f;
    }
}
