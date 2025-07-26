using System.Collections;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)  //check if we can spawn (coroutine handles true/false)
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
        Instantiate(foodObj, stayPoint[stayPoint.Length - 1].transform.position, Quaternion.identity);
        Instantiate(saira7cores, stayPoint[stayPoint.Length - 1].transform.position, Quaternion.identity);
    }
}
