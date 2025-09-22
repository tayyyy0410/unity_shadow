using UnityEngine;

public class backgroundMusic : MonoBehaviour
{
    public static backgroundMusic instance;
    private AudioSource audioSource;
    public AudioClip music;
    public float volume = 0.8f;
    
    
    
    void Start()
    {
        
    
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = music ;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
    }


    void Update()
    {
        
    }
}
