using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
//using UnityEngine.UI;
//scene controller

public class SceneController : MonoBehaviour
{
    //prefabs
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject hidingEnemyPrefab;
    [SerializeField] private GameObject enemyBystander;
    [SerializeField] private GameObject medkitPrefab;
    [SerializeField] private GameObject ammoPrefab;

    private GameObject ammo;
    private GameObject medkit;
    private GameObject player;
    private GameObject alertedText;

    //audio sources
    private AudioSource[] playerAudio;
    private AudioSource[] controllerAudio;

    //will make everything spawn around the controller
    private Vector3 ControllerPosition;

    //i need to manage the instantied clones
    List<GameObject> cloneList;
    List<GameObject> savedList;
    //static list of volumes
    List<float> staticPlayerVolumes;
    List<float> staticControllerVolumes;

    public int minSpawnRange;
    public int maxSpawnRange;
    public int numToSpawn;
    private int randomNum;
    private int randomNum2;
    private bool canPlay;


    //number of enemies fighting or near or very near
    private int totalNPCFighting;
    private int totalNPCNear;
    private int totalNPCVeryNear;
    private int totalIsScreaming;
    private int totalNPCPlayingJumpScare;
    private int totalNPCType1;

    private int NPCFightingTemp;
    private int NPCNearTemp;
    private int NPCVeryNearTemp;
    private int isScreamingTemp;
    private int NPCPlayingJumpscareTemp;
    private int NPCType1Temp;


    bool fadingAudioIn;
    bool fadingAudioOut;
    bool showingMessage;

    //check if the game was saved
    bool saved;
    bool loading;

    //this stores the original position
    private Vector3 originalPlayerPos;
    private Vector3 originalMedkitPos;
    private Vector3 originalAmmoPos;

    //for saving
    private Vector3 savedPlayerPos;
    private Vector3 savedMedkitPos;
    private Vector3 savedAmmoPos;
    private int savedHealth;
    private int savedAmmo;


    [NonSerialized] public CanvasGroup gameOverMenu;

    private void Start()
    {
        Time.timeScale = 1;

        //gameOverMenu = GameObject.Find("GameOverMenu");
        gameOverMenu = GameObject.Find("GameOverMenu").GetComponent<CanvasGroup>();
        gameOverMenu.alpha = 0;

        loading = false;
        saved = false;
        showingMessage = false;
        alertedText = GameObject.Find("Alerted Text");
        canPlay = true;
        ControllerPosition = this.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        playerAudio = player.GetComponents<AudioSource>();
        controllerAudio = GetComponents<AudioSource>();
        staticPlayerVolumes = new List<float>();
        staticControllerVolumes = new List<float>();
        savedList = new List<GameObject>();

        //getting all the volumes
        for (int i = 0; i < playerAudio.Length; i++)
            staticPlayerVolumes.Add(playerAudio[i].volume);

        for (int i = 0; i < controllerAudio.Length; i++)
            staticControllerVolumes.Add(controllerAudio[i].volume);

        //i need to made a starting medkit to get its position to so i can reset it
        medkit = Instantiate(medkitPrefab) as GameObject;
        medkit.transform.position = new Vector3(UnityEngine.Random.Range(minSpawnRange,
            maxSpawnRange), 10, UnityEngine.Random.Range(minSpawnRange, maxSpawnRange));

        ammo = Instantiate(ammoPrefab) as GameObject;
        ammo.transform.position = new Vector3(UnityEngine.Random.Range(minSpawnRange,
            maxSpawnRange), 10, UnityEngine.Random.Range(minSpawnRange, maxSpawnRange));

        //starting positions
        originalMedkitPos = medkit.transform.position;
        originalPlayerPos = player.transform.position;
        originalAmmoPos = ammo.transform.position;

        cloneList = new List<GameObject>();
        cloneList.AddRange(SpawnNPCS(numToSpawn));

        NPCFightingTemp = 0;
        NPCNearTemp = 0;
        NPCVeryNearTemp = 0;
        isScreamingTemp = 0;
        NPCType1Temp = 0;

    }

    void Update()
    {
        //here i am constantly cycling through the enemies
        if (!loading)
        {
            for (int i = 0; i < cloneList.Count; i++)
            {
                //if its null, then it died, and if it died it means i need to spawn enemies
                if (!cloneList[i])
                {
                    cloneList.RemoveAt(i);
                    //if there are less than 8 clones generate 0-2 enemies
                    //that way i can randomize up to 2 all the way to 7. worst case is
                    //it hits exactly 10 clones
                    if (cloneList.Count < 8)
                    {
                        randomNum = UnityEngine.Random.Range(0, 3);
                        cloneList.AddRange(SpawnNPCS(randomNum));
                    }
                }

                //check for out of bounds
                if (cloneList[i] == null)
                    i = 0;

                //checking if it is fighting or near
                if (cloneList[i].GetComponent<NPC>().isAttacking)
                    NPCFightingTemp++;

                if (cloneList[i].GetComponent<NPC>().isNear)
                    NPCNearTemp++;

                if (cloneList[i].GetComponent<NPC>().isVeryNear)
                    NPCVeryNearTemp++;

                if (cloneList[i].GetComponent<NPC>().isPlayingJumpscare)
                    NPCPlayingJumpscareTemp++;

                //checking if any is screaming (to alert enemy NPC)
                if (cloneList[i].GetComponent<EnemyAI2>())
                {
                    if (cloneList[i].GetComponent<EnemyAI2>().isScreaming)
                        isScreamingTemp++;
                }

                //check if the enemy is the wandering kind
                if (cloneList[i].GetComponent<NPC>().type == 1)
                    NPCType1Temp++;


            }

            //check any of the above conditions
            //after it has checked all the fighting enemies it will add them together then reset temp
            totalIsScreaming = isScreamingTemp;
            totalNPCNear = NPCNearTemp;
            totalNPCVeryNear = NPCVeryNearTemp;
            totalNPCFighting = NPCFightingTemp;
            totalNPCPlayingJumpScare = NPCPlayingJumpscareTemp;
            totalNPCType1 = NPCType1Temp;
            NPCNearTemp = 0;
            NPCVeryNearTemp = 0;
            NPCFightingTemp = 0;
            NPCPlayingJumpscareTemp = 0;
            isScreamingTemp = 0;
            NPCType1Temp = 0;

            //check if anyone is fighting
            if (totalNPCFighting > 0)
            {
                if (!playerAudio[1].isPlaying)
                    StartCoroutine(FadeInAudio(playerAudio[1], staticPlayerVolumes[1]));
            }
            else
            {
                if (playerAudio[1].isPlaying)
                    StartCoroutine(FadeOutAudio(playerAudio[1]));
            }

            //check if anyone is near, if yes, play heart beat sound
            if (totalNPCNear > 0)
            {
                if (!playerAudio[6].isPlaying)
                    StartCoroutine(FadeInAudio(playerAudio[6], staticPlayerVolumes[6]));
            }
            else
            {
                if (playerAudio[6].isPlaying)
                    StartCoroutine(FadeOutAudio(playerAudio[6]));
            }

            //check if anyone is very near
            if (totalNPCVeryNear > 0)
            {
                if (!controllerAudio[1].isPlaying)
                    StartCoroutine(FadeInAudio(controllerAudio[1], staticControllerVolumes[1]));
            }
            else
            {
                if (controllerAudio[1].isPlaying)
                    StartCoroutine(FadeOutAudio(controllerAudio[1]));
            }

            if (totalNPCPlayingJumpScare > 0 && canPlay)
            {
                canPlay = false;
                if (!controllerAudio[0].isPlaying)
                    controllerAudio[0].Play();
            }

            if (totalIsScreaming > 0)
            {
                if (totalNPCType1 > 0)
                {
                    if (!controllerAudio[3].isPlaying)
                        controllerAudio[3].Play();
                    if (!showingMessage)
                        StartCoroutine(ShowDelayedMessage("Something heard you..."));
                }
            }


            if (medkit == null)
            {
                medkit = Instantiate(medkitPrefab) as GameObject;
                medkit.transform.position = new Vector3(UnityEngine.Random.Range(minSpawnRange,
                maxSpawnRange), 10, UnityEngine.Random.Range(minSpawnRange, maxSpawnRange));
            }
            if (ammo == null)
            {
                ammo = Instantiate(ammoPrefab) as GameObject;
                ammo.transform.position = new Vector3(UnityEngine.Random.Range(minSpawnRange,
                maxSpawnRange), 10, UnityEngine.Random.Range(minSpawnRange, maxSpawnRange));
            }
        }


        if (Input.GetKeyDown(KeyCode.P))
            ResetGame();
    }

    //a reset function could be handy later
    public void ResetGame()
    {

        player.transform.position = originalPlayerPos;
        medkit.transform.position = originalMedkitPos;
        ammo.transform.position = originalAmmoPos;
        player.GetComponent<PlayerCharacter>().ResetHealth();
        player.GetComponent<PlayerCharacter>().foundAmmo(5);


        //this clears the entire list
        int listSize = cloneList.Count;
        for (int i = 0; i < listSize; i++)
        {
            //Destroy(cloneList[i]);
            Destroy(cloneList[0]);
            cloneList.RemoveAt(0);
        }

        //this will generate a new 5
        cloneList.AddRange(SpawnNPCS(numToSpawn));
    }

    public void SaveGame()
    {
        saved = true;
        List<GameObject> temp = new List<GameObject>();
        temp.AddRange(cloneList);

        //this clears the entire list
        int listSize = savedList.Count;
        for (int i = 0; i < listSize; i++)
        {
            Destroy(savedList[0]);
            savedList.RemoveAt(0);
        }

        savedList.AddRange(temp);

        savedHealth = player.GetComponent<PlayerCharacter>().getHealth();
        savedAmmo = player.GetComponent<PlayerCharacter>().getAmmo();
        savedPlayerPos = player.transform.position;
        savedMedkitPos = medkit.transform.position;
        savedAmmoPos = ammo.transform.position;

        StartCoroutine(ShowMessage("Game Saved"));

    }

    public void LoadGame()
    {
        if (saved)
        {
            loading = true;
            //user has made a choice
            player.GetComponent<PlayerCharacter>().waitingForUser = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;


            gameOverMenu.alpha = 0;

            int listSize = cloneList.Count;
            for (int i = 0; i < listSize; i++)
            {
                //Destroy(cloneList[i]);
                Destroy(cloneList[0]);
                cloneList.RemoveAt(0);
            }

            cloneList.AddRange(savedList);
            player.transform.position = savedPlayerPos;
            medkit.transform.position = savedMedkitPos;
            ammo.transform.position = savedAmmoPos;
            player.GetComponent<PlayerCharacter>().setHealth(savedHealth);
            player.GetComponent<PlayerCharacter>().setAmmo(savedAmmo);

            loading = false;


        }
        else
        {
            StartCoroutine(ShowMessage("No Save Found"));
        }

 
    }

    //total npc fighting accessor for wanderingAI to stop shooting
    public int getTotalNPCFighting()
    {
        return totalNPCFighting;
    }

    //spawn a list of NPCS
    public List<GameObject> SpawnNPCS(int numOfNPC)
    {
        //check to see if there is a hiding enemy 
        GameObject hidingEnemy = GameObject.FindGameObjectWithTag("Hiding Enemy");
        List<GameObject> temp = new List<GameObject>();
        if (numOfNPC == 0)
            return temp;

        if (!hidingEnemy)
            temp.Add(Instantiate(hidingEnemyPrefab));


        for (int i = 0; i < numOfNPC; i++)
        {
            randomNum2 = UnityEngine.Random.Range(0, 2);

            if (randomNum2 == 0)
                temp.Add(Instantiate(enemyPrefab));

            else if (randomNum2 == 1)
                temp.Add(Instantiate(enemyBystander));

            temp[i].transform.position = ControllerPosition;
        }

        return temp;

    }

    public int GetTotalEnemyType1()
    {
        return totalNPCType1;
    }

    //this allows my sounds to fade in and out
    /// <param name="maxVolume">the volume it will peak at</param>
    IEnumerator FadeInAudio(AudioSource audiosource, float maxVolume)
    {
        //this one needs a max volume because its possible i dont want it to play at full volume (1)
        //maxVolume = audiosource.volume;
        audiosource.volume = 0;
        audiosource.Play();
        for (float i = 0f; i < maxVolume; i += 0.1f)
        {
            audiosource.volume = i;
            yield return new WaitForSeconds(0.1f);
        }
        //audiosource.volume = maxVolume;
    }

    IEnumerator FadeOutAudio(AudioSource audiosource)
    {
        for (float i = audiosource.volume; i > 0; i -= 0.1f)
        {
            audiosource.volume = i;
            yield return new WaitForSeconds(0.1f);
        }
        audiosource.Stop();
    }

    IEnumerator ShowDelayedMessage(string message)
    {
        showingMessage = true;
        alertedText.GetComponent<Text>().text = message;

        yield return new WaitForSeconds(3.0f);
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

    IEnumerator ShowMessage(string message)
    {

        alertedText.GetComponent<Text>().text = message;
        for (float i = 0; i < 1; i += 0.02f)
        {
            alertedText.GetComponent<CanvasGroup>().alpha = i;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(4.0f);
        for (float i = 1; i > 0; i -= 0.02f)
        {
            alertedText.GetComponent<CanvasGroup>().alpha = i;
            yield return new WaitForEndOfFrame();
        }

    }
}

