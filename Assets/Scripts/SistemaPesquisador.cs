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

    private int sairaMilitar = 0;

    private int tiePreto = 0;

    private int sanhaco = 0;

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

    public void pause()
    {
        comedouroController.GetComponent<SistemaComedouroScript>().pause();
        passarinhoController.GetComponent<SistemaPassarinho>().pause();
        isPlaying = !isPlaying;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
