using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//enemy1 ai script

public class EnemyAI1 : MonoBehaviour
{
    //Time to wait at each point
    private float _totalWaitTime;
    //Chance of going back (make it seem less... robotic?)
    private float _switchProbability;
    //list of all patrol points
    [SerializeField] GameObject waypointPrefab;

    private NPC enemy;
    private GameObject player;
    NavMeshAgent _navMeshAgent;
    List<Waypoint> _patrolPoints;
    int _currentPatrolIndex;
    private float distanceFromPlayer;

    //statuses
    bool _travelling;
    bool _patrolForward;

    private float _waitTimer;
    private float lookTimer;
    //private float timeWaited;

    //stats
    [System.NonSerialized] public bool _waiting;
    [System.NonSerialized] public bool isLookingAround;
    [System.NonSerialized] public bool isHitting;
    [System.NonSerialized] public int aggroRange;
    private int defaultAggroRange;


    //audio stuff
    //private AudioSource chasingPlayer;
    //private AudioSource[] audioSources;

    // Use this for initialization
    void Start()
    {

        isHitting = false;
        isLookingAround = false;
        _totalWaitTime = 3f;
        _switchProbability = 0.2f;
        lookTimer = 3f;
        _waiting = false;
        enemy = GetComponent<NPC>();
        enemy.type = 1;
        defaultAggroRange = enemy.aggroRange;
        aggroRange = enemy.aggroRange;
        player = GameObject.FindGameObjectWithTag("Player");

        //audioSources = player.GetComponents<AudioSource>();

        _patrolPoints = new List<Waypoint>();

        //this gets all the waypoints and puts them in a list
        foreach (Transform waypoint in waypointPrefab.transform)
        {
            _patrolPoints.Add(waypoint.GetComponent<Waypoint>());
        }

        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.Log("No nav mesh agent attached");
        }
        else
        {
            if (_patrolPoints != null && _patrolPoints.Count >= 2)
            {
                _currentPatrolIndex = UnityEngine.Random.Range(0, _patrolPoints.Count);
                SetDestination();
            }
            else
            {
                Debug.Log("Not enough Patrol Points");
            }
        }
    }

    void Update()
    {

        if (enemy.isAlive)
        {
            distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (player.GetComponent<PlayerCharacter>().getHealth() > 0)
            {
                //if you shot him at all, hes going to chase you to death
                if (enemy.health < 100)
                {
                    _navMeshAgent.speed = 12.0f;
                    enemy.isPlayingJumpscare = false;
                    enemy.isAttacking = true;
                    _navMeshAgent.SetDestination(player.transform.position);
                    if (distanceFromPlayer <= 7)
                    {
                        //look at the player
                        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
                        //keep them from looking up or down
                        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

                        if (!isHitting)
                            StartCoroutine(Hitting());
                    }
                    else
                    {
                        _navMeshAgent.SetDestination(player.transform.position);
                    }
                }
                else
                {
                    //if the player is running the aggro range gets larger
                    if (player.GetComponent<FPSInput>().speed > 3)
                        aggroRange = defaultAggroRange + 12;
                    else
                        aggroRange = defaultAggroRange;

                    //decrease range if light is off
                    if (player.GetComponent<FPSInput>().speed < 3)
                        aggroRange = aggroRange - 5;


                    //further decrease the range if player is ducking
                    if (!player.GetComponent<FPSInput>().lightStatus)
                        aggroRange = aggroRange - 5;


                    // Debug.Log("Total Aggro Range: " + aggroRange);
                    //enemy stars sensing here

                    if (distanceFromPlayer < (aggroRange + 15))
                    {
                        enemy.isNear = true;
                        //gets the angle between me and the enemy, thus an angle
                        Vector3 targetDirection = player.transform.position - transform.position;
                        float viewAngle = Vector3.Angle(targetDirection, transform.forward);

                        if (viewAngle < 55.0f)
                        {
                            //he sees you.
                            enemy.isAttacking = true;
                            _navMeshAgent.speed = 12.0f;
                            isLookingAround = false;

                            //now i have to stop the enemy if its in front of me
                            if (distanceFromPlayer <= 7)
                            {
                                //look at the player
                                transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
                                //keep them from looking up or down
                                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                                if (!isHitting)
                                    StartCoroutine(Hitting());
                            }
                            else
                            {
                                if (!isHitting)
                                {
                                    _navMeshAgent.isStopped = false;
                                    _navMeshAgent.SetDestination(player.transform.position);
                                }
                            }
                        }

                        //he senses you
                        else if (distanceFromPlayer < aggroRange + 6)
                        {
                            //now youre just too close
                            if (distanceFromPlayer < aggroRange)
                            {
                                enemy.isPlayingJumpscare = false;
                                enemy.isAttacking = true;
                                isLookingAround = false;
                                _navMeshAgent.isStopped = false;
                                _navMeshAgent.SetDestination(player.transform.position);
                            }

                            //if he isn't looking around and he isn't attacking...
                            else if (!isLookingAround && !enemy.isAttacking)
                            {
                                StartCoroutine(LookingAround());
                                lookTimer = 0f;
                            }
                            //if he is looking around (because youre too close)...
                            else
                            {
                                //play suspense sound
                                //enemy will stop because he senses something
                                lookTimer += Time.deltaTime;
                                if (lookTimer >= _totalWaitTime)
                                {
                                    //play jump scare
                                    enemy.isPlayingJumpscare = true;
                                    isLookingAround = false;
                                    _navMeshAgent.isStopped = false;
                                    //look at the player
                                    transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
                                    //keep them from looking up or down
                                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                                    enemy.isAttacking = true;
                                    _navMeshAgent.SetDestination(player.transform.position);
                                }
                            }

                        }
                        else
                        {
                            enemy.isPlayingJumpscare = false;
                            if (!isLookingAround)
                                patrolAround();
                        }

                    }
                    //same else
                    else
                    {
                        enemy.isPlayingJumpscare = false;
                        enemy.isNear = false;
                        if (!isLookingAround)
                            patrolAround();
                    }
                }
            }
            else
            {
                enemy.isPlayingJumpscare = false;
                enemy.isNear = false;
                if (!isLookingAround)
                    patrolAround();
            }
        }
        else
        {
            enemy.isPlayingJumpscare = false;
            enemy.isNear = false;
            enemy.isAttacking = false;
            _navMeshAgent.isStopped = true;
        }
    }

    private void SetDestination()
    {
        if (_patrolPoints != null)
        {
            Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _navMeshAgent.isStopped = false;
            _travelling = true;
        }
    }
    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
            _patrolForward = !_patrolForward;

        if (_patrolForward)
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
        }
        else
        {
            _currentPatrolIndex--;
            if (_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = _patrolPoints.Count - 1;
            }
        }
    }

    private void patrolAround()
    {
        //set speed back
        enemy.isAttacking = false;
        _navMeshAgent.speed = 3.0f;

        //if he reached his destination, then start the waiting animation
        if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            _travelling = false;
            _waiting = true;
            _navMeshAgent.isStopped = true;
            _waitTimer = 0f;
        }
        //if hes waiting, start waiting animation
        if (_waiting)
        {
            _navMeshAgent.isStopped = true;
            //after waiting (waitTimer) amount of time, then change destination
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;
                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    IEnumerator Hitting()
    {
        _navMeshAgent.isStopped = true;
        isHitting = true;
        yield return new WaitForSeconds(1.3f);
        isHitting = false;
        _navMeshAgent.isStopped = false;

    }

    IEnumerator LookingAround()
    {
        _navMeshAgent.isStopped = true;
        enemy.isVeryNear = true;
        isLookingAround = true;
        yield return new WaitForSeconds(3f);
        isLookingAround = false;
        enemy.isVeryNear = false;
        _navMeshAgent.isStopped = false;
    }
    //used in animator
    public float getDistanceFromPlayer()
    {
        return distanceFromPlayer;
    }

}

