/*
Originial Coder: Owynn A.
Recent Coder: Owynn A.
Recent Changes: Added throwing behaviour
Last date worked on: 10/4/2025
*/

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GhostBehaviour : MonoBehaviour
{
    public Animator animator;
    public float radius = .0005f;
    public float waitTime = 2f, delay = .5f;
    private NavMeshAgent agent;
    public bool isWandering = true, isDrifting = false;
	public Transform cameraTransform;

    //public Vector3Data center;  replace later
    public Vector3 center, driftPointOne, driftPointTwo;
    public Quaternion rotation;
    public float rotationSpeed = 5f;
    private int position = 0;

	[SerializeField] private ThrowObjectBehavior throwManager;
    [SerializeField] private ObjectListSO throwablePrefab;
    [SerializeField] private GameObject startObject;
    [SerializeField] private Transform target;
    [SerializeField] private float throwSpeed = 1f;
    [SerializeField] private float overshoot = 0.5f;

    private GameObject currentThrowable;
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
			Attack();
			yield return new WaitForSeconds(delay);
			Vector3 location = target.position;

            if (currentThrowable != null)
            {
                // Stop coroutine for this object
                throwManager.EndThrow(currentThrowable);

                // Destroy the object
                Destroy(currentThrowable);
                currentThrowable = null;
            }
			int randomInt = Random.Range(0, throwablePrefab.list.Count);
            currentThrowable = Instantiate(throwablePrefab.list[randomInt], startObject.transform.position, Quaternion.identity);

            throwManager.StartThrow(currentThrowable, location, throwSpeed);
            
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
    public void Attack()
    {
        animator.SetTrigger("attack");

        //Wwise Audio trigger
        //ghost_attack.Post(gameObject);
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
}
