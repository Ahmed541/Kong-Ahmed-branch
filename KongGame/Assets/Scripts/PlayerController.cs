using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentState
{
    STANDING = 0,
    WALKING,
    RUNNING,
    CROUCHING,
    JUMPING,
    CLIMBING,
    VAULTING
}

public class PlayerController : MonoBehaviour
{
    //VARIABLES
    //Regarding player movement
    private float walkSpeed_f = 5f;
    private float crouchSpeed_f = 1.5f;
    private float runSpeed_f = 10f;
    private float smoothTurnTime_f = 0.1f;
    private float smoothTurnVelocity_f;
    private float targetAngle_f;
    private float jumpForce_f = 3.0f;
    private float yVelocity_f;

    //Whether the player is crouching or running
    private bool running_b = false;
    private bool crouching_b = false;
    
    public Vector2 inputDirection_v2;

    public CharacterController controller;

    public CurrentState currentState_e;

    //Regarding camera control
    public Transform camera_t;
    public Vector3 cameraTarget_v3;
    public float interpolationSpeed_f;

    //Animator component
    public Animator animator;
    

    //METHODS
    // Start is called before the first frame update
    void Start()
    {
        //Initial setup for values and the overally player
        currentState_e = CurrentState.STANDING;
    }

    Vector2 GetInputDirection()
    {
        Vector2 direction_v2 = new Vector2();
        //Process input
        direction_v2.x = Input.GetAxis("Horizontal");
        direction_v2.y = Input.GetAxis("Vertical");

        //set the player state to standing if the control values are (0, 0)
        if (direction_v2.x == 0 && direction_v2.y == 0)
        {
            currentState_e = CurrentState.STANDING;
            running_b = false;
            //Animator Components
            animator.SetBool("Idle", true);
            animator.SetBool("Running", false);
            animator.SetBool("Crouching", false);
            animator.SetBool("Walking", false);
        }
        else
        {
            //Checks if the player presses the toggle sprint button
            if (Input.GetAxis("ToggleSprint") != 0)
            {
                //Start sprinting and set the player's state
                running_b = true;
                currentState_e = CurrentState.RUNNING;
                //Animator Components
                animator.SetBool("Idle", false);
                animator.SetBool("Running", true);
                animator.SetBool("Crouching", false);
                animator.SetBool("Walking", false);

                Debug.Log("Started running!");
            }
            else if (running_b == false)//If the player hasn't pressed the button and is not sprinting
            {
                currentState_e = CurrentState.WALKING;
                //Animator Components
                animator.SetBool("Idle", false);
                animator.SetBool("Running", false);
                animator.SetBool("Crouching", false);
                animator.SetBool("Walking", true);
            }

            //Checks if the player presses the toggle crouch button
            if (Input.GetAxis("ToggleCrouch") != 0)
            {
                //Start sprinting
                crouching_b = true;
                currentState_e = CurrentState.CROUCHING;
                //Animator Components
                animator.SetBool("Idle", false);
                animator.SetBool("Running", false);
                animator.SetBool("Crouching", true);
                animator.SetBool("Walking", true);

                Debug.Log("Started crouching!");
            }
            else if (crouching_b == false)//If the player hasn't pressed the button and is not sprinting
            {
                currentState_e = CurrentState.WALKING;
                //Animator Components
                animator.SetBool("Idle", false);
                animator.SetBool("Running", false);
                animator.SetBool("Crouching", false);
                animator.SetBool("Walking", true);

            }
        }
        //Return the vector with a maginitude of 1
        return direction_v2.normalized;
    }

    void CheckForJump()
    {
        if(Input.GetAxis("Jump") != 0 && controller.isGrounded)
        {
            yVelocity_f = jumpForce_f;
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded)
        {
            yVelocity_f = 0;
        }
        else
        {
            yVelocity_f += (Physics.gravity.y * Time.deltaTime);
        }
    }

    void MovePlayer(Vector2 _inputDir)
    {
        Vector3 moveDir_v3 = Quaternion.Euler(0, targetAngle_f, 0) * Vector3.forward * _inputDir.magnitude;
        moveDir_v3.y = yVelocity_f;
        //Debug.Log(moveDir_v3);
        //Moves the player at different speeds depending on if they are walking, running or crouching
        switch (currentState_e)
        {
            case CurrentState.WALKING:
                Debug.Log(moveDir_v3 * walkSpeed_f * Time.deltaTime);
                controller.Move(moveDir_v3 * walkSpeed_f * Time.deltaTime);
                break;
            case CurrentState.RUNNING:
                controller.Move(moveDir_v3 * runSpeed_f * Time.deltaTime);
                break;
            case CurrentState.CROUCHING:
                controller.Move(moveDir_v3 * crouchSpeed_f * Time.deltaTime);
                break;
            default:
                controller.Move(moveDir_v3 * walkSpeed_f * Time.deltaTime);
                break;
        }
    }

    void RotatePlayer(Vector2 _inputDir_v2)
    {
        //Create a transform point for the player to look at based on the input direction
        targetAngle_f = Mathf.Atan2(_inputDir_v2.x, _inputDir_v2.y) * Mathf.Rad2Deg + camera_t.eulerAngles.y;
        float angle_f = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle_f, ref smoothTurnVelocity_f, smoothTurnTime_f);
        transform.rotation = Quaternion.Euler(0, angle_f, 0);
    }

    

    // Update is called once per frame
    void Update()
    {
        //Get the controller or keyboard input and move the player based on the input value
        inputDirection_v2 = GetInputDirection();
        ApplyGravity();
        CheckForJump();
        RotatePlayer(inputDirection_v2);
        MovePlayer(inputDirection_v2);


        //switch (currentState_e)
        //{
        //    case CurrentState.WALKING:
        //        animator.SetBool("Walking", true);
        //        animator.SetBool("Running", false);
        //        animator.SetBool("Crouching", false);
        //        animator.SetBool("Idle", false);
        //        Debug.Log("Working");
        //        break;
        //    case CurrentState.RUNNING:
        //        animator.SetBool("Walking", false); ;
        //        animator.SetBool("Running", true);
        //        animator.SetBool("Crouching", false);
        //        animator.SetBool("Idle", false);
        //        break;
        //    case CurrentState.CROUCHING:
        //        animator.SetBool("Walking", false);
        //        animator.SetBool("Running", false);
        //        animator.SetBool("Crouching", true);
        //        animator.SetBool("Idle", false);
        //        break;
        //    default:
        //        animator.SetBool("Walking", false);
        //        animator.SetBool("Running", false);
        //        animator.SetBool("Crouching", false);
        //        animator.SetBool("Idle", true);
        //        break;
        //}

        //if (Input.GetAxis("Jump") != 0 && controller.isGrounded)
        //{
        //    animator.SetBool("Jump", true);
        //}
        //else if (controller.isGrounded)
        //{
        //    animator.SetBool("Jump", false);
        //}

    }
}
