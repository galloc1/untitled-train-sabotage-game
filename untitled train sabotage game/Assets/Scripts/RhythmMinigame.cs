using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMinigame : MonoBehaviour
{
    //Stores the starting position of each element of the canvas (used to reset their positions)
    private List<Vector3> canvasElementsStartingPositions;


    // Start is called before the first frame update
    void Start()
    {
        //Gets the starting position of each canvas element and stores them in a list
        canvasElementsStartingPositions = new List<Vector3>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            canvasElementsStartingPositions.Add(child.GetComponent<RectTransform>().anchoredPosition);
        }
    }

    //Initialise is used to set the minigame to its beginning state
    void Initialise()
    {
        //Resets the position of each element on the canvas
        for(int i= 0; i < canvasElementsStartingPositions.Count; i++)
        {
            Transform child = transform.GetChild(i);
            child.GetComponent<RectTransform>().anchoredPosition = canvasElementsStartingPositions[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
