using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsMinigame : MonoBehaviour
{
    //Stores each button's transform to be easily accessed and interacted with
    private Transform[] allButtons;

    private Transform currentButton;

    //Stores the randomly generated sequence of buttons that the player is to remember
    private List<Transform> buttonPressSequence;

    bool addingToSequence;

    //Controls when a new button should be added to the sequence, and when the player is able to attempt to repeat the pattern
    private bool readyToAddToSequence;
    private bool playerIsActing;

    private float periodButtonHasBeenHeld;

    public Material activatedButtonMaterial;
    public Material deactivatedButtonMaterial;

    public int initialLengthOfSequence;

    // Start is called before the first frame update
    void Start()
    {
        allButtons = new Transform[transform.childCount];
        //Adds each button to the allButtons array (in order)
        for(int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i] = transform.GetChild(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Runs while the game is generating and showing the player the sequence
        if (addingToSequence)
        {
            if (readyToAddToSequence)
            {
                if(buttonPressSequence.Count >= initialLengthOfSequence)
                {
                    addingToSequence = false;
                    playerIsActing = true;
                }
            }
        }
    }

    private void AddToSequence()
    {
        currentButton = allButtons[Random.Range(0, allButtons.Length)];
    }
}
