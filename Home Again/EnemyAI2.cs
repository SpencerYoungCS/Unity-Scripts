using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//enemy2 (hiding) ai script

public class EnemyAI2 : MonoBehaviour
{
    //list of all patrol points
    [SerializeField] GameObject waypointPrefab;
    List<Waypoint> hidingWaypoints;
    private GameObject player;
    NavMeshAgent _navMeshAgent;
    float distanceFromPlayer;
    //these are bools that are used in other scripts but are not to be touched (kind of easier than using getter/setters
    [System.NonSerialized] public bool isScreaming;
    [System.NonSerialized] public bool isFalling;
    [System.NonSerialized] public bool isHitting;
    [System.NonSerialized] public bool reachedHidingSpot;
    //[System.NonSerialized] public bool chasing;
    private NPC enemy;
    private float remainingDistance;

    private bool initialScream;
    private bool initialFall;

    private AudioSource[] enemyAudioSources;
    private AudioSource scream;
    private AudioSource crying;

    // Use this for initialization
    void Start()
    {


        enemy = GetComponent<NPC>();
        enemy.type = 2;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAudioSources = GetComponents<AudioSource>();
        scream = enemyAudioSources[0];
        crying = enemyAudioSources[1];
        reachedHidingSpot = false;
        initialScream = false;
        initialFall = false;
        isScreaming = false;
        isFalling = false;
        isHitting = false;
        //        chasing = false;
        //ill initialize it really high because it takes a few frames to calculate the path, so my hiding enemy wont start chasing me right away;
        remainingDistance = 100f;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        hidingWaypoints = new List<Waypoint>();

        //add the waypoints into the list
        foreach (Transform waypoint in waypointPrefab.transform)
        {
            hidingWaypoints.Add(waypoint.GetComponent<Waypoint>());
        }

        if (_navMeshAgent == null)
        {
            Debug.Log("No nav mesh agent attached");
        }
        else
        {
            if (hidingWaypoints != null && hidingWaypoints.Count >= 2)
            {
                int randomNum = UnityEngine.Random.Range(0, hidingWaypoints.Count - 1);
                _navMeshAgent.SetDestination(hidingWaypoints[randomNum].transform.position);
            }
            else
            {
                Debug.Log("Not enough Patrol Points");
            }
        }
    }

    void Update()
    {
        //Debug.Log("distance from player:" + distanceFromPlayer);
        if (enemy.isAlive)
        {
            //once it has gotten to position then it will be hostile
            if (!reachedHidingSpot)
            {
                if (!_navMeshAgent.pathPending)
                {
                    if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                    {
                        if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                        {
                            _navMeshAgent.isStopped = true;
                            reachedHidingSpot = true;
                        }
                        else
                            _navMeshAgent.isStopped = false;
                    }
                }
            }

            //reached hiding spot resume enemyAI------------------------------------------------------------------------------------

            distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

            //if the player is alive..
            if (player.GetComponent<PlayerCharacter>().getHealth() > 0)
            {
                if (reachedHidingSpot)
                {
                    //after it reached the hiding spot, set the destination to the player, so we can caluclate the remaining distance using navmeshagent.remainingdistance
                    _navMeshAgent.SetDestination(player.transform.position);
                    if (!enemy.isAttacking)
                    {
                        if (!_navMeshAgent.pathPending)
                            remainingDistance = _navMeshAgent.remainingDistance;
                        //if you shot her... she'll attack
                        if (enemy.health < 100)
                        {
                            //initial scream, will only play once
                            if (!initialScream)
                            {
                                initialScream = true;
                                if (crying.isPlaying)
                                    crying.Stop();
                                StartCoroutine(Scream());
                            }
                        }
                        else
                        {
                            if (remainingDistance < 35)
                            {
                                enemy.isNear = true;
                                if (remainingDistance < 13)
                                {
                                    if (!initialScream)
                                    {
                                        if (crying.isPlaying)
                                            crying.Stop();

                                        initialScream = true;
                                        StartCoroutine(Scream());
                                    }
                                }
                            }
                            else
                            {
                                enemy.isNear = false;
                            }
                        }
                    }
                    else
                    {
                        // Debug.Log("isScreaming: " + isScreaming + " isFalling: " + isFalling + " isHitting: " + isHitting);
                        //after she is aggroed at all, the code will resume here. ------------------------------------------------------------------------------------------------------------------
                        if (enemy.health < 60)
                        {
                            _navMeshAgent.speed = 2f;
                            if (!initialFall)
                            {
                                initialFall = true;
                                StartCoroutine(Fall());
                            }

                        }
                        if (!isScreaming && !isFalling && !isHitting)
                        {
                            if (distanceFromPlayer <= 4)
                                StartCoroutine(Hitting());
                            else
                                _navMeshAgent.SetDestination(player.transform.position);
                        }
                        else
                        {
                            _navMeshAgent.isStopped = true;
                        }
                    }
                }
            }
            //player is dead
            else
            {
                _navMeshAgent.isStopped = true;
                enemy.isNear = false;
                enemy.isAttacking = false;
            }
        }
        //enemy2 is dead
        else
        {
            if (crying.isPlaying)
                crying.Stop();

            enemy.isNear = false;
            enemy.isAttacking = false;
            //chasing = false;
            _navMeshAgent.isStopped = true;

        }

    }

    //this allows my sounds to fade in and out
    IEnumerator FadeInAudio(AudioSource audiosource)
    {
        audiosource.volume = 0;
        audiosource.Play();
        for (float i = 0f; i < 1f; i += 0.1f)
        {
            audiosource.volume = i;
            yield return new WaitForSeconds(0.1f);
        }
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
    public float getDistanceFromPlayer()
    {
        return distanceFromPlayer;
    }

    public static bool GetPath(NavMeshPath path, Vector3 fromPos, Vector3 toPos, int passableMask)
    {
        path.ClearCorners();

        if (NavMesh.CalculatePath(fromPos, toPos, passableMask, path) == false)
            return false;

        return true;
    }

    IEnumerator Scream()
    {
        isScreaming = true;
        _navMeshAgent.isStopped = true;
        //look at the player
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
        //keep them from looking up or down
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        yield return new WaitForSeconds(.5f);
        scream.Play();
        yield return new WaitForSeconds(2f);
        _navMeshAgent.isStopped = false;
        isScreaming = false;
        //after shes done screaming, shes now in attacking and chasing forever state.
        enemy.isAttacking = true;
        //chasing = true;
    }

    IEnumerator Fall()
    {
        isFalling = true;
        _navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(3.3f);
        _navMeshAgent.isStopped = false;
        isFalling = false;
    }

    IEnumerator Hitting()
    {
        isHitting = true;
        _navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(1.2f);
        _navMeshAgent.isStopped = false;
        isHitting = false;
    }

}
