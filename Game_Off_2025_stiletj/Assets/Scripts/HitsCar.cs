using UnityEngine;

public class HitsCar : MonoBehaviour
{
    public Collider front;

    private GameObject gameManager;
    private ScrollEnvironment environmentManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        environmentManager = GameObject.Find("Environment Manager").GetComponent<ScrollEnvironment>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.layer == 8)
        {
            Collider thisCollider = collision.contacts[0].thisCollider;

            if (thisCollider == front)
            {
                gameManager.GetComponent<CarSpawner>().PauseCar(gameObject);
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                environmentManager.Pause();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.layer == 8)
        {

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.layer == 8)
        {
            if (collision.collider == front)
            {
                gameManager.GetComponent<CarSpawner>().PlayCar(gameObject);
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                environmentManager.Play();
            }
        }
    }
}
