using UnityEngine;

public class AnimateMovement : MonoBehaviour
{
    public GameObject scrollEnvironment;

    private Movement movement;
    private Animator animator;

    private bool isLeft;
    private bool isRight;

    private string left = "Left";
    private string right = "Right";
    private string idle = "Idle";

    private ScrollEnvironment scrollManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        scrollManager = scrollEnvironment.GetComponent<ScrollEnvironment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!movement.isFrozen)
        {
            isLeft = Input.GetKey(KeyCode.A);
            isRight = Input.GetKey(KeyCode.D);

            animator.SetBool(left, isLeft);
            animator.SetBool(right, isRight);

            animator.SetBool(idle, scrollManager.IsPaused());
        }
    }
}
