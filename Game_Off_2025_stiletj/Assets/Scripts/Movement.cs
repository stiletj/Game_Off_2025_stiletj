using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float horizontalMoveSpeed;
    public float verticalMoveSpeed;
    public ScrollEnvironment environmentManager;

    public bool isFrozen;
    public bool interacting;

    private Animator animator;

    private bool isLeft;
    private bool isRight;

    private string left = "Left";
    private string right = "Right";
    private string idle = "Idle";

    private bool freezeForward;

    // Start is called before the first frame update
    void Start()
    {
        isFrozen = false;
        interacting = false;
        animator = GetComponent<Animator>();
        freezeForward = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            if (!freezeForward)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(new Vector3(0, 0, verticalMoveSpeed * Time.deltaTime));
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-horizontalMoveSpeed * Time.deltaTime, 0, 0));
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0, 0, -verticalMoveSpeed * Time.deltaTime));
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(horizontalMoveSpeed * Time.deltaTime, 0, 0));
            }

            isLeft = Input.GetKey(KeyCode.A);
            isRight = Input.GetKey(KeyCode.D);

            animator.SetBool(left, isLeft);
            animator.SetBool(right, isRight);

            animator.SetBool(idle, environmentManager.IsPaused());
        }

        animator.SetBool(idle, environmentManager.IsPaused());
    }

    public void FreezeMovement()
    {
        isFrozen = true;
    }

    public void UnFreezeMovement()
    {
        isFrozen = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            freezeForward = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            freezeForward = false;
        }
    }
}
