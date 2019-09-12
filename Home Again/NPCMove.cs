using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour {

    [SerializeField] Transform _destination;
    NavMeshAgent _navMeshAgent;

	void Start () {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if(_navMeshAgent == null)
        {
            Debug.LogError("Nav Agent Not Attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }
		
	}
	
	// Update is called once per frame
    private void SetDestination()
    {
        Vector3 targetVector = _destination.transform.position;
        _navMeshAgent.SetDestination(targetVector);
    }

}
