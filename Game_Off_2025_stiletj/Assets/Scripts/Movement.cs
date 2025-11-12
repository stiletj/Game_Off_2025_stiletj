using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
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
            if (Input.GetKeyDown(KeyCode.W))
            {
                environmentManager.SpeedUp(-2);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                environmentManager.ResetSpeed();
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                environmentManager.SlowDown(-2);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                environmentManager.ResetSpeed();
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
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
