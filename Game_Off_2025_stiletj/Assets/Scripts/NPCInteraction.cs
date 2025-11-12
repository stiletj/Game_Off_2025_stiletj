using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject puzzlePrefab;
    public GameObject canvas;
    private GameObject puzzle;
    private GameObject player = null;
    private int difficulty;
    private bool used;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = 0;
        used = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (puzzle.GetComponent<ArrowPuzzle>().isComplete)
            {
                puzzle.GetComponent<ArrowPuzzle>().DeletePuzzle();
                Destroy(puzzle);
                puzzle = null;

                player.GetComponent<Movement>().UnFreezeMovement(true);
                player = null;

                ScoreTracker.IncrementInteractionScore();

                used = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Player") && !used)
        {
            player = other.gameObject;

            Interact();
        }
    }

    private void Interact()
    {
        player.GetComponent<Movement>().FreezeMovement();

        puzzle = Instantiate(puzzlePrefab);
        puzzle.transform.parent = transform;
        puzzle.GetComponent<ArrowPuzzle>().CreateArrowPuzzle(5 + difficulty, canvas);
    }
}
