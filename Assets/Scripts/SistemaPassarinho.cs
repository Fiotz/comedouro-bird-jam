using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SistemaPassarinho : MonoBehaviour
{
    [SerializeField, Header("Food")]
    public GameObject foodObj;

    [SerializeField, Header("SpawnPoints")]
    public GameObject spawn1;
    public GameObject spawn2;

    [SerializeField, Header("StayPoins")]
    public GameObject[] stayPoint = new GameObject[8];

    [SerializeField, Header("Text")]
    public TMP_Text text1;

    [SerializeField, Header("Bird")]
    public GameObject saira7cores;
    public GameObject saira7cores_1;
    public GameObject saira7cores_2;
    public GameObject sairaMilitar;
    public GameObject tiePreto;
    public GameObject tiePreto2;
    public GameObject sanhacoDoEncontroAzul;

    // private bool canSpawn = false;

    private int level;

    private bool hasViewOneSanhaco = false;

    // private List<int> usageArray = new List<int>();

    private bool isPlaying = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text1.SetText("8");
    }

    // Update is called once per frame
    void Update()
    {
        // if (canSpawn && isPlaying)  //check if we can spawn (coroutine handles true/false)
        // {
        //     StartCoroutine(SpawnBirdsRoutine());  //start a Coroutine
        // }
    }

    // IEnumerator SpawnBirdsRoutine()  //Coroutine
    // {
    //     canSpawn = false;    //set the bool to false - we cannot spawn!
    //     yield return new WaitForSeconds(GameConstants.timerToCheckInSeconds);  //wait for X amount of time
    // }

    private void SpawnSaira(GameObject chooseSpawn, Transform parent)
    {
        int randomSaira = Random.Range(0, 99);
        if (randomSaira < 33)
        {
            Instantiate(saira7cores, chooseSpawn.transform.position, Quaternion.identity, parent);
        }
        else if (randomSaira < 66)
        {
            Instantiate(saira7cores_1, chooseSpawn.transform.position, Quaternion.identity, parent);
        }
        else
        {
            Instantiate(saira7cores_2, chooseSpawn.transform.position, Quaternion.identity, parent);
        }
    }

    private void SpawnTiePreto(GameObject chooseSpawn, Transform parent)
    {
        int randomSaira = Random.Range(0, 99);
        if (randomSaira < 50)
        {
            Instantiate(tiePreto, chooseSpawn.transform.position, Quaternion.identity, parent);
        }
        else
        {
            Instantiate(tiePreto2, chooseSpawn.transform.position, Quaternion.identity, parent);
        }
    }

    private void SpawnBird(Transform parent) //Spawn object (Bird) function
    {
        int chanceOfBird = Random.Range(0, 100);
        GameObject chooseSpawn = Random.Range(0, 100) < 50 ? spawn1 : spawn2;
        if (chanceOfBird < GameConstants.chanceOfSairaSorte && !hasViewOneSanhaco)
        {
            Instantiate(sairaMilitar, chooseSpawn.transform.position, Quaternion.identity, parent);
            hasViewOneSanhaco = true;
        }
        else if (level <= 0)
        {
            SpawnSaira(chooseSpawn, parent);
        }
        else if (level == 1)
        {
            if (chanceOfBird < GameConstants.chanceLvlTwoBirdOne)
            {
                SpawnSaira(chooseSpawn, parent);
            }
            else
            {
                SpawnTiePreto(chooseSpawn, parent);
            }
        }
        else
        {
            if (chanceOfBird < GameConstants.chanceLvlThreeBirdOne)
            {
                SpawnSaira(chooseSpawn, parent);
            }
            else if (chanceOfBird < GameConstants.chanceLvlThreeBirdTwo)
            {
                SpawnTiePreto(chooseSpawn, parent);
            }
            else
            {
                Instantiate(sanhacoDoEncontroAzul, chooseSpawn.transform.position, Quaternion.identity, parent);
            }
        }
    }

    public void createFood()
    {
        GameObject[] spawnFood = GameObject.FindGameObjectsWithTag("SpawnComida");
        List<GameObject> spawnLivres = new List<GameObject>();
        level = GameObject.FindGameObjectWithTag("PesquisadorController").GetComponent<SistemaPesquisador>().getLevel();
        foreach (GameObject gameObject in spawnFood)
        {
            if (gameObject.transform.childCount == 0)
            {
                spawnLivres.Add(gameObject);
            }
        }

        if (spawnLivres.Count() == 0)
        {
            Debug.Log("Cannot Create more food");
        }
        else
        {
            int position = Random.Range(0, spawnLivres.Count());
            Debug.Log("Creating food in position: " + position);
            createFoodAndBird(spawnLivres[position].transform);
        }
        text1.SetText(spawnLivres.Count().ToString());
    }

    public void createFoodAndBird(Transform spawn)
    {
        Instantiate(foodObj, spawn.position, Quaternion.identity, spawn);
        SpawnBird(spawn);
    }

    public void endOfTheDay()
    {
        isPlaying = false;
    }

    public void startTheDay()
    {
        isPlaying = true;
    }

    public void pause()
    {
        isPlaying = !isPlaying;
        Debug.Log("PausePassarinho - isPlaying: " + isPlaying);
    }
}
