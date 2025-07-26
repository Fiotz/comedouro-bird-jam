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

    [SerializeField, Header("BG")]
    public GameObject bgHud;

    private float hoursPassed = 0;
    private int daysPassed = 0;
    private int level = 0;

    public int saira7coresComAnilhas = 0;

    public int sairaMilitarComAnilhas = 0;

    public int tiePretoComAnilhas = 0;

    public int sanhacoComAnilhas = 0;

    private bool isPlaying = true;

    private bool canAddHour = true;

    private int registroPesquisaHoje = 0;

    public bool setGameOver = false;
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
    }

    private void checkDay()
    {
        hoursPassed += GameConstants.oneHourInSeconds;
        if (hoursPassed > GameConstants.maxTimeDaySeconds)
        {
            bgHud.GetComponent<Animator>().speed = 0;
            comedouroController.GetComponent<SistemaComedouroScript>().endOfTheDay();
            passarinhoController.GetComponent<SistemaPassarinho>().endOfTheDay();
            daysPassed++;
            Debug.Log(daysPassed);
            informacaoHud.SetActive(false);
            botoesHud.SetActive(false);
            diarioHud.SetActive(true);
        }
        else
        {
            canAddHour = true;
        }
    }

    public void registeringBirds()
    {
        if (registroPesquisaHoje <= GameConstants.numMaxResearchUsesPerDay)
        {
            registroPesquisaHoje++;
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
                                Debug.Log("PRIMEIRA AVE ENCONTRADA SAIRA");
                            }
                            saira7coresComAnilhas++;
                            break;
                        case "sairaMilitar":
                            if (sairaMilitarComAnilhas == 0)
                            {
                                Debug.Log("PRIMEIRA AVE ENCONTRADA SORTE");
                            }
                            sairaMilitarComAnilhas++;
                            break;
                        case "tie":
                            if (tiePretoComAnilhas == 0)
                            {
                                Debug.Log("PRIMEIRA AVE ENCONTRADA TIE");
                            }
                            tiePretoComAnilhas++;
                            break;
                        case "sanhaco":
                            if (sanhacoComAnilhas == 0)
                            {
                                Debug.Log("PRIMEIRA AVE ENCONTRADA SANHACO");
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
        else
        {
            Debug.Log("Max Use per Day!!!!!");
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
        if (setGameOver)
        {
            GameOver();
        }
        else
        {
            verifyLevel();
            diarioHud.SetActive(false);
            informacaoHud.SetActive(true);
            botoesHud.SetActive(true);
            comedouroController.GetComponent<SistemaComedouroScript>().startTheDay();
            passarinhoController.GetComponent<SistemaPassarinho>().startTheDay();
            hoursPassed = 0;
            bgHud.GetComponent<Animator>().speed = 1;
            canAddHour = true;
            // Reseta Numero de Limpeza e Registros
            registroPesquisaHoje = 0;
        }
    }

    public void verifyLevel() {
        if (daysPassed == GameConstants.daysForLevelOne && (saira7coresComAnilhas + sairaMilitarComAnilhas) > 3)
        {
            Debug.Log("PARABENS, Voce chegou ao LVL One");
            level++;
        }
        else if (daysPassed == GameConstants.daysForLevelTwo && (tiePretoComAnilhas + saira7coresComAnilhas + sairaMilitarComAnilhas) > 6 )
        {
            Debug.Log("PARABENS, Voce chegou ao LVL Two");
            level++;
        }
        else if (daysPassed >= GameConstants.daysForLevelThree && (tiePretoComAnilhas + saira7coresComAnilhas + sairaMilitarComAnilhas) > 9)
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
