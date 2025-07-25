using UnityEngine;

public class Food : MonoBehaviour
{
    private float timeOfExistenceInSeconds = 0;

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
        checkExpiredFood();
    }

    private void checkExpiredFood()
    {
        print("Add Function to expire Food if pass a threshold");
    }

    public void destroyFood()
    {
        Destroy(this);
    }
}
