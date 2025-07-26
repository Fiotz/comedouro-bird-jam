using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SistemaComedouroScript : MonoBehaviour
{

    [SerializeField, Header("Sliders")]
    public Slider healthSlider;
    public Slider socialSlider;

    private bool canCheckStatus = true;
    private bool isPlaying = true;

    private float healthComedouro;

    private float socialComedouro;

    private int numBirdsThatPassedTodayWithoutCleaning = 0;

    private int numBirds;

    void Awake()
    {
        healthComedouro = GameConstants.maxHealthForComedouro;
        socialComedouro = GameConstants.maxSocialForComedouro;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.maxValue = healthComedouro;
        healthSlider.value = healthComedouro;
        socialSlider.maxValue = socialComedouro;
        socialSlider.value = socialComedouro;
    }

    // Update is called once per frame
    void Update()
    {
        if (canCheckStatus && isPlaying)
        {
            print("Check Status");
            StartCoroutine(VerifyStatusRoutine());
        }
    }

    IEnumerator VerifyStatusRoutine()
    {
        canCheckStatus = false;
        Debug.Log("VerifyFood");
        Comida();
        Debug.Log("VerifySaude");
        Saude();
        Debug.Log("VerifySocial");
        Social();
        Debug.Log("Wait");
        yield return new WaitForSeconds(GameConstants.timerToCheckInSeconds);
        canCheckStatus = true;
    }

    void Comida()
    {
        GameObject[] foodFound = GameObject.FindGameObjectsWithTag("Comida");
        foreach (GameObject food in foodFound)
        {
            food.GetComponent<Food>().updateTimeOfExistence(GameConstants.timerToCheckInSeconds);
            if (food.GetComponent<Food>().isExpired())
            {
                Destroy(food, 1);
            }
        }
        print("Check Comida");
    }

    void Saude()
    {
        GameObject[] findBirds = GameObject.FindGameObjectsWithTag("Passaro");
        numBirds = findBirds.Length;
        numBirdsThatPassedTodayWithoutCleaning += numBirds;
        healthComedouro -= numBirds * GameConstants.multiplyHealthPerBirdExistent;
        healthComedouro -= numBirdsThatPassedTodayWithoutCleaning * GameConstants.multiplyHealthPerBirdThatHasPass;

        foreach (GameObject bird in findBirds)
        {
            bird.GetComponent<Bird>().updateTimeOfExistence(GameConstants.timerToCheckInSeconds);
            if (bird.GetComponent<Bird>().isExpired())
            {
                Destroy(bird, 1);
            }
        }

        healthSlider.value = healthComedouro;
        if (healthComedouro <= 0)
        {
            print("Voce Perdeu, Passarinhos Doentes!");
            setPlaying(false);
        }
        else if (healthComedouro < 25)
        {
            print("Comedouro Sujo, passaros doentes");
        }
        else if (healthComedouro < 50)
        {
            print("Comedouro Sujo");
        }


        print("Check Saude");
        Debug.Log("SAUDE DO COMEDOURO:" + healthComedouro);
        Debug.Log(numBirds + " now and " + numBirdsThatPassedTodayWithoutCleaning + " birds today");
    }

    void Social()
    {
        if (numBirds < GameConstants.numBirdsSocialGoodMin || numBirds > GameConstants.numBirdsSocialGoodMax)
        {
            socialComedouro -= numBirds * GameConstants.decreaseSocialByNumBirds;
        }
        else
        {
            socialComedouro += numBirds * GameConstants.increaseSocialByNumBirds;
            if (socialComedouro > 100) socialComedouro = 100;
        }

        if (healthComedouro < 50)
        {
            socialComedouro -= numBirds * GameConstants.multiplySocialForBadHealth;
        }

        socialSlider.value = socialComedouro;

        if (socialComedouro <= 0)
        {
            print("Voce Perdeu, Passarinhos Não aparecem mais!");
            setPlaying(false);
        }

        print("Check Social");
        Debug.Log("SOCIAL DO COMEDOURO:" + socialComedouro);
    }

    public void setPlaying(bool play)
    {
        isPlaying = play;
    }

    public void cleanComedouro()
    {
        GameObject[] foodFound = GameObject.FindGameObjectsWithTag("Comida");
        GameObject[] birdsFound = GameObject.FindGameObjectsWithTag("Passaro");

        foreach (GameObject food in foodFound)
        {
            Destroy(food);
        }
        foreach (GameObject bird in birdsFound)
        {
            Destroy(bird);
        }

        healthComedouro = GameConstants.maxHealthForComedouro;
        healthSlider.maxValue = healthComedouro;
        healthSlider.value = healthComedouro;
        numBirdsThatPassedTodayWithoutCleaning = 0;
    }

    public void endOfTheDay()
    {
        GameObject[] foodFound = GameObject.FindGameObjectsWithTag("Comida");

        if (foodFound.Length > 0)
        {
            // Trigger Destroy Comedouro por animais a noite!
        }
        
        healthComedouro = GameConstants.maxHealthForComedouro;
        socialComedouro = GameConstants.maxSocialForComedouro;
        healthSlider.maxValue = healthComedouro;
        healthSlider.value = healthComedouro;
        socialSlider.maxValue = socialComedouro;
        socialSlider.value = socialComedouro;
    }
}
