using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RhythmMinigame : MonoBehaviour
{
    //Stores the starting position of each element of the canvas (used to reset their positions)
    private List<Vector3> carriageSpriteStartingPositions;
    //Stores the carriage sprites
    private List<Transform> carriageSprites;
    //Stores the arrows
    private List<GameObject> arrows;

    public float arrowSpawnBuffer;
    public float arrowSpawnTime;

    public float rampUpScale;
    public float baseDifficulty;
    private float timeSinceStart;

    private bool playing;

    public GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the starting position of each canvas element and stores them in a list
        carriageSpriteStartingPositions = new List<Vector3>();
        arrows = new List<GameObject>();
        for (int i = 0; i<transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
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
    public void Initialise()
    {
        //Resets the position of each element on the canvas
        for(int i= 0; i < carriageSpriteStartingPositions.Count; i++)
        {
            Transform child = transform.GetChild(i);
            child.GetComponent<RectTransform>().anchoredPosition = carriageSpriteStartingPositions[i];
        }
        timeSinceStart = 0.0f;
        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            timeSinceStart += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (playing)
        {
            AttemptArrowSpawn();
        }
    }

    //Adds arrows to the game
    private void AttemptArrowSpawn()
    {
        if (arrowSpawnTime > 0.5f+(arrowSpawnBuffer/(1+(Mathf.Sqrt(timeSinceStart)*rampUpScale))))
        {
            if(Random.Range(0, 1+Mathf.RoundToInt(baseDifficulty/(1+(Mathf.Sqrt(timeSinceStart)*rampUpScale)))) == 0)
            {
                Debug.Log("Spawned at " + timeSinceStart.ToString());
                arrows.Add(Instantiate(arrow, new Vector3(400, 104, 0), arrow.transform.rotation, transform));
                arrowSpawnTime = 0.0f;
            }
        }
        else
        { arrowSpawnTime += Time.deltaTime; }
    }
}
