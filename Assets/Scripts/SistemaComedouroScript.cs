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

    private int numBirdsThatPassedToday = 0;

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
        Comida();
        Saude();
        Social();
        yield return new WaitForSeconds(GameConstants.timerToCheckInSeconds);
        canCheckStatus = true;
    }

    void Comida()
    {
        GameObject[] foodFound = GameObject.FindGameObjectsWithTag("Comida");
        foreach (GameObject food in foodFound)
        {
            food.GetComponent<Food>().updateTimeOfExistence(GameConstants.timerToCheckInSeconds);
        }
        print("Check Comida");
    }

    void Saude()
    {
        GameObject[] findBirds = GameObject.FindGameObjectsWithTag("Passaro");
        numBirds = Random.Range(1, 11); // findBirds.Length;
        numBirdsThatPassedToday += numBirds;
        healthComedouro -= numBirds * GameConstants.multiplyHealthPerBirdExistent;
        healthComedouro -= numBirdsThatPassedToday * GameConstants.multiplyHealthPerBirdThatHasPass;

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
        Debug.Log(numBirds + " now and " + numBirdsThatPassedToday + " birds today");
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
            food.GetComponent<Food>().destroyFood();
        }
        foreach (GameObject bird in birdsFound)
        {
            bird.GetComponent<Bird>().destroyBird();
        }
                
        healthComedouro = GameConstants.maxHealthForComedouro;
        healthSlider.maxValue = healthComedouro;
        healthSlider.value = healthComedouro;
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
