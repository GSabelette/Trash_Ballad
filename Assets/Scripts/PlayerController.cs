using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] float speed = 3f;// m/s
    [SerializeField] float runSpeed = 6f;// m/s

    [SerializeField] float jumpSpeed = 5f;

    [SerializeField] float gravity = 13f;// m/s/s
    [SerializeField] float gravityStrong = 20f;// m/s/s
    [SerializeField] float maxFallSpeed = 20f;// m/s

    [SerializeField] float smoothSpeed = 8f;
    [SerializeField] float airSmoothSpeed = 1f;

    Vector3 dir;
    float targetSpeed;
    float targetGravity;
    float targetSmooth;
    float fallSpeed;

    public Vector3 velocity;
    public bool onGround;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        controller.minMoveDistance = 0f; // To avoid problems with jumping
    }

    // Update is called once per frame
    void Update()
    {
        if (TabletController.tabletState == TabletController.TabletState.SIDE)
        {
            Movement();
        } 
    }

    void Movement()
    {
        /* Conditional variables:
         * - target_speed for higher speed when running
         * - target_smooth for slower movement changes when in the air
         * - targetGravity for adjustable jump height
         * */
        targetSpeed = Input.GetButton("Fire3") ? Mathf.Lerp(targetSpeed, runSpeed, 2f * Time.deltaTime) : speed;
        targetSmooth = onGround ? smoothSpeed : airSmoothSpeed;
        if (fallSpeed < 0) targetGravity = gravityStrong;

        // Get normalized input dir
        dir = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized;
        // For smooth movement changes, Lerp the velocity
        velocity = Vector3.Lerp(velocity, new Vector3(dir.x, 0f, dir.z) * targetSpeed, targetSmooth * Time.deltaTime);

        onGround = controller.isGrounded;
        //if (onGround) fallSpeed = 0f; //-> Removed because it created bouncing on downwards slopes
        fallSpeed = Mathf.Clamp(fallSpeed - targetGravity * Time.deltaTime, -maxFallSpeed, Mathf.Infinity);

        // Requires the minMoveDistance of the CharacterController to be 0, see Start()
        if (onGround && Input.GetButtonDown("Jump"))
        {
            fallSpeed = jumpSpeed;
            targetGravity = gravity;
            onGround = false;
        }
        if (Input.GetButtonUp("Jump"))
        {
            targetGravity = gravityStrong;
        }

        controller.Move(new Vector3(velocity.x, fallSpeed, velocity.z) * Time.deltaTime);
    }
}
