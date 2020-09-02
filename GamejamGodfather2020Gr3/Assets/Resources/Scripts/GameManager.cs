using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Liste des malus
    public ScriptableObject[] penaltyArray;
    //malus a trigger
    private ScriptableObject pickedPenalty;
    //Joueur qui a le malus
    public GameObject playerWithPenalty;
    //Timer entre malus
    public float timerBtwPenalty;
    private float currentTimerValue = 0;
    public float penaltyDuration;

    public Text timerText;

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

    }

    // Update is called once per frame
    void Update()
    {
        //check temps avant trigger malus
        currentTimerValue += Time.deltaTime;
        timerText.text = currentTimerValue.ToString();
        if(currentTimerValue >= timerBtwPenalty)
        {
            StartCoroutine(DoPenalty());
        }
    }

    void PickNewPenalty()
    {
        //random ?
    }

    public IEnumerator DoPenalty()
    {
        yield return new WaitForSeconds(penaltyDuration);
        currentTimerValue = 0;
    }
}
