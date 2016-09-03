using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject PetHappyObject;
    public GameObject PetSadObject;
    public GameObject PetDeadObject;
    public GameObject HappinessScaleObject;
    public Button NewGameButton;

    private LifeParameterController[] lifeParameters;
    private int happiness;

    // Use this for initialization
    void Start()
    {
        lifeParameters = GameObject.FindObjectsOfType<LifeParameterController>();
    }

    // Update is called once per frame
    void Update()
    {
        calculateHappiness();
        updateUI();
    }

    public void NewGame()
    {
        foreach (var lifeParameter in lifeParameters)
        {
            lifeParameter.NewGame();
        }
    }

    public void Die()
    {
        foreach (var lifeParameter in lifeParameters)
        {
            lifeParameter.Die();
        }
    }

    private void calculateHappiness()
    {
        int totalValue = 0;
        foreach (var lifeParameter in lifeParameters)
        {
            totalValue += lifeParameter.ParameterValue;
        }

        happiness = totalValue / lifeParameters.Length;

        if (happiness == 0)
        {
            Die();
        }
    }

    private void updateUI()
    {
        int totalMaxValue = 0;
        foreach (var lifeParameter in lifeParameters)
        {
            totalMaxValue += lifeParameter.MaxParameterValue;
        }

        var happinessMax = totalMaxValue/lifeParameters.Length;
        Utils.SetProgressBarValue(HappinessScaleObject, happiness, happinessMax);


        PetHappyObject.SetActive(false);
        PetSadObject.SetActive(false);
        PetDeadObject.SetActive(false);

        if (happiness == 0)
        {
            PetDeadObject.SetActive(true);
        }
        else if (happiness < 51)
        {
            PetSadObject.SetActive(true);
        }
        else
        {
            PetHappyObject.SetActive(true);
        }
    }
}