using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//enemy 3 bystander ai script

public class EnemyAI3 : MonoBehaviour
{

    //Chance of going back (make it seem less... robotic?)
    [SerializeField] float _switchProbability = 0.2f;

    //list of all patrol points
    private GameObject player;
    private GameObject waypointPrefab;
    private NPC enemy;
    List<Waypoint> runningPoints;

    //AudioSource[] audioSources;
    NavMeshAgent _navMeshAgent;
    private int _currentPatrolIndex;
    private float distanceFromPlayer;

    //statuses
    bool _travelling;
    bool _patrolForward;
    [System.NonSerialized] public bool isAttacking;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        waypointPrefab = GameObject.FindGameObjectWithTag("Waypoints Patrolling");
        enemy = GetComponent<NPC>();
        enemy.type = 3;
        //audioSources = player.GetComponents<AudioSource>();

        //isFighting = false;
        runningPoints = new List<Waypoint>();

        //this gets all the waypoints and puts them in a list
        foreach (Transform waypoint in waypointPrefab.transform)
        {
            runningPoints.Add(waypoint.GetComponent<Waypoint>());
        }

        _navMeshAgent = GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.Log("No nav mesh agent attached");
        }
        else
        {
            if (runningPoints != null && runningPoints.Count >= 2)
            {
                _currentPatrolIndex = UnityEngine.Random.Range(0, runningPoints.Count);
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
                if (distanceFromPlayer < 18)
                {
                    _navMeshAgent.speed = 16f;
                    if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
                    {
                        _travelling = false;

                        ChangePatrolPoint();
                        SetDestination();
                    }

                }
                else
                {
                    //set speed back
                    _navMeshAgent.speed = 3.0f;

                    if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
                    {
                        _travelling = false;

                        ChangePatrolPoint();
                        SetDestination();
                    }

                }
            }
            else
            {
                //set speed back
                _navMeshAgent.speed = 3.0f;

                if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
                {
                    _travelling = false;

                    ChangePatrolPoint();
                    SetDestination();
                }

            }
        }
        else
        {
            _navMeshAgent.isStopped = true;
        }


    }
    private void SetDestination()
    {
        if (runningPoints != null)
        {
            Vector3 targetVector = runningPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
        }
    }
    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
        {
            _patrolForward = !_patrolForward;
        }
        if (_patrolForward)

        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % runningPoints.Count;
        }
        else
        {
            _currentPatrolIndex--;
            if (_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = runningPoints.Count - 1;
            }
        }
    }
}
