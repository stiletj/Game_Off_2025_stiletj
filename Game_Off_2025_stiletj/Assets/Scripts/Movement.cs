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

    // Start is called before the first frame update
    void Start()
    {
        isFrozen = false;
        interacting = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0, 0, verticalMoveSpeed * Time.deltaTime));
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
        //else
        //{
        //    if (interacting)
        //    {
        //        animator.SetBool(idle, environmentManager.IsPaused());
        //    }
        //}

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
}
