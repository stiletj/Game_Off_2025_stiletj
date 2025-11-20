using UnityEngine;

public class AnimateMovement : MonoBehaviour
{
    public ScrollEnvironment scrollEnvironment;

    private Animator animator;

    private bool isLeft;
    private bool isRight;

    private string left = "Left";
    private string right = "Right";
    private string idle = "Idle";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isLeft = Input.GetKey(KeyCode.A);
        isRight = Input.GetKey(KeyCode.D);

        animator.SetBool(left, isLeft);
        animator.SetBool(right, isRight);

        animator.SetBool(idle, scrollEnvironment.IsPaused());
    }
}
