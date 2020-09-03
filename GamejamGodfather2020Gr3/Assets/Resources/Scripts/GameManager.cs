using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text timerText;

    private Player player;
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
            timerText.text = timerBtwPenalty.ToString();
            pickedPenalty.SetPlayer(playerWithPenalty);
            if (readyToPunish)
            {
                readyToPunish = false;
                DoPenalty();
            }
        }
        else
        {
            timerText.text = currentTimerValue.ToString();
        }
    }

    void PickNewPenalty()
    {
        pickedPenalty = penaltyArray[Random.Range(0, penaltyArray.Length)];
    }

    void PickNewRandomPlayer()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        playerWithPenalty = allPlayers[Random.Range(0, allPlayers.Length)];
    }

    public void ResetTimer()
    {
        currentTimerValue = 0;
        readyToPunish = true;
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
