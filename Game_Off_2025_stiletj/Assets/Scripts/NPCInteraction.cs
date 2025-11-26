using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject puzzlePrefab;
    public GameObject canvas;
    public GameObject speechBubble;

    private GameObject puzzle;
    private GameObject player = null;
    private int difficulty;
    private bool used;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = 0;
        used = false;
        speechBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (puzzle.GetComponent<ArrowPuzzle>().isIncorrect)
            {
                speechBubble.SetActive(true);
                speechBubble.GetComponentInChildren<TextMeshProUGUI>().text = "Who the hell are you?";
            }

            if (puzzle.GetComponent<ArrowPuzzle>().isComplete)
            {
                speechBubble.SetActive(true);
                speechBubble.GetComponentInChildren<TextMeshProUGUI>().text = "Hey man, what's up?";

                puzzle.GetComponent<ArrowPuzzle>().DeletePuzzle();
                Destroy(puzzle);
                puzzle = null;

                player.GetComponent<Movement>().UnFreezeMovement();
                player.GetComponent<Movement>().interacting = false;
                GameObject.Find("Environment Manager").GetComponent<ScrollEnvironment>().Play();
                GameObject.Find("Environment Manager").GetComponent<ScrollEnvironment>().IncrementDefaultSpeed(-1);
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
        player.GetComponent<Movement>().interacting = true;
        GameObject.Find("Environment Manager").GetComponent<ScrollEnvironment>().Pause();

        puzzle = Instantiate(puzzlePrefab);
        puzzle.transform.parent = transform;
        puzzle.GetComponent<ArrowPuzzle>().CreateArrowPuzzle(5 + difficulty, canvas);
    }
}
