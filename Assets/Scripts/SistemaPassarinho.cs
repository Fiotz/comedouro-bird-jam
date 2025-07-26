using System.Collections;
using UnityEngine;

public class SistemaPassarinho : MonoBehaviour
{

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


    private int food = 0;

    private int birds = 0;

    private bool canSpawn = false;

    private int level;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level = 0;
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
            if (level == 1)
            {
                Instantiate(saira7cores, transform.position, Quaternion.identity);
            }
            else if (level == 2)
            {
                if (chanceOfBird < GameConstants.chanceLvlTwoBirdOne)
                {

                }
                else
                {
                    
                }
             }
            else
            {

            }
        }
    }

    private bool isFoodEnougth()
    {
        // math to see if we have food for new birds
        return false;
    }

    public void createFood()
    {
        
    }
}
