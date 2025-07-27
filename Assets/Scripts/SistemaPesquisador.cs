using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class SistemaPesquisador : MonoBehaviour
{
    [SerializeField, Header("Globa")]
    public GameObject diaNoite;

    [SerializeField, Header("Controllers")]
    public GameObject comedouroController;
    public GameObject passarinhoController;

    [SerializeField, Header("Diario")]
    public GameObject diarioHud;
    public GameObject informacaoHud;
    public GameObject botoesHud;
    public GameObject pauseHud;

    [SerializeField, Header("Diario")]
    public GameObject popupSaira;
    public GameObject poemaSaira;
    public GameObject popupTie;
    public GameObject poemaTie;
    public GameObject popupSanhaco;
    public GameObject poemaSanhaco;
    public GameObject popupSorte;
    public GameObject poemaSorte;

    [SerializeField, Header("Alertas e GameOver")]
    public GameObject gameOverSaude;
    public GameObject gameOverSocial;
    public GameObject gameOverComedouroSujo;
    public GameObject alertaPrimeiroPassaro;
    public GameObject alertaPesquisaAtualizada;
    public GameObject alertaMudancaDeDia;
    public GameObject Tutorial;


    [SerializeField, Header("BG")]
    public GameObject bgHud;

    [SerializeField, Header("Text")]
    public TMP_Text text1;
    public TMP_Text text2;

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
    public string gameOver = "";

    private float timeElapsed = 0.0f;
    private bool dayOrNight = true;
    private bool inDiario = false;

    AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        text1.SetText(GameConstants.numMaxResearchUsesPerDay.ToString());
        text2.SetText(GameConstants.numMaxResearchUsesPerDay.ToString());
        isPlaying = !isPlaying;
        Debug.Log("Pause - isPlaying: " + isPlaying);
        comedouroController.GetComponent<SistemaComedouroScript>().pause(isPlaying);
        passarinhoController.GetComponent<SistemaPassarinho>().pause(isPlaying);
        StartCoroutine(startGame());
    }

    IEnumerator startGame()
    {
        Tutorial.SetActive(true);
        bgHud.GetComponent<Animator>().speed = 0;
        yield return new WaitForSeconds(6f);
        bgHud.GetComponent<Animator>().speed = 1;
        Tutorial.SetActive(false);
        isPlaying = !isPlaying;
        Debug.Log("Pause - isPlaying: " + isPlaying);
        comedouroController.GetComponent<SistemaComedouroScript>().pause(isPlaying);
        passarinhoController.GetComponent<SistemaPassarinho>().pause(isPlaying);
    }

    // Update is called once per frame
    void Update()
    {
        if (canAddHour && isPlaying)
        {
            StartCoroutine(PassedHour());
        }
        if (isPlaying && dayOrNight && !inDiario)
        {
            timeElapsed += Time.deltaTime;

            // Calculate the new volume using Lerp for smooth transition
            // Mathf.Clamp01 ensures the interpolation value stays between 0 and 1
            float currentVolume = Mathf.Lerp(0f, 1f, timeElapsed / (GameConstants.maxTimeDaySeconds / 2));
            // Debug.Log(currentVolume);
            diaNoite.GetComponent<Volume>().weight = 1 - currentVolume;

            // Optional: Stop updating once the target volume is reached
            if (timeElapsed >= (GameConstants.maxTimeDaySeconds / 2))
            {
                dayOrNight = false; // Disable the script to stop further updates
                timeElapsed = 0;
            }
        }
        if (isPlaying && !dayOrNight && !inDiario)
        {
            timeElapsed += Time.deltaTime;

            // Calculate the new volume using Lerp for smooth transition
            // Mathf.Clamp01 ensures the interpolation value stays between 0 and 1
            float currentVolume = Mathf.Lerp(0f, 1f, timeElapsed / (GameConstants.maxTimeDaySeconds / 2));
            // Debug.Log(currentVolume);
            diaNoite.GetComponent<Volume>().weight = currentVolume;

            // Optional: Stop updating once the target volume is reached
            if (timeElapsed >= (GameConstants.maxTimeDaySeconds / 2))
            {
                dayOrNight = true; // Disable the script to stop further updates
                timeElapsed = 0;
            }
        }
    }

    IEnumerator PassedHour()
    {
        canAddHour = false;
        yield return new WaitForSeconds(GameConstants.oneHourInSeconds);
        checkDay();
    }

    private void checkDay()
    {
        hoursPassed += GameConstants.oneHourInSeconds;
        Debug.Log(hoursPassed);
        if (hoursPassed >= GameConstants.maxTimeDaySeconds)
        {
            bgHud.GetComponent<Animator>().speed = 0;
            audioManager.changeBackgroundMusic(audioManager.backgroundNight);
            comedouroController.GetComponent<SistemaComedouroScript>().endOfTheDay();
            passarinhoController.GetComponent<SistemaPassarinho>().endOfTheDay();
            daysPassed++;
            Debug.Log(daysPassed);
            informacaoHud.SetActive(false);
            botoesHud.SetActive(false);
            diarioHud.SetActive(true);
            inDiario = true;
            timeElapsed = 0;
            diaNoite.GetComponent<Volume>().weight = 1;
            for (int i = 0; i < saira7coresComAnilhas; i++)
            {
                if (i < popupSaira.transform.childCount) popupSaira.transform.GetChild(popupSaira.transform.childCount - i - 1).gameObject.SetActive(true);
                else poemaSaira.SetActive(true);
            }
            for (int i = 0; i < tiePretoComAnilhas; i++)
            {
                if (i < popupTie.transform.childCount) popupTie.transform.GetChild(popupTie.transform.childCount - i - 1).gameObject.SetActive(true);
                else poemaTie.SetActive(true);
            }
            for (int i = 0; i < sanhacoComAnilhas; i++)
            {
                if (i < popupSanhaco.transform.childCount) popupSanhaco.transform.GetChild(popupSanhaco.transform.childCount - i - 1).gameObject.SetActive(true);
                else poemaSanhaco.SetActive(true);
            }
            for (int i = 0; i < sairaMilitarComAnilhas; i++)
            {
                if (i < popupSorte.transform.childCount) {
                    popupSorte.transform.GetChild(popupSorte.transform.childCount - i - 1).gameObject.SetActive(true);
                    poemaSorte.SetActive(true);
                }
            }
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
            text1.SetText((GameConstants.numMaxResearchUsesPerDay - registroPesquisaHoje).ToString());

            GameObject[] foundBirds = GameObject.FindGameObjectsWithTag("Passaro");
            foreach (GameObject bird in foundBirds)
            {
                Bird scriptBird = bird.GetComponent<Bird>();
                if (scriptBird.isSpecialBird)
                {
                    string typeOfBird = scriptBird.nameOfBird;
                    StartCoroutine(alertaPesquisa());
                    switch (typeOfBird)
                    {
                        case "saira7cores":
                            if (saira7coresComAnilhas == 0)
                            {
                                Debug.Log("PRIMEIRA AVE ENCONTRADA SAIRA");
                                NovoPassaro();
                            }
                            saira7coresComAnilhas++;
                            break;
                        case "sairaMilitar":
                            if (sairaMilitarComAnilhas == 0)
                            {
                                NovoPassaro();
                                Debug.Log("PRIMEIRA AVE ENCONTRADA SORTE");
                            }
                            sairaMilitarComAnilhas++;
                            break;
                        case "tie":
                            if (tiePretoComAnilhas == 0)
                            {
                                NovoPassaro();
                                Debug.Log("PRIMEIRA AVE ENCONTRADA TIE");
                            }
                            tiePretoComAnilhas++;
                            break;
                        case "sanhaco":
                            if (sanhacoComAnilhas == 0)
                            {
                                NovoPassaro();
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
    public void NovoPassaro()
    {
        StartCoroutine(newBird());
    }

    IEnumerator newBird()
    {
        alertaPrimeiroPassaro.SetActive(true);
        yield return new WaitForSeconds(1);
        alertaPrimeiroPassaro.SetActive(false);
    }

    IEnumerator alertaPesquisa()
    {
        alertaPesquisaAtualizada.SetActive(true);
        yield return new WaitForSeconds(1);
        alertaPesquisaAtualizada.SetActive(false);
    }

    IEnumerator alertaDeMudancaDia()
    {
        alertaMudancaDeDia.SetActive(true);
        yield return new WaitForSeconds(2);
        alertaMudancaDeDia.SetActive(false);
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
            audioManager.changeBackgroundMusic(audioManager.backgroundDay);
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
            text1.SetText(GameConstants.numMaxResearchUsesPerDay.ToString());
            Debug.Log("LEVEL ATUA: " + level + "\n" +
            "Saira 7 cores: " + saira7coresComAnilhas + "\n" +
            "SairaSorte: " + sairaMilitarComAnilhas + "\n" +
            "TIE: " + tiePretoComAnilhas + "\n" +
            "Sanhaco: " + sanhacoComAnilhas + "\n" +
            "DIAS: " + daysPassed + "\n" +
            "");
            inDiario = false;
            StartCoroutine(alertaDeMudancaDia());
        }
    }

    public void verifyLevel() {
        if (daysPassed == GameConstants.daysForLevelOne && (saira7coresComAnilhas + sairaMilitarComAnilhas) > 3)
        {
            Debug.Log("PARABENS, Voce chegou ao LVL One");
            level = 1;
        }
        else if (daysPassed == GameConstants.daysForLevelTwo && (tiePretoComAnilhas + saira7coresComAnilhas + sairaMilitarComAnilhas) > 6)
        {
            Debug.Log("PARABENS, Voce chegou ao LVL Two");
            level = 2;
        }
        else if (daysPassed >= GameConstants.daysForLevelThree && (tiePretoComAnilhas + saira7coresComAnilhas + sairaMilitarComAnilhas) > 9)
        {
            Debug.Log("PARABENS, Voce chegou ao LVL Three");
            level = 3;
        }
    }


    public void pause()
    {
        bgHud.GetComponent<Animator>().speed = 0;
        isPlaying = false;
        Debug.Log("Pause - isPlaying: " + isPlaying);
        comedouroController.GetComponent<SistemaComedouroScript>().pause(isPlaying);
        passarinhoController.GetComponent<SistemaPassarinho>().pause(isPlaying);
        pauseHud.SetActive(true);
    }

    public void continueJogo(){
        isPlaying = true;
        Debug.Log("Pause - isPlaying: " + isPlaying);
        comedouroController.GetComponent<SistemaComedouroScript>().pause(isPlaying);
        passarinhoController.GetComponent<SistemaPassarinho>().pause(isPlaying);
        pauseHud.SetActive(false);
        bgHud.GetComponent<Animator>().speed = 1;
    }

    public void defineGameOver(string over)
    {
        gameOver = over;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        switch (gameOver)
        {
            case "social":
                gameOverSocial.SetActive(true);
                break;
            case "noturno":
                gameOverComedouroSujo.SetActive(true);
                break;
            case "saude":
                gameOverSaude.SetActive(true);
                break;
            default:
                break;
        }
    }
}
