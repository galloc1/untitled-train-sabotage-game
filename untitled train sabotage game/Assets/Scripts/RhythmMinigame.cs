using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMinigame : MonoBehaviour
{
    //Stores the starting position of each element of the canvas (used to reset their positions)
    private List<Vector3> carriageSpriteStartingPositions;
    //Stores the prefab instance that contains each carriage sprite
    private List<Transform> carriageSprites;


    // Start is called before the first frame update
    void Start()
    {
        //Gets the starting position of each canvas element and stores them in a list
        carriageSpriteStartingPositions = new List<Vector3>();
        for (int i = 0; i < 5; i++)
        {
            Transform child = transform.GetChild(i).GetChild(0);
            carriageSpriteStartingPositions.Add(child.GetComponent<RectTransform>().anchoredPosition);
        }

        //Gets a list of carriage prefab instances (grants access to the carriage sprites)
        carriageSprites = new List<Transform>();
        for (int i=0; i < 5; i++)
        {
            Transform child = transform.GetChild(i);
            carriageSprites.Add(child);
        }
    }

    //Initialise is used to set the minigame to its beginning state
    void Initialise()
    {
        //Resets the position of each element on the canvas
        for(int i= 0; i < carriageSpriteStartingPositions.Count; i++)
        {
            Transform child = transform.GetChild(i);
            child.GetComponent<RectTransform>().anchoredPosition = carriageSpriteStartingPositions[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
