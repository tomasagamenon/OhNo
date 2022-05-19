using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;

    [SerializeField] private QuestManager am;

    public float speed;
    public float diagonalSpeed;
    public float sprintSpeed;
    public float walkSpeed;
    private float _speed;
    public float jumpForce;

    private bool _isDiagonalWalking;
    private bool _isSprinting;
    private bool _isWalking;

    public PlayerGravity playerGravity;

    float pushPower = 2.0f;

    private void Awake()
    {
        //am = GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<QuestManager>();
    }
    private void Start()
    {
        _speed = speed;
        cc.detectCollisions = true;
    }
    void LateUpdate()
    {
        
        float movementX = Input.GetAxis("Horizontal") * Time.deltaTime;
        float movementZ = Input.GetAxis("Vertical") * Time.deltaTime;


        if(Input.GetButton("Run") && !Input.GetButton("Horizontal"))
        {
            _speed = sprintSpeed;
            _isSprinting = true;
        }else if(Input.GetButtonUp("Run") && _isSprinting)
        {
            _speed = speed;
            _isSprinting = false;
        }

        if(Input.GetButton("Walk") && !_isSprinting)
        {
            _speed = walkSpeed;
            if (_isDiagonalWalking) _speed = diagonalSpeed;
            _isWalking = true;
        }else if(Input.GetButtonUp("Walk") && _isWalking)
        {
            _speed = speed;
            if (_isDiagonalWalking) _speed = diagonalSpeed;
            _isWalking = false;
        }

        if (Input.GetButton("Horizontal") && Input.GetButton("Vertical") && !_isDiagonalWalking)
        {
            diagonalSpeed = _speed / 1.5f;
            _speed = diagonalSpeed;
            _isDiagonalWalking = true;
        }
        else if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical") && _isDiagonalWalking)
        {
            _speed = speed;
            _isDiagonalWalking = false;
        }

        Vector3 movement = transform.right * movementX + transform.forward * movementZ;

        cc.Move(movement * _speed);

        

        if (playerGravity.isGrounded && Input.GetButtonDown("Jump"))
        {
            playerGravity.velocity.y *= -jumpForce;
            //am.AddAmount("Jump", 1);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("MapLimit"))
        {
            float force = 2.5f;

            Vector3 dir = hit.point - transform.position;
            dir = -dir.normalized;
            
            cc.Move(dir * force);

            //hit.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            hit.gameObject.GetComponentInParent<DialogueTrigger>().TriggerDialogue();
        }

        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }

}
