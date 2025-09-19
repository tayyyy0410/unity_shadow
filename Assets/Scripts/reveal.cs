using UnityEngine;

public class reveal: MonoBehaviour
{
    [Header("fade")] 
    public float revealSpeed = 1.6f;
    public float fadeSpeed = 0.5f;  //word's fade speed
    
    SpriteRenderer sr;
    private float targetAlpha = 0f;
    
    
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        GetComponent<BoxCollider2D>().isTrigger = true;
        Color c = sr.color; c.a = 0f; sr.color = c; //set alpha to 0 at start
        


    }

    // Update is called once per frame
    void Update()
    {
        Color c = sr.color;
        float speed = (targetAlpha > c.a) ? revealSpeed : fadeSpeed;
        c.a = Mathf.MoveTowards(c.a, targetAlpha, Time.deltaTime * speed);
        sr.color = c;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("flashlight"))
        {
            targetAlpha = 1f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("flashlight"))
        {
            targetAlpha = 0f;
        }
    }
}
