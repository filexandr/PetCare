using UnityEngine;
using UnityEngine.UI;

public class LifeParameterController : MonoBehaviour
{
    public Text TimerTextObject;
    public GameObject ScaleObject;
    public Button UserButtonObject;
    public int InitialTimerValue;
    public int InitialParameterValue = 100;
    public int MinParameterValue = 0;
    public int MaxParameterValue = 100;
    public int ParameterChangeByTimer = -20;
    public int ParameterChangeByUser = 20;

    private int currentParameterValue;
    private float currentTimerValue;
    private bool isDead;

    public int ParameterValue
    {
        get
        {
            return currentParameterValue;
        }
        set
        {
            currentParameterValue = value;
            checkParameterBoundaries();
            currentTimerValue = 0;
            startTimerIfNeeded();
            updateUI();
        }
    }

    // Use this for initialization
    void Start()
    {
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimerValue == 0 || isDead)
        {
            return;
        }

        currentTimerValue -= Time.deltaTime;
        if (currentTimerValue <= 0)
        {
            currentTimerValue = 0;
            TimerChange();
        }

        updateUI();
    }

    public void NewGame()
    {
        isDead = false;
        ParameterValue = InitialParameterValue;
    }

    public void Die()
    {
        isDead = true;
        updateUI();
    }

    public void UserChange()
    {
        ParameterValue += ParameterChangeByUser;
    }

    public void TimerChange()
    {
        ParameterValue += ParameterChangeByTimer;
    }

    private void startTimerIfNeeded()
    {
        if (currentParameterValue > MinParameterValue && currentTimerValue == 0)
        {
            currentTimerValue = InitialTimerValue;
        }
    }

    private void checkParameterBoundaries()
    {
        if (currentParameterValue < MinParameterValue)
        {
            currentParameterValue = MinParameterValue;
        }

        if (currentParameterValue > MaxParameterValue)
        {
            currentParameterValue = MaxParameterValue;
        }
    }

    private void updateUI()
    {
        TimerTextObject.text = string.Format("{0}:{1:00}", (int)(currentTimerValue / 60), currentTimerValue % 60);
        Utils.SetProgressBarValue(ScaleObject, currentParameterValue, MaxParameterValue);
        UserButtonObject.interactable = (currentParameterValue < MaxParameterValue) && !isDead;
    }
}