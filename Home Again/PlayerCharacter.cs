using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{
    private int _health;
    private int ammo;
    private GameObject childObj;
    public bool waitingForUser;
    //implementing max health allows me to have med-kits heal to max health;
    public int maxHealth = 100;
    public int maxAmmo = 10;
    Camera[] _cameras;
    Camera deathCam;
    CameraShake camShake;
    CanvasGroup hitFlash;
    AudioSource[] audioSources;
    AudioSource[] controllerAudioSources;
    AudioSource playerHurtSound;
    AudioSource playerSlashedSound;
    AudioSource playerHealedSound;
    AudioSource playerFoundAmmoSound;
    //SceneController sceneController;
    CanvasGroup gameOverMenu;
    [System.NonSerialized] public bool isAlive;

    //i need a way to see if im being attacked at all (audio overlapping issues)
    [System.NonSerialized] public bool beingAttacked;

    void Start()
    {
        //sceneController = GameObject.Find("GameOverMenu").GetComponent<SceneController>();
        //Debug.Log(sceneController.name);
        gameOverMenu = GameObject.Find("GameOverMenu").GetComponent<CanvasGroup>();
        hitFlash = GameObject.Find("HitFlash").GetComponent<CanvasGroup>();
        _cameras = GetComponentsInChildren<Camera>();
        deathCam = _cameras[2];
        camShake = GetComponent<CameraShake>();
        controllerAudioSources = GameObject.Find("Game Controller").GetComponents<AudioSource>();
        audioSources = GetComponents<AudioSource>();
        playerHurtSound = audioSources[3];
        playerHealedSound = audioSources[5];
        playerFoundAmmoSound = audioSources[8];
        playerSlashedSound = controllerAudioSources[2];
        _health = maxHealth;
        ammo = maxAmmo;
        beingAttacked = false;
        isAlive = true;
        waitingForUser = false;


    }

    //need a way to make sure health doesnt go over 100
    private void Update()
    {
        if (_health > maxHealth)
            _health = maxHealth;

        if (_health <= 0)
        {
            deathCam.enabled = true;
            isAlive = false;
            _health = 0;
            
            if(!waitingForUser)
                StartCoroutine(YouDied());
        }
        else
        {
            deathCam.enabled = false;
            isAlive = true;
        }
        if (ammo > maxAmmo)
            ammo = maxAmmo;
        else if (ammo < 0)
            ammo = 0;

    }

    public void Hurt(int damage)
    {
        if (_health <= damage)
            _health = 0;
        else
            _health -= damage;


        StartCoroutine(HitFlash());

        camShake.ShakeCamera();
        playerHurtSound.Play();
        playerSlashedSound.Play();

    }

    //this will get called when a player collides with a MedKit
    public void Healed(int healAmt)
    {
        if (_health > maxHealth - healAmt)
            _health = maxHealth;
        else
            _health += healAmt;

        playerHealedSound.Play();
    }

    public void firedAmmo()
    {
        ammo--;
    }

    public void foundAmmo(int ammoAmt)
    {
        ammo += ammoAmt;
        playerFoundAmmoSound.Play();

    }

    //accessor for HUD
    public int getHealth()
    {
        return _health;
    }
    public int getAmmo()
    {
        return ammo;
    }

    public void setHealth(int health)
    {
        _health = health;
    }

    public void setAmmo(int ammo)
    {
        this.ammo = ammo;
    }

    //reset health and ammo for game reset
    public void ResetHealth()
    {
        _health = maxHealth;
    }

    public void ResetAmmo()
    {
        ammo = maxAmmo;
    }

    IEnumerator YouDied()
    {
        waitingForUser = true;
        yield return new WaitForSeconds(3.5f);
        gameOverMenu.alpha = 1;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator HitFlash()
    {
        hitFlash.alpha = 1f;
        yield return new WaitForEndOfFrame();
        hitFlash.alpha = .5f;
        yield return new WaitForEndOfFrame();
        hitFlash.alpha = 0f;
    }
}
