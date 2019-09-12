using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RayShooter : MonoBehaviour
{
    //im calling this playercharacter object to get it's health
    GameObject playerGameObj;
    private Animator _animator;
    private PlayerCharacter player;
    private Camera _camera;
    private Ray ray;
    private RaycastHit hit;
    private bool outofammo;
    private Vector3 point;
    private float lastShot;
    private SceneController controller;
    [SerializeField] private GameObject bloodGushFX;
    [SerializeField] private GameObject smokeFX;
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private GameObject muzzleFlash;
    [NonSerialized] public bool isLookingAtObject;
    private GameObject door;
    private GameObject alertedText;

    //audio stuff
    private AudioSource[] audioSource;
    private AudioSource bulletFire;
    private AudioSource outOfAmmo;


    public int bulletDamage;
    public float fireRate;

    private float distanceFromPlayer;
    private bool showingMessage;
    void Start()
    {
        //Scene scene = SceneManager.GetActiveScene();
        showingMessage = false;
        alertedText = GameObject.Find("Alerted Text");
        //guiStyle.normal.textColor = Color.white;
        //guiStyle.fontSize = 35;
        isLookingAtObject = false;
        fireRate = .5f;
        playerGameObj = GameObject.Find("Player");
        _animator = GetComponentInParent<Animator>();
        _camera = GetComponent<Camera>();
        player = GetComponentInParent<PlayerCharacter>();
        controller = GameObject.Find("Game Controller").GetComponent<SceneController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        audioSource = player.GetComponentsInParent<AudioSource>();
        bulletFire = audioSource[2];
        outOfAmmo = audioSource[7];
    }
    //this is used to change the font for the health to be displayed
    //private GUIStyle guiStyle = new GUIStyle(); //create a new variable


    void Update()
    {
       if (player.getHealth() > 0)
        {

            point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            ray = _camera.ScreenPointToRay(point);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject objToInteract = hit.transform.gameObject;
                distanceFromPlayer = Vector3.Distance(hit.transform.position, transform.position);
                if (distanceFromPlayer < 10)
                {
                    // Debug.Log(objToInteract.name);

                    //moves hands over an object
                    if (objToInteract.tag == "Object")
                        isLookingAtObject = true;
                    else
                        isLookingAtObject = false;


                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        switch (objToInteract.name)
                        {
                            case "Door1":
                                {
                                    door = GameObject.Find("Door2");
                                    playerGameObj.transform.position = door.transform.position;
                                    playerGameObj.transform.rotation = door.transform.rotation;
                                    break;
                                }
                            case "Door2":
                                {
                                    door = GameObject.Find("Door1");
                                    playerGameObj.transform.position = door.transform.position;
                                    playerGameObj.transform.rotation = door.transform.rotation;
                                    break;
                                }
                            case "Medkit":
                                {
                                    objToInteract.GetComponent<MedKit>().PickedUp();
                                    break;
                                }
                            case "Ammo":
                                {
                                    objToInteract.GetComponent<Ammo>().PickedUp();
                                    break;
                                }
                            case "clock":
                                {
                                    controller.SaveGame();
                                    break;
                                }
                            case "MainDoor":
                                {
                                    if (controller.GetTotalEnemyType1() == 0)
                                        SceneManager.LoadScene("EndingScene");
                                    break;
                                }
                            case "Notebook":
                                {
                                    if (!showingMessage)
                                        StartCoroutine(ShowMessage("If youre reading this, i've locked myself inside the study where I know it's safe. " +
                                            "I think i'm being watched. Please help. The key is in the drawer"));
                                    break;
                                }
                            default:
                                {
                                    Debug.Log("You can't pick this up");
                                    break;
                                }
                        }

                    }
                }
                else
                {
                    isLookingAtObject = false;
                }
            }

            //firing
            if (Input.GetMouseButtonDown(0))
            {
                if (player.getAmmo() > 0)
                {
                    if (GetComponentInParent<FPSInput>().speed < 7)
                    {

                        if (Time.time > fireRate + lastShot)
                        {
                            lastShot = Time.time;
                            _animator.SetTrigger("fire");

                            StartCoroutine(MuzzleFlash());

                            bulletFire.Play();
                            player.firedAmmo();
                            point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
                            ray = _camera.ScreenPointToRay(point);
                            if (Physics.Raycast(ray, out hit))
                            {
                                GameObject hitObject = hit.transform.gameObject;
                                //Debug.Log("Hit Object: " + hitObject.name);

                                //here i will check if the ITakeDamage interface exists. and if it does, call it
                                ITakeDamage damagable = hitObject.GetComponent<ITakeDamage>();

                                if (damagable != null)
                                {
                                    damagable.TakeDamage(25);
                                    //now check if its an NPC (something that bleeds when you shoot it)
                                    if(hitObject.GetComponent<NPC>())
                                        StartCoroutine(BloodGush(hitObject, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)));
                                }

                                //------------THIS IS MY OLD CODE BEFORE IMPLEMENTING INTERFACES!-------------------------

                                //if it hits an NPC... (or something that can take damage)
                                //if (hitObject.GetComponent<NPC>())
                                //{
                                    //hitObject.GetComponent<NPC>().Hit(bulletDamage);
                                //    hitObject.GetComponent<ITakeDamage>();
                                //    StartCoroutine(BloodGush(hitObject, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)));
                                //}
                                //else if (hitObject.GetComponent<Headshot>())
                                //{
                                //    hitObject.GetComponent<Headshot>().Hit(bulletDamage * 2);
                                //    StartCoroutine(BloodGush(hitObject, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)));
                                //}
                                else
                                {
                                    //spawn bullethole
                                    StartCoroutine(BulletSmoke(hitObject, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)));
                                    StartCoroutine(BulletHole(hitObject, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)));
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!showingMessage)
                        StartCoroutine(ShowMessage("Out Of Ammo..."));
                    outOfAmmo.Play();
                }
            }
        }
    }

    private IEnumerator BloodGush(GameObject hitObject, Vector3 position, Quaternion rotation)
    {
        //var rot = Quaternion.FromToRotation(Vector3.up, Quaternion test);
        GameObject blood = Instantiate(bloodGushFX);
        //Instantiate(blood);
        blood.transform.position = position;
        blood.transform.rotation = rotation;

        yield return new WaitForSeconds(0.3f);
        Destroy(blood);
    }

    private IEnumerator BulletSmoke(GameObject hitObject, Vector3 position, Quaternion rotation)
    {
        GameObject smoke = Instantiate(smokeFX);
        smoke.transform.position = position;
        smoke.transform.rotation = rotation;
        yield return new WaitForSeconds(2f);
        Destroy(smoke);
    }
    private IEnumerator BulletHole(GameObject hitObject, Vector3 position, Quaternion rotation)
    {
        GameObject hole = Instantiate(bulletHole);
        hole.transform.position = position;
        hole.transform.rotation = rotation;
        Vector3 rotate = new Vector3(85, 90, 0);
        hole.transform.Rotate(rotate);
        yield return new WaitForSeconds(10f);
        Destroy(hole);
    }

    private IEnumerator MuzzleFlash()
    {
        GameObject muzzleflash = GameObject.Find("MuzzleFlash");
        Light lightSource = muzzleflash.GetComponent<Light>();

        lightSource.intensity = 1;
        lightSource.enabled = true;
        yield return new WaitForSeconds(0.1f);
        lightSource.intensity = 2;
        yield return new WaitForSeconds(0.1f);
        lightSource.intensity = 1;
        yield return new WaitForSeconds(0.1f);
        lightSource.enabled = false;
    }


    IEnumerator ShowMessage(string message)
    {
        showingMessage = true;

        alertedText.GetComponent<Text>().text = message;
        for (float i = 0; i < 1; i += 0.02f)
        {
            alertedText.GetComponent<CanvasGroup>().alpha = i;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(5.0f);
        for (float i = 1; i > 0; i -= 0.02f)
        {
            alertedText.GetComponent<CanvasGroup>().alpha = i;
            yield return new WaitForEndOfFrame();
        }

        showingMessage = false;

    }

}