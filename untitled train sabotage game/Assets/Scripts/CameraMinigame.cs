using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMinigame : MonoBehaviour
{
    private Transform[] buttons;

    private Vector3 buttonStart;

    public bool sequencing;

    private bool midSequence;

    private int buttonIndex;

    // Start is called before the first frame update
    void Start()
    {
        buttons = new Transform[transform.childCount];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = transform.GetChild(i);
        }
        midSequence = false;
        sequencing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sequencing)
        {
            if (!midSequence)
            {
                buttonIndex = Random.Range(0, buttons.Length - 1);
                midSequence = true;
                buttonStart = buttons[buttonIndex].position;
            }
            ExtendSequence();
        }
    }

    //Chooses a random button to light up and adds it to the sequence
    void ExtendSequence()
    {
        Transform button = buttons[buttonIndex];
        button.position = Vector3.MoveTowards(button.position, buttonStart + ( button.forward * 0.007f ), 0.00025f);
    }
}
