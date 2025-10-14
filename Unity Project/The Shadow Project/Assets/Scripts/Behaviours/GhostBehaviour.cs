/*
Originial Coder: Owynn A.
Recent Coder: Owynn A.
Recent Changes: Added throwing behaviour
Last date worked on: 10/4/2025
*/

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class GhostBehaviour : MonoBehaviour
{
    [Header("Settings")]
    public float radius = .0005f;
    public float waitTime = 2f, delay = .5f;
    [SerializeField] private float throwSpeed = 1f;
    [SerializeField] private float levitationHeight = 2f;
    [SerializeField] private float overshoot = 0.5f;

    [Header("Components")]
    public Animator animator;
    [SerializeField] private Transform target;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private ThrowObjectBehavior throwManager;

    [Header("Info")]
    private NavMeshAgent agent;
    public bool isWandering = true, isDrifting = false;
	public Transform cameraTransform;

    //public Vector3Data center;  replace later
    public Vector3 center, driftPointOne, driftPointTwo;
    public Quaternion rotation;
    public float rotationSpeed = 5f;

    private ObjectBehaviour currentThrowable;
    private void Awake()
    {
        center = transform.position;
        rotation = transform.rotation;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(WanderRoutine());
    }
	public void StartWander()
	{
		StartCoroutine(WanderRoutine());
	}	

    private IEnumerator WanderRoutine()
    {
        agent.updateRotation = true;
        while (isWandering)
        {
            Vector3 destination = GetRandomNavMeshPoint(transform.position, radius);
            agent.SetDestination(destination);
            Walk();
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            Idle();
            
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator DriftRoutine()
    {
        while (isDrifting)
        {
            Vector3 destination = Vector3.Lerp(driftPointOne, driftPointTwo, Random.value);
            agent.SetDestination(destination);
            Walk();
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
				transform.LookAt(cameraTransform.position);
            	transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                yield return null;
            }
			
            Idle();
            StartCoroutine(Attack());

            yield return new WaitForSeconds(waitTime);
        }
    }
    private Vector3 GetRandomNavMeshPoint(Vector3 center, float radius)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPos = center + Random.insideUnitSphere * radius;
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return center;
    }

    public void StartEndIdle()
    {

		animator.SetTrigger("suprise");
        StartCoroutine(EndIdle());
		
    }

    public IEnumerator EndIdle()
    {
        isWandering = false;
        agent.updateRotation = false;
        agent.SetDestination(center);
        
        Walk();
		
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        Idle();
		
        isDrifting = true;
        StartCoroutine(DriftRoutine());
    }
    public IEnumerator Attack()
    {
        ObjectBehaviour newSelectedObject = SelectObject();
        GameObject throwable = newSelectedObject.gameObject;

        Vector3 location = target.position;

        if (currentThrowable != null)
        {
            throwManager.EndThrow(currentThrowable.gameObject);
            // Destroy the object
            Destroy(currentThrowable.gameObject);
            currentThrowable = null;
        }

        LevitateObject(throwable);
        // Add levitate animation

        yield return new WaitForSeconds(1);

        animator.SetTrigger("attack");
        ThrowObject(throwable, location);

        currentThrowable = newSelectedObject;

        //Wwise Audio trigger
        //ghost_attack.Post(gameObject);
    }

    public void ThrowObject(GameObject throwable, Vector3 location)
    {
        throwManager.StartThrow(throwable, location, throwSpeed);
    }
    
    public void LevitateObject(GameObject throwable)
    {
        Vector3 location = throwable.transform.position + new Vector3(0, levitationHeight, 0);
        throwManager.StartThrow(throwable, location, throwSpeed);
    }

    public void Walk()
    {
        animator.SetBool("isWalking", true);
    }

    public void Idle()
    {
        animator.SetBool("isWalking", false);
    }

    public void Disappear()
    {
        animator.SetTrigger("disappear");
    }

    public void Appear()
    {
        
    }

    public void TakeDamage()
    {

    }
    
    // Helper Functions

    public ObjectBehaviour SelectObject()
    {
        List<ObjectBehaviour> allObjects = objectManager.spawnedObjects;
        ObjectBehaviour selectedObject = allObjects[Random.Range(0, allObjects.Count)];

        return selectedObject;
    }
}
