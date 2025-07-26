using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // [SerializeField, Header("ObjParentsBirds")]
    // public GameObject allBirds;

    [SerializeField, Header("Bird")]
    public GameObject saira7cores;
    public GameObject sairaMilitar;
    public GameObject tiePreto;
    public GameObject sanhacoDoEncontroAzul;


    private int foodNum = 0;

    private bool canSpawn = false;

    private int level;

    private bool hasViewOneSanhaco = false;

    private List<int> usageArray = new List<int>();

    private bool isPlaying = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && isPlaying)  //check if we can spawn (coroutine handles true/false)
        {
            StartCoroutine(SpawnBirdsRoutine());  //start a Coroutine
        }
    }

    IEnumerator SpawnBirdsRoutine()  //Coroutine
    {
        SpawnBird();  //spawn enemy
        canSpawn = false;    //set the bool to false - we cannot spawn!
        yield return new WaitForSeconds(GameConstants.timerToCheckInSeconds);  //wait for X amount of time
    }

    private void SpawnBird() //Spawn object (Bird) function
    {
        if (isFoodEnougth())
        {
            int chanceOfBird = Random.Range(0, 100);
            GameObject chooseSpawn = Random.Range(0, 100) < 50 ? spawn1 : spawn2;
            if (chanceOfBird < GameConstants.chanceOfSanhaco && !hasViewOneSanhaco)
            {
                Instantiate(sanhacoDoEncontroAzul, chooseSpawn.transform.position, Quaternion.identity);
                hasViewOneSanhaco = true;
            }
            else if (level == 1)
            {
                Instantiate(saira7cores, chooseSpawn.transform.position, Quaternion.identity);
            }
            else if (level == 2)
            {
                if (chanceOfBird < GameConstants.chanceLvlTwoBirdOne)
                {
                    Instantiate(saira7cores, chooseSpawn.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(sairaMilitar, chooseSpawn.transform.position, Quaternion.identity);
                }
            }
            else
            {
                if (chanceOfBird < GameConstants.chanceLvlThreeBirdOne)
                {
                    Instantiate(saira7cores, chooseSpawn.transform.position, Quaternion.identity);
                }
                else if (chanceOfBird < GameConstants.chanceLvlThreeBirdTwo)
                {
                    Instantiate(sairaMilitar, chooseSpawn.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(sanhacoDoEncontroAzul, chooseSpawn.transform.position, Quaternion.identity);
                }
            }
        }
    }

    private bool isFoodEnougth()
    {
        // math to see if we have food for new birds
        return true;
    }

    public void createFood()
    {
        GameObject[] spawnFood = GameObject.FindGameObjectsWithTag("SpawnComida");
        List<GameObject> spawnLivres = new List<GameObject>();
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
            Instantiate(foodObj, spawnLivres[position].transform.position, Quaternion.identity, spawnLivres[position].transform);
            Instantiate(saira7cores, spawnLivres[position].transform.position, Quaternion.identity, spawnLivres[position].transform);
        }
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
    }
}
