using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f; // Velocidade do scroll (ajuste no Inspector)
    private float backgroundWidth;  // Largura do sprite

    void Start()
    {
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Move o background para a esquerda
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Se o background saiu completamente da tela, reposiciona
        if (transform.position.x < -backgroundWidth)
        {
            Vector3 newPos = new Vector3(backgroundWidth, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
