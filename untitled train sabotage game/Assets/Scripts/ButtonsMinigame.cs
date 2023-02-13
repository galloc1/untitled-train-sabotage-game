using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsMinigame : MonoBehaviour
{
    //Stores each button's transform to be easily accessed and interacted with
    private Transform[] allButtons;

    private Transform currentButton;
    private Transform currentButtonChild;

    //Stores the randomly generated sequence of buttons that the player is to remember
    private List<Transform> buttonPressSequence;

    public bool addingToSequence;

    //Controls when a new button should be added to the sequence, and when the player is able to attempt to repeat the pattern
    private bool readyToAddToSequence;
    private bool playerIsActing;

    public int initialLengthOfSequence;

    // Start is called before the first frame update
    void Start()
    {
        readyToAddToSequence = true;
        allButtons = new Transform[transform.childCount];
        //Adds each button to the allButtons array (in order)
        for(int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i] = transform.GetChild(i);
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
            else
            {
                Debug.Log(currentButtonChild.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base Layer.New State"));
                if (!currentButtonChild.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base Layer.New State"))
                {
                    readyToAddToSequence = true;
                }
            }
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
}
