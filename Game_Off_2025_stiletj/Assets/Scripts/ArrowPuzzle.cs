using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

enum DirectionInput
{
    Up, 
    Down, 
    Left, 
    Right
}

public class ArrowPuzzle : MonoBehaviour
{
    public GameObject canvas;
    public List<GameObject> arrowImages;
    public bool isComplete;
    public bool isIncorrect;
    public Material correctMaterial;
    public Material incorrectMaterial;
    public float waitTime = 1f;

    [SerializeField] private List<DirectionInput> puzzle = new List<DirectionInput>();
    [SerializeField] private int length;
    [SerializeField] private int currentIndex;
    private int correctKey;                         //0 = no input, >0 = true, <0 = false
    private bool isDrawn;
    private List<GameObject> arrowObjects = new List<GameObject>();
    private float timer;
    private bool wait;

    public void CreateArrowPuzzle(int _length, GameObject _canvas)
    {
        length = _length;
        canvas = _canvas;
        CreatePuzzle();
        currentIndex = 0;
        correctKey = -1;
        isComplete = false;
        isIncorrect = false;
        isDrawn = false;
        timer = waitTime;
        wait = false;
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
        }
        
        isDrawn = false;

        DrawPuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDrawn)
        {
            DrawPuzzle();
        }

        correctKey = 0;

        timer -= Time.deltaTime;
        if (wait && timer <= 0)
        {
            wait = false;

            for (int i = 0; i < arrowObjects.Count; i++)
            {
                arrowObjects[i].GetComponent<Image>().material = null;
            }
        }

        if (!wait)
        {
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
                    isIncorrect = false;
                }
                else
                {
                    arrowObjects[currentIndex].GetComponent<Image>().material = correctMaterial;
                    currentIndex++;
                }
            }
            else if (correctKey < 0)
            {
                currentIndex = 0;

                for (int i = 0; i < arrowObjects.Count; i++)
                {
                    arrowObjects[i].GetComponent<Image>().material = incorrectMaterial;
                }

                isIncorrect = true;

                wait = true;
                timer = waitTime;
            }
        }
    }

    private void DrawPuzzle()
    {
        for (int i = 0; i < length; i++)
        {
            arrowObjects.Add(Instantiate(arrowImages[(int)puzzle[i]]));
            arrowObjects[i].transform.SetParent(canvas.transform, false);
            arrowObjects[i].transform.position = new Vector3(
                arrowObjects[i].transform.position.x + (i * 100), 
                arrowObjects[i].transform.position.y, 
                arrowObjects[i].transform.position.z);
        }

        isDrawn = true;
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

    public void DeletePuzzle()
    {
        for (int i = 0; i < length; i++)
        {
            Destroy(arrowObjects[i]);
        }
    }
}
