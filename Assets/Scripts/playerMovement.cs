using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class playerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    private bool flipByInput = true;
    public SpriteRenderer spriteToFlip;
    

    [Header("Flashlight")] 
    public Transform flashlight;
    public Light2D light2d;
    public KeyCode flashlightKey = KeyCode.F;
    public CircleCollider2D lightCollider;
    public bool startOn = true;
    //light radius
    public float fixedOuterRadius = 15f;
    
    [Header("pitch degree")]
    public float minPitchDeg = -30f;
    public float maxPitchDeg= 30f;
    public float pitchSpeed = 180f;
    

    private Rigidbody2D rb;
    private bool lightOn;
    private float pitchDeg = 0f; 
    private int facing = 1; //1 = right, -1 = left
    
    
    
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setFlashlight(startOn);  //start the game with flashlight on
        
        //fixed light radius
        if (light2d && lightCollider)
        {
            light2d.pointLightOuterRadius = fixedOuterRadius;
            lightCollider.radius = light2d.pointLightOuterRadius;
        }

    }
    
    void Update()
    {
        //player moving left and right
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveHorizontal * moveSpeed, rb.linearVelocity.y);
        
        //player turning
        if (flipByInput && Mathf.Abs(moveHorizontal) > 0.01f)
        {
            facing = moveHorizontal > 0 ? 1 : -1; //to determine if player is moving right or left
            spriteToFlip.flipX = facing == -1;
        }
        
        // turn on and off flashlight
        if (Input.GetKeyDown(flashlightKey))
        {
            setFlashlight((!lightOn));
        }
        
        //control angle of flashlight
        float pitchInput = Input.mouseScrollDelta.y; //mouse scroll up = pitch up, mouse scroll down = pitch down
        if (Mathf.Abs(pitchInput) > 0.01f)
        {
            pitchDeg += pitchInput * pitchSpeed * Time.deltaTime;
            pitchDeg = Mathf.Clamp(pitchDeg, minPitchDeg, maxPitchDeg);
        }
        
        //flashlight facing
        const float facingOffset = -75f; 
        float baseA = (facing == 1) ? 0f : 180f;  //if facing right, baseYaw = 0, if facing left, baseYaw = 180
        float finalA = baseA+ facingOffset + (facing ==1 ? pitchDeg : -pitchDeg); //mirror the angle if facing left
        flashlight.rotation = Quaternion.Euler(0f, 0f, finalA);
        
    }
    //flashlight on/off
    public void setFlashlight(bool on)
    {
        lightOn = on;
        if (flashlight)
        {
            flashlight.gameObject.SetActive(on);
        }

        if (lightCollider)
        {
            lightCollider.enabled = on;
        }
    }
}
