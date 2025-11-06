using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DirectionInput
{
    Up, 
    Down, 
    Left, 
    Right
}

public class ArrowPuzzle : MonoBehaviour
{
    [SerializeField] private List<DirectionInput> puzzle = new List<DirectionInput>();
    [SerializeField] private int length;
    [SerializeField] private int currentIndex;
    private int correctKey;                         //0 = no input, >0 = true, <0 = false
    private bool isComplete;

    public ArrowPuzzle(int _length)
    {
        length = _length;
        CreatePuzzle();
        currentIndex = 0;
        correctKey = -1;
        isComplete = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (puzzle.Count == 0)
        {
            length = 5;
            CreatePuzzle();
            currentIndex = 0;
            correctKey = -1;
            isComplete = false;
            Debug.Log("Set Length");
        }
    }

    // Update is called once per frame
    void Update()
    {
        correctKey = 0;

        if (!isComplete)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                correctKey = CheckInput(0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                correctKey = CheckInput((DirectionInput)1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                correctKey = CheckInput((DirectionInput)2);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                correctKey = CheckInput((DirectionInput)3);
            }
        }

        if (correctKey > 0)
        {
            if (currentIndex == length - 1)
            {
                currentIndex = 0;
                isComplete = true;
                Debug.Log("FINISHED");
            }
            else
            {
                currentIndex++;
                Debug.Log("CORRECT");
            }
        }
        else if (correctKey < 0)
        {
            currentIndex = 0;
            Debug.Log("INCORRECT");
        }
        else if (isComplete)
        {
            Debug.Log("COMPLETE");
        }
    }

    private void CreatePuzzle()
    {
        int randDirection = -1;
        puzzle.Clear();

        for (int i = 0; i < length; i++)
        {
            randDirection = Random.Range(0, 4);

            puzzle.Add((DirectionInput)randDirection);
        }
    }

    private int CheckInput(DirectionInput input)
    {
        int res = -1;
        if (puzzle[currentIndex] == input)
        {
            res = 1;
        }

        return res;
    }
}
