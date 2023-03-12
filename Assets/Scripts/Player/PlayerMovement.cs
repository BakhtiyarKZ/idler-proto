using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dependency")]
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private new Camera camera;
    [SerializeField] private GameManager gm;

    [Header("Variables")]
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Vector3 targetVector;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float moveX;
    [SerializeField] private float moveZ;
    [SerializeField] private bool runStarted;
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private bool canMove;

    void Start()
    {
        animator = GetComponent<Animator>();
        canMove= true;
    }

    // Update is called once per frame
    void Update()
    {

        //if (gm.currentState == GameManager.States.Menu) return;
        if (!canMove)
        {
            moveX = 0;
            moveZ = 0;
            movementDirection = new Vector3(moveX, 0, moveZ);
            moveDirection.Normalize();

            controller.Move(movementDirection * moveSpeed * Time.deltaTime);

            return;
        }
        moveX = joystick.Horizontal;
        moveZ = joystick.Vertical;
        

        movementDirection = new Vector3(moveX, 0, moveZ);
        moveDirection.Normalize();

        controller.Move(movementDirection * moveSpeed * Time.deltaTime);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            targetVector = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
            RotateTowardsTarget(targetVector);
            animator.SetFloat("Horizontal", moveX * speedMultiplier);
            animator.SetFloat("Vertical", moveZ * speedMultiplier);

            animator.speed = movementVector.magnitude * 2f;
            movementVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        }
        else
        {
            animator.SetFloat("Horizontal", moveX * speedMultiplier);
            animator.SetFloat("Vertical", moveZ * speedMultiplier);
            animator.speed = 1f;
        }

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }


    public void RotateTowardsTarget(Vector3 targetVector)
    {
        if (targetVector == Vector3.zero) return;
        var rotation = Quaternion.LookRotation(targetVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 60f);

    }

    public void SetCanMove(bool _canMove)
    {
        canMove = _canMove;
    }
}
