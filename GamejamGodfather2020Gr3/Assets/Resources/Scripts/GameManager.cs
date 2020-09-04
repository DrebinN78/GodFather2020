using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //List of all penalties
    public ScriptablePenalty[] penaltyArray;
    //Penalty that will be triggered
    private ScriptablePenalty pickedPenalty;
    //Player that the penalty will target
    private GameObject playerWithPenalty;
    //Winner of the last round
    private GameObject bestPlayer = null;
    //Timer between penalties
    public float timerBtwPenalty;
    private float currentTimerValue = 0;
    private bool readyToPunish = true;

    private List<GameObject> allPlayers;
    private List<GameObject> remainingPlayers;

    public static GameManager instance = null;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetAllPlayers(); //Getting all player in the scene
        remainingPlayers = allPlayers;
        PickNewRandomPlayer(); //choosing a player to curse
        PickNewPenalty(); //choosing the curse
    }

    // Update is called once per frame
    void Update()
    {
        //check temps avant trigger malus
        currentTimerValue += Time.deltaTime;
        if (currentTimerValue >= timerBtwPenalty)
        {
            pickedPenalty.SetPlayer(playerWithPenalty);
            if (readyToPunish)
            {
                readyToPunish = false;
                DoPenalty();
            }
        }
        CheckForWinner();
    }

    void CheckForWinner()
    {
        int playerRemaining = 0;
        for (int i = 0; i < remainingPlayers.Count; i++)
        {
            PlayerController temppc = remainingPlayers[i].GetComponent<PlayerController>();
            if (temppc != null && temppc.IsAlive())
            {
                playerRemaining++;
                temppc = null;
            }
        }
        if (playerRemaining <= 1 && allPlayers.Count > 1) // >1 player
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GetAllPlayers();
            remainingPlayers = allPlayers;
            PickNewRandomPlayer();
            ResetTimer();
        }
        else if (playerRemaining == 0 && allPlayers.Count == 1) // 1 player
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GetAllPlayers();
            remainingPlayers = allPlayers;
            PickNewRandomPlayer();
            ResetTimer();
        }

    }

    public void RemoveFromTheLiving(GameObject pc)
    {
        if (remainingPlayers.Contains(pc))
        {
            remainingPlayers.Remove(pc);
        }
    }

    public void PickNewPenalty()
    {
        pickedPenalty = penaltyArray[Random.Range(0, penaltyArray.Length)];
    }

    public void GetAllPlayers()
    {
        allPlayers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }

    public GameObject WhoIsCursed()
    {
        return playerWithPenalty;
    }

    public void IGuessImCursed(GameObject me)
    {
        playerWithPenalty = me;
    }

    public void PickNewRandomPlayer()
    {
        //for (int i = 0; i < remainingPlayers.Count; i++)
        //{
        //    if(!remainingPlayers[i].GetComponent<PlayerController>().IsAlive())
        //    {
        //        RemoveFromPlayer(remainingPlayers[i]);
        //    }
        //}
        if (remainingPlayers.Count > 0)
        {
            if (bestPlayer == null)
            {
                playerWithPenalty = remainingPlayers[Random.Range(0, remainingPlayers.Count)];
            }
            else
            {
                playerWithPenalty = bestPlayer;
            }
        }
    }

    public void ResetTimer()
    {
        PickNewPenalty();
        currentTimerValue = 0;
        readyToPunish = true;
    }

    public bool IsReadyToPunish()
    {
        return readyToPunish;
    }

    public void DoPenalty()
    {
        switch (pickedPenalty.effectType)
        {
            case effect.Jump:
                StartCoroutine(pickedPenalty.Jump());
                break;
            case effect.Speed:
                StartCoroutine(pickedPenalty.Speed());
                break;
            case effect.Invisibility:
                StartCoroutine(pickedPenalty.Invisibility());
                break;
            case effect.Weight:
                //get player rigidbody
                //create temp var that contain player initial rigidbody value
                //verify collision between player
                //get the position of the player colliding
                //add force in the opposite direction (same force as a dash or not ?)
                //set player's rigidbody weight to something else
                break;
            case effect.Autojump:
                StartCoroutine(pickedPenalty.AutoJump());
                break;
            case effect.Gravity:
                StartCoroutine(pickedPenalty.Gravity());
                break;
            case effect.Size:
                StartCoroutine(pickedPenalty.Size());
                break;
            default:
                Debug.Log("No penalty was picked !");
                break;
        }
    }
}
