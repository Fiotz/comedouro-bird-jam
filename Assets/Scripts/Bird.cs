using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField, Header("Anilha")]
    public GameObject Anilha;

    public bool isSpecialBird = false;

    public string nameOfBird = "semNome";

    private float timeOfExistenceInSeconds = 0;

    void Awake()
    {
        // Pega todos os Passaros
        GameObject[] foundBirds = GameObject.FindGameObjectsWithTag("Passaro");
        bool hasSpecialBird = false;
        nameOfBird = this.name.Split("(")[0];
        Debug.Log(nameOfBird);
        // Verificar o nome do objeto! pra ver se existe algum outro bird desse nome que tem anilha
        foreach (GameObject bird in foundBirds)
        {
            if (bird.GetComponent<Bird>().isSpecialBird && bird.name.Split("(")[0] == nameOfBird)
            {
                // VerificarNomeDoPassaro!
                // Se nao for o memso passaro entao pode ser especial
                hasSpecialBird = true;
            }
        }
        // Se nao tiver anilha 50% de ter anilha
        if (!hasSpecialBird)
        {
            int specialChance = Random.Range(0, 100);
            if (specialChance < 50)
            {
                Anilha.SetActive(true);
                isSpecialBird = true;
                Debug.Log("ESPECIAL!!!!!!!");
            }
        }
        else
        {
            Anilha.SetActive(false);
            isSpecialBird = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void updateTimeOfExistence(float passTime)
    {
        timeOfExistenceInSeconds += passTime;
    }

    public bool isExpired()
    {
        print("Add Function to expire Food if pass a threshold");
        if (timeOfExistenceInSeconds > GameConstants.foodTimeToExpireInSeconds)
        {
            return true;
        }
        return false;
    }
}
