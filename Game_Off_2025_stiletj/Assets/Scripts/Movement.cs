using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float horizontalMoveSpeed;
    public float verticalMoveSpeed;
    public ScrollEnvironment environmentManager;

    private bool isFrozen;

    // Start is called before the first frame update
    void Start()
    {
        isFrozen = false;
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
        }
    }

    public void FreezeMovement()
    {
        isFrozen = true;
        environmentManager.Pause();
    }

    public void UnFreezeMovement(bool increment)
    {
        isFrozen = false;
        if (increment)
        {
            environmentManager.IncrementDefaultSpeed(-1);
        }
        environmentManager.Play();
    }
}
