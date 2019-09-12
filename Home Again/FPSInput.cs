using UnityEngine;
using System.Collections;

// basic WASD-style movement control
// commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{

    [System.NonSerialized] public Vector3 movement;
    [System.NonSerialized] public bool isCrouching;
    [System.NonSerialized] public bool lightStatus;
    [System.NonSerialized] public bool isWalkingBackwards;
    private CharacterController _charController;
    private AudioSource[] audioSources;
    private AudioSource runningSound;
    private PlayerCharacter player;

    //ambient light values
    private byte r;
    private byte g;
    private byte b;

    //fog values
    private float fogDensity;

    public float speed = 3.0f;
    public float gravity = -9.8f;

    //Camera[] _cameras;    //i need to keep track of the player's health

    private Light[] lightSources;

    void Start()
    {
        lightStatus = true;
        player = GetComponentInParent<PlayerCharacter>();
        audioSources = GetComponents<AudioSource>();
        runningSound = audioSources[4];
        lightSources = GetComponentsInChildren<Light>();
        _charController = GetComponent<CharacterController>();
        isCrouching = false;
        isWalkingBackwards = false;
        fogDensity = 0.025f;
        r = 0;
        g = 0;
        b = 0;

    }


    void Update()
    {


        //this looks at the parent's playerCharacter and checks its health which allows me to disable movement when they die
        if (player.isAlive)
        {
            transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;
            movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);

            movement.y = gravity;

            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _charController.Move(movement);

            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
            {
                speed = 2.0f;
                isWalkingBackwards = true;
            }
            else
            {
                isWalkingBackwards = false;
            }

            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S))
            {
                if (!runningSound.isPlaying)
                    runningSound.Play();

                speed = 7.0f;
            }
            else
            {
                if (runningSound.isPlaying)
                    runningSound.Stop();
                speed = 3.0f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                lightStatus = !lightStatus;
                foreach (Light l in lightSources)
                {
                    if (l.name != "MuzzleFlash")
                        l.enabled = !l.enabled;
                }
                if (!lightSources[0].enabled)
                    StartCoroutine(AdjustEyesToDark());
                else
                    StartCoroutine(AdjustEyesToLight());
            }

            //make sure they dont cheat by pressing run and croutch at the same time
            if ((!Input.GetKeyDown(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.LeftControl)))
            {
                isCrouching = true;
                _charController.height = 0.8f;
                speed = 2.0f;
            }
            else
            {
                isCrouching = false;
                _charController.height = 2.1f;
            }
        }
        else
        {
            speed = 0;
            isCrouching = false;
            isWalkingBackwards = false;
            if (runningSound.isPlaying)
                runningSound.Stop();
        }
    }

    IEnumerator AdjustEyesToDark()
    {
        while (r <= 19 || g <= 28 || b <= 72 || fogDensity > 0.012f)
        {
            if (r <= 19)
                r++;
            if (g <= 28)
                g++;
            if (b <= 72)
                b++;
            if (fogDensity > 0.012f) 
                fogDensity -= 0.001f;
            

            //if user turns light back on, then it will stop adjusting eyes to darkness
            if (lightStatus)
                yield break;

            RenderSettings.fogDensity = fogDensity;
            RenderSettings.ambientLight = new Color32(r, g, b, 0);

            yield return new WaitForSeconds(0.1f);
        }

    }

    IEnumerator AdjustEyesToLight()
    {

        while (r > 0 || g > 0 || b > 0 || fogDensity < 0.025f)
        {
            if (r > 0)
                r--;
            if (g > 0)
                g--;
            if (b > 0)
                b--;
            if (fogDensity < 0.025f) 
                fogDensity += 0.001f;

            //if user turns light off, then it will stop adjusting eyes to the light
            if (!lightStatus)
                yield break;

            RenderSettings.ambientLight = new Color32(r, g, b, 0);
            RenderSettings.fogDensity = fogDensity;
            yield return new WaitForSeconds(0.1f);
        }
    }
}