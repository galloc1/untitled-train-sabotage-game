using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEditor.UI;
using UnityEngine;

public class ButtonsMinigame : MonoBehaviour
{
    //Stores each button's transform to be easily accessed and interacted with
    private Transform[] allButtons;

    private Transform currentButton;
    private Transform currentButtonChild;

    private GameObject player;

    //Stores the randomly generated sequence of buttons that the player is to remember
    private List<Transform> buttonPressSequence;

    public bool addingToSequence;
    public bool endGame;

    //Controls when a new button should be added to the sequence, and when the player is able to attempt to repeat the pattern
    private bool readyToAddToSequence;
    private bool playerIsActing;

    public int initialLengthOfSequence;
    private int currentPositionInButtonPressSequence;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("/Player");
        readyToAddToSequence = true;
        allButtons = new Transform[transform.childCount];
        //Adds each button to the allButtons array (in order)
        for(int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i] = transform.Find("/Security Camera(Clone)/Camera Buttons/"+(i+1).ToString());
        }
        buttonPressSequence = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Runs while the game is generating and showing the player the sequence
        if (addingToSequence)
        {
            if (readyToAddToSequence)
            {
                AddToSequence();
            }
            
            if (buttonPressSequence.Count >= initialLengthOfSequence)
            {
                addingToSequence = false;
                playerIsActing = true;
            }
            currentPositionInButtonPressSequence = 0;
        }
        else if (playerIsActing && Input.GetMouseButtonDown(0))
        {
            TryPressingButton();
        }
    }

    private void AddToSequence()
    {
        currentButton = allButtons[Random.Range(0, allButtons.Length)];
        currentButtonChild = currentButton.GetChild(0);
        buttonPressSequence.Add(currentButton);
        currentButtonChild.GetComponent<Animator>().SetTrigger("ButtonPress");
        readyToAddToSequence = false;
    }

    //For the player to press a button
    private void TryPressingButton()
    {
        RaycastHit hit;
        //Casts a ray from the mouse position on the screen; if clicking an object
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (buttonPressSequence.ElementAt(currentPositionInButtonPressSequence) == hit.transform.parent)
            {
                Debug.Log("hit!");
                if (currentPositionInButtonPressSequence >= initialLengthOfSequence-1)
                {
                    ResetGame();
                    player.GetComponent<PlayerController>().ExitGame();
                }
                else
                {
                    currentPositionInButtonPressSequence++;
                }
            }
            else if (buttonPressSequence.Contains(hit.transform.parent))
            {
                Debug.Log("miss!");
                addingToSequence=true;
                Start();
            }
        }
    }

    private void ResetGame()
    {
        buttonPressSequence = new List<Transform>();
    }

    public void ResetAnimation()
    {
        readyToAddToSequence = true;
    }
}
