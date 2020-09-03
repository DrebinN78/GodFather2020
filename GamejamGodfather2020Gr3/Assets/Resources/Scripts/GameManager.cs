using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Liste des malus
    public ScriptablePenalty[] penaltyArray;
    //malus a trigger
    [Header("for debug only")]
    [SerializeField]private ScriptablePenalty pickedPenalty;
    //Joueur qui a le malus
    public GameObject playerWithPenalty;
    //Timer entre malus
    public float timerBtwPenalty;
    private float currentTimerValue = 0;
    private bool readyToPunish = true;

    private List<GameObject> allPlayers;
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
        PickNewRandomPlayer();
        PickNewPenalty();
    }

    // Update is called once per frame
    void Update()
    {
        //check temps avant trigger malus
        currentTimerValue += Time.deltaTime;
        if(currentTimerValue >= timerBtwPenalty)
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
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (allPlayers[i].GetComponent<PlayerController>().IsAlive())
                playerRemaining++;
        }
        if (playerRemaining <= 1 && allPlayers.Count > 1) // >1 player
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //restart
        }
        else if (playerRemaining == 0 && allPlayers.Count == 1) // 1 player
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //restart
        }

    }

    public void RemoveFromPlayer(GameObject pc)
    {
        if(allPlayers.Contains(pc))
        {
            allPlayers.Remove(pc);
        }
    }

    public void PickNewPenalty()
    {
        pickedPenalty = penaltyArray[Random.Range(0, penaltyArray.Length)];
    }

    public void PickNewRandomPlayer()
    {
        allPlayers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        playerWithPenalty = allPlayers[Random.Range(0, allPlayers.Count)];
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
