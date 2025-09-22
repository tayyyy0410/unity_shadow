using UnityEngine;
using UnityEngine.SceneManagement;

public class enterGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void startGame()
    {
        SceneManager.LoadScene("main");
    }
    
    public void quitGame()
    {
        Application.Quit();
    }
}
