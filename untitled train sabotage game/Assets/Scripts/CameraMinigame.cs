using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMinigame : MonoBehaviour
{
    /*
    private Transform[] buttons;
    private List<Transform> sequence;
    private Transform button;

    public bool sequencing;

    public int sequenceLength;

    private bool playing;
    private bool midSequence;

    private int buttonIndex;

    private float buttonHoldTimer;

    public Material activeButton;
    public Material inactiveButton;

    // Start is called before the first frame update
    void Start()
    {
        //Adds transform of each button to a list
        buttons = new Transform[transform.childCount];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = transform.GetChild(i);
        }

        //Empties the sequence array and button Transform
        sequence = new List<Transform>();
        //button = null;

        sequencing = false;
        playing = false;
        midSequence = false;
        buttonIndex = 0;
        buttonHoldTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (sequencing)
        {
            if (!midSequence)
            {
                buttonHoldTimer = 0;
                buttonIndex = Random.Range(0, buttons.Length - 1);
                button = buttons[buttonIndex];
                if (sequence.Count >= sequenceLength)
                {
                    buttonIndex = 0;
                    sequencing = false;
                    playing = true;
                }
                else
                {
                    sequence.Add(button);
                }
                midSequence = true;
            }
            ExtendSequence();
        }
        
        else if(playing && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction, Color.white, 100.0f, true);
                if (sequence.ElementAt(buttonIndex) == hit.transform.gameObject.transform)
                {
                    buttonIndex++;
                }
                else if (buttons.Contains(hit.transform.gameObject.transform))
                {
                    Debug.Log("Start again");
                    Start();
                    sequencing = true;
                }
            }
        }
    }

    //Chooses a random button to light up and adds it to the sequence
    void ExtendSequence()
    {
        buttonHoldTimer += Time.deltaTime;
        if (buttonHoldTimer > periodButtonIsHeld)
        {
            button.gameObject.GetComponent<MeshRenderer>().material = inactiveButton;
        }
        else if (buttonHoldTimer > Time.deltaTime)
        {
            button.gameObject.GetComponent<MeshRenderer>().material = activeButton;
        }
        if (buttonHoldTimer > intervalBetweenButtonPresses)
        {
            midSequence = false;
        }
    }*/
}
