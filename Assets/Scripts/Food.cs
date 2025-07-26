using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField, Header("Foods")]
    public Sprite food1;
    public Sprite food2;
    public Sprite food3;
    public Sprite food4;

    private float timeOfExistenceInSeconds = 0;

    private int position = -1;

    void Awake()
    {
        // Adicionar random da lista de foods 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int chance = Random.Range(0, 100);
        if (chance < 25)
        {
            this.GetComponent<SpriteRenderer>().sprite = food1;
        }
        else if (chance < 50)
        {
            this.GetComponent<SpriteRenderer>().sprite = food2;
        }
        else if (chance < 75)
        {
            this.GetComponent<SpriteRenderer>().sprite = food3;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = food4;
        }
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

    public void setPosition(int pos)
    {
        position = pos;
    }

    public int getPosition()
    {
        return position;
    }
}
