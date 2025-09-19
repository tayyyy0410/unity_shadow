using UnityEngine;

[RequireComponent(typeof(Camera))]
public class mainCamera : MonoBehaviour
{
    [Header("Targets")]
    public Transform player;                 
    public SpriteRenderer backgroundSprite;  

    [Header("Motion")]
    public float smooth = 5f;                

    private Camera cam;
    private float halfHeight;               
    private float halfWidth;                

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
    }

    void Start()
    {
        FitHeightToBackground();
        UpdateViewportHalfExtents();
    }

    void LateUpdate()
    {
        if (player == null || backgroundSprite == null)
        {
            return;
        }

        UpdateViewportHalfExtents();

      
        Bounds bg = backgroundSprite.bounds;
        
        float targetY = bg.center.y;
        
        float minX = bg.min.x + halfWidth;
        float maxX = bg.max.x - halfWidth;

        float targetX;
        if (bg.size.x <= halfWidth * 2f)
        {
    
            targetX = bg.center.x;
        }
        else
        {
            targetX = Mathf.Clamp(player.position.x, minX, maxX);
        }

        Vector3 desired = new Vector3(targetX, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desired, smooth * Time.deltaTime);
    }

   
    private void FitHeightToBackground()
    {
        if (backgroundSprite == null) return;
        float bgHeight = backgroundSprite.bounds.size.y; 
        cam.orthographicSize = bgHeight * 0.5f;          
    }

 
    private void UpdateViewportHalfExtents()
    {
        halfHeight = cam.orthographicSize;
        halfWidth  = halfHeight * cam.aspect;
    }
}
