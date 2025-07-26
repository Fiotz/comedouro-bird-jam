using System;
using System.Collections;
using UnityEngine;

public class SistemaPesquisador : MonoBehaviour
{
    [SerializeField, Header("Controllers")]
    public GameObject comedouroController;
    public GameObject passarinhoController;

    [SerializeField, Header("Diario")]
    public GameObject diarioHud;
    public GameObject informacaoHud;
    public GameObject botoesHud;
    public GameObject pauseHud;

    private float hoursPassed = 0;
    private int daysPassed = 0;
    private int level = 0;

    private int saira7coresComAnilhas = 0;

    private int sairaMilitarComAnilhas = 0;

    private int tiePretoComAnilhas = 0;

    private int sanhacoComAnilhas = 0;

    private bool isPlaying = true;

    private bool canAddHour = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canAddHour && isPlaying)
        {
            StartCoroutine(PassedHour());
        }
    }

    IEnumerator PassedHour()
    {
        canAddHour = false;
        yield return new WaitForSeconds(1);
        checkDay();
        canAddHour = true;
    }

    private void checkDay()
    {
        hoursPassed += GameConstants.oneHourInSeconds;
        if (hoursPassed > GameConstants.maxTimeDaySeconds)
        {
            comedouroController.GetComponent<SistemaComedouroScript>().endOfTheDay();
            passarinhoController.GetComponent<SistemaPassarinho>().endOfTheDay();
            daysPassed++;
            informacaoHud.SetActive(false);
            botoesHud.SetActive(false);
            diarioHud.SetActive(true);
        }
    }

    public void registeringBirds()
    {
        GameObject[] foundBirds = GameObject.FindGameObjectsWithTag("Passaro");
        foreach (GameObject bird in foundBirds)
        {
            Bird scriptBird = bird.GetComponent<Bird>();
            if (scriptBird.isSpecialBird)
            {
                string typeOfBird = scriptBird.nameOfBird;
                // if ()
                switch (typeOfBird)
                {
                    case "saira7cores":
                        if (saira7coresComAnilhas == 0)
                        {
                            Debug.Log("PRIMEIRA AVE ENCONTRADA");
                        }
                        saira7coresComAnilhas++;
                        break;
                    case "sairaMilitar":
                        if (sairaMilitarComAnilhas == 0)
                        {
                            Debug.Log("PRIMEIRA AVE ENCONTRADA");
                        }
                        sairaMilitarComAnilhas++;
                        break;
                    case "tie":
                        if (tiePretoComAnilhas == 0)
                        {
                            Debug.Log("PRIMEIRA AVE ENCONTRADA");
                        }
                        tiePretoComAnilhas++;
                        break;
                    case "sanhaco":
                        if (sanhacoComAnilhas == 0)
                        {
                            Debug.Log("PRIMEIRA AVE ENCONTRADA");
                        }
                        sanhacoComAnilhas++;
                        break;
                    default:
                        Debug.Log("Nao foi encontrado nenhum passaro com esse nome. VERIFICAR!!!!");
                        break;
                }
            }
        }
    }

    public int getDaysPassed()
    {
        return daysPassed;
    }

    public int getLevel()
    {
        return level;
    }

    public void toNextDay()
    {
        verifyLevel();
        diarioHud.SetActive(false);
        informacaoHud.SetActive(true);
        botoesHud.SetActive(true);
        comedouroController.GetComponent<SistemaComedouroScript>().startTheDay();
        passarinhoController.GetComponent<SistemaPassarinho>().startTheDay();
    }

    public void verifyLevel() {
        if (daysPassed >= GameConstants.daysForLevelOne && daysPassed < GameConstants.daysForLevelTwo)
        {
            Debug.Log("PARABENS, Voce chegou ao LVL One");
            level++;
        }
        else if (daysPassed >= GameConstants.daysForLevelTwo && daysPassed < GameConstants.daysForLevelThree)
        {
            Debug.Log("PARABENS, Voce chegou ao LVL Two");
            level++;
        }
        else if (daysPassed >= GameConstants.daysForLevelThree)
        {
            Debug.Log("PARABENS, Voce chegou ao LVL Three");
            level++;
        }
    }


    public void pause()
    {
        isPlaying = !isPlaying;
        Debug.Log("Pause - isPlaying: " + isPlaying);
        comedouroController.GetComponent<SistemaComedouroScript>().pause();
        passarinhoController.GetComponent<SistemaPassarinho>().pause();
        pauseHud.SetActive(true);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
