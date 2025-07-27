using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField, Header("Anilha")]
    public GameObject Anilha;
    public GameObject AnilhaFilha;
    public GameObject reacaoObj;

    public Sprite feliz;
    public Sprite fome;
    public Sprite nojo;
    public Sprite briga;

    public bool isSpecialBird = false;

    public string nameOfBird = "semNome";

    private float timeOfExistenceInSeconds = 0;

    public bool canMove = true;

    private float speed = 10.0f;

    private bool isShowingReacao = false;

    private Vector3 posInit;

    public bool movingToParent = true;

    AudioManager audioManager;

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
        GetComponent<Animator>().Play("voando");
        if (transform.position.x > transform.parent.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            Anilha.GetComponent<SpriteRenderer>().flipX = true;
            AnilhaFilha.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void flipBird()
    {
        GetComponent<Animator>().Play("voando");
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        Anilha.GetComponent<SpriteRenderer>().flipX = !Anilha.GetComponent<SpriteRenderer>().flipX;
        AnilhaFilha.GetComponent<SpriteRenderer>().flipX = !AnilhaFilha.GetComponent<SpriteRenderer>().flipX;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posInit = transform.position;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        switch (nameOfBird)
        {
            case "saira7cores":
                audioManager.playSFX(audioManager.setecores);
                break;
            case "sairaMilitar":
                audioManager.playSFX(audioManager.sorte);
                break;
            case "tie":
                audioManager.playSFX(audioManager.tiepreto);
                break;
            case "sanhaco":
                audioManager.playSFX(audioManager.sanhaco);
                break;
            default:
                Debug.Log("Nao foi encontrado nenhum passaro com esse nome. VERIFICAR!!!!");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveToParent();
        }

        if (isShowingReacao)
        {
            StartCoroutine(RemoveReacao());
        }
    }

    IEnumerator RemoveReacao()
    {
        yield return new WaitForSeconds(1f);
        reacaoObj.SetActive(false);
        isShowingReacao = false;
    }

    public void MoveToParent()
    {
        Vector3 targetPosition = transform.parent.position;
        if (movingToParent)
        {
            // Move the child towards the parent's position
            // Use Vector3.MoveTowards for consistent linear speed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Optional: You might want to stop moving when very close to avoid jittering
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                canMove = false;
                GetComponent<Animator>().Play("sa[ira");
                
                // Optionally snap to the parent's position or stop movement
                // transform.position = targetPosition;
                // enabled = false; // Disable the script
            }
        }
        else
        {
            // Move the child towards the parent's position
            // Use Vector3.MoveTowards for consistent linear speed
            transform.position = Vector3.MoveTowards(transform.position, posInit, speed * Time.deltaTime);

            // Optional: You might want to stop moving when very close to avoid jittering
            if (Vector3.Distance(transform.position, posInit) < 0.01f)
            {
                canMove = false;
                Destroy(gameObject);
                // Optionally snap to the parent's position or stop movement
                // transform.position = targetPosition;
                // enabled = false; // Disable the script
            }
        }
    }
    
    public void updateTimeOfExistence(float passTime)
    {
        timeOfExistenceInSeconds += passTime;
    }

    public void showReacao(int reacao)
    {
        switch (reacao)
        {
            case 1:
                if (!canMove)
                {
                    reacaoObj.GetComponent<SpriteRenderer>().sprite = feliz;
                }
                if (canMove)
                {
                    reacaoObj.GetComponent<SpriteRenderer>().sprite = fome;
                }
                break;
            case 2:
                reacaoObj.GetComponent<SpriteRenderer>().sprite = nojo;
                break;
            case 3:
                reacaoObj.GetComponent<SpriteRenderer>().sprite = briga;
                break;
            default:
                Debug.Log("SEM EMOCAO REGISTRADA");
                break;
        }
        reacaoObj.SetActive(true);
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
