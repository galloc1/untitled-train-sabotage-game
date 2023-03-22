using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RhythmMinigame : MonoBehaviour
{
    //Stores the carriage sprites
    private List<Transform> carriageSprites;
    //Stores the arrows
    private List<GameObject> arrows;
    private List<GameObject> activeArrows;

    private List<float> song1 = new List<float>() { 0.25f, 0.75f, 1.25f, 1.75f };
    private List<float> activeSong;
    private List<float> posInSong;


    private float timeSinceStart;

    private bool playing;

    public GameObject arrow;

    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    // Start is called before the first frame update
    void Start()
    {
        arrows = new List<GameObject>();
        activeArrows = new List<GameObject>();

        //Gets a list of carriage prefab instances (grants access to the carriage sprites)
        carriageSprites = new List<Transform>();
        for (int i = 0; i < 5; i++)
        {
            Transform child = transform.GetChild(i);
            carriageSprites.Add(child);
        }
        activeSong = song1.ToList<float>();
        posInSong = song1.ToList<float>();
        timeSinceStart = 0.0f;
        playing = true;
    }

    //Initialise is used to set the minigame to its beginning state
    public void Initialise()
    {
        arrows = new List<GameObject>();
        activeArrows = new List<GameObject>();

        //Gets a list of carriage prefab instances (grants access to the carriage sprites)
        carriageSprites = new List<Transform>();
        for (int i = 0; i < 5; i++)
        {
            Transform child = transform.GetChild(i);
            carriageSprites.Add(child);
        }
        activeSong = song1.ToList<float>();
        posInSong = song1.ToList<float>();
        timeSinceStart = 0.0f;
        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            timeSinceStart += Time.deltaTime;
            if (posInSong.Count > 0)
            {
                AttemptArrowSpawn();
                GetPlayerInput();
            }
            else if (activeSong.Count > 0)
            {
                GetPlayerInput();
            }
            else { playing = false; }
        }
    }

    private void FixedUpdate()
    {
        if (playing)
        {
            if(activeSong.Count > 0)
            {
                MoveArrows();
            }
        }
    }

    //Adds arrows to the game
    private void AttemptArrowSpawn()
    {
        //if (arrowSpawnTime > 0.5f+(arrowSpawnBuffer/(1+(Mathf.Sqrt(timeSinceStart)*rampUpScale))))
        //{
        //    if(Random.Range(0, 1+Mathf.RoundToInt(baseDifficulty/(1+(Mathf.Sqrt(timeSinceStart)*rampUpScale)))) == 0)
        //    {
        if (timeSinceStart > posInSong[0])
        {
            posInSong.RemoveAt(0);
            GameObject obj = Instantiate(arrow, new Vector3(573, 150, 0), arrow.transform.rotation, transform);
            int rand = Random.Range(0, 3);
            if (rand == 0)
            {
                obj.transform.GetChild(0).GetComponent<Image>().sprite = up;
            }
            else if (rand == 1)
            {
                obj.transform.GetChild(0).GetComponent<Image>().sprite = down;
            }
            else if (rand == 2)
            {
                obj.transform.GetChild(0).GetComponent<Image>().sprite = left;
            }
            else
            {
                obj.transform.GetChild(0).GetComponent<Image>().sprite = right;
            }
            arrows.Add(obj);
            activeArrows.Add(obj);
        }
        //    }
        //}
        //else
        //{ arrowSpawnTime += Time.deltaTime; }
    }

    //Handles player input
    private void GetPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (activeArrows[0].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x > -68.5 && activeArrows[0].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x < 90.5)
            {
                if(activeArrows[0].transform.GetChild(0).GetComponent<Image>().sprite == up)
                {
                    Destroy(activeArrows[0]);
                    activeArrows.RemoveAt(0);
                }
                else
                {
                    Debug.Log("wrong arrow fuckface");
                }
            }
            else
            {
                Debug.Log("shit reaction time");
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (activeArrows[0].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x > -68.5 && activeArrows[0].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x < 90.5)
            {
                if (activeArrows[0].transform.GetChild(0).GetComponent<Image>().sprite == down)
                {
                    Destroy(activeArrows[0]);
                    activeArrows.RemoveAt(0);
                }
                else
                {
                    Debug.Log("wrong arrow fuckface");
                }
            }
            else
            {
                Debug.Log("shit reaction time");
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (activeArrows[0].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x > -68.5 && activeArrows[0].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x < 90.5)
            {
                if (activeArrows[0].transform.GetChild(0).GetComponent<Image>().sprite == left)
                {
                    Destroy(activeArrows[0]);
                    activeArrows.RemoveAt(0);
                }
                else
                {
                    Debug.Log("wrong arrow fuckface");
                }
            }
            else
            {
                Debug.Log("shit reaction time");
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (activeArrows[0].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x > -68.5 && activeArrows[0].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x < 90.5)
            {
                if (activeArrows[0].transform.GetChild(0).GetComponent<Image>().sprite == right)
                {
                    Destroy(activeArrows[0]);
                    activeArrows.RemoveAt(0);
                }
                else
                {
                    Debug.Log("wrong arrow fuckface");
                }
            }
            else
            {
                Debug.Log("shit reaction time");
            }
        }
    }

    private void MoveArrows()
    {
        for (int i = 0; i < activeArrows.Count; i++)
        {
            activeArrows[i].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition -= new Vector2(3.0f, 0.0f);
        }
    }
}
