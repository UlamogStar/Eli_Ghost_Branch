/*
Originial Coder: Owynn A.
Recent Coder: Zackery E.
Recent Changes: Reorganized and fixed bugs. Added Attack Routine
for randomizing attacks, Added Strength Checkpoints and strength,
so when it hits an Strength Checkpoint, and on StrengthUpdate, the
ghost's strength increases allowing it to throw the next sized object
Last date worked on: 10/28/2025
*/

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

public class GhostBehaviour : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int strength; // 0 can throw small objects, 1 throws the next size up, and so on

    [Header("Settings")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float radius = .0005f;
    [SerializeField] private float waitTime = 2f, delay = .5f;
    [SerializeField] private float throwSpeed = 1f, levitateSpeed = 1f;
    [SerializeField] private float levitationHeight = 2f;
    [SerializeField] private float overshoot = 0.5f;
    [SerializeField] private Vector3 center, driftPointOne, driftPointTwo;
    [SerializeField] private List<int> strengthCheckpoints = new List<int>();

    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private IntData health;
    private Transform target;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private ThrowObjectBehavior throwManager;
    [SerializeField] private UIBehaviorScript indicatorManager;

    [SerializeField] private NavMeshAgent agent;

    [Header("Info")]
    [SerializeField] private Quaternion rotation;
    [SerializeField] private bool isWandering = false, isDrifting = false, isAttacking = false;

    private List<TransformDataList> players;

    #region Unity Functions

    private void Awake()
    {
        // Set ghost to start, start wandering by deafault
        center = transform.position;
        rotation = transform.rotation;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(WanderRoutine());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TransformDataList list = other.GetComponent<TransformDataList>();
            players.Add(list);
        }
    }

    public void RemovePlayer(TransformDataList player)
    {
        players.Remove(player);
    }
    #endregion

    #region Routines

    private IEnumerator WanderRoutine()
    {
        // Set wander to true, turn of attacking and drifitng
        isWandering = true;
        isAttacking = false;
        isDrifting = false;

        // Wandering Logic
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
        // Turn on drifting, Turn of wandering is wandering is true,
        isDrifting = true;
        isWandering = false;

        // If not attacking, Start attacking
        if (isAttacking == false)
        {
            StartCoroutine(AttackRoutine());
        }
    
        // Drift Logic
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

            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator AttackRoutine()
    {
        // Start attacking
        isAttacking = true;

        // Attack Logic
        while (isAttacking)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            StartCoroutine(Attack());
        }
    }

    #endregion

    #region Actions

    public void StartWander()
    {
        // Start Wander Routine
        StartCoroutine(WanderRoutine());
    }

    public void StartEndIdle()
    {
        // Start the end of idle
        animator.SetTrigger("suprise");
        StartCoroutine(EndIdle());

    }

    public IEnumerator EndIdle()
    {
        // Reset Position to center
        agent.updateRotation = false;
        agent.SetDestination(center);

        Walk();

        // Wait for ghost to return to center
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        Idle();

        // Start Drift Routine if not already
        if (isDrifting == false)
        {
            StartCoroutine(DriftRoutine());
        }
    }
    public IEnumerator Attack()
    {
        // Select available target throwable
        ObjectBehaviour newSelectedObject = SelectObject();
        
        // If object  is selected, throw at target
        if (newSelectedObject != null)
        {
            // Set as thrown and throw
            newSelectedObject.thrown = true;
            GameObject throwable = newSelectedObject.gameObject;

            target = SelectTarget();

            Vector3 location = target.position;

            // Levitate Object (Add Animation for this in future)
            LevitateObject(throwable);

            // Wait for levitate to end
            yield return new WaitForSeconds(1);

            if (throwable != null) // Checks if object is destroyed in the 1 second it was levitated
            {
                animator.SetTrigger("attack");
                ThrowObject(throwable, location);
            }  
        }
    }

    public void ThrowObject(GameObject throwable, Vector3 location)
    {
        // Being Linear Throw to location
        throwManager.StartThrow(throwable, location, throwSpeed);
    }
    public void LevitateObject(GameObject throwable)
    {
        //Start attack indicator
        indicatorManager.StartIndicator();
        //Activate glow outline
        MeshRenderer[] glow = throwable.GetComponentsInChildren<MeshRenderer>();
        for (int i=0; i < glow.Length; i++)
        {
            glow[i].enabled = true;
        }
        // Hover Object for attack anticipation
        Vector3 location = throwable.transform.position + new Vector3(0, levitationHeight, 0);
        throwManager.StartThrow(throwable, location, levitateSpeed);
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
        animator.SetTrigger("appear");
    }

    public void TakeDamage()
    {
        animator.SetTrigger("damage");
    }

    public void UpdateStrength()
    {
        if (strengthCheckpoints.Count <= 0) { return; }

        for (int i = 0; i < strengthCheckpoints.Count; i++)
        {
            if (health.value < strengthCheckpoints[i])
            {
                strength = i;
            }
        }
    }

    #endregion

    #region Helper Functions

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

    public ObjectBehaviour SelectObject()
    {
        List<ObjectBehaviour> allObjects = objectManager.spawnedObjects;
        allObjects = filterByStrength(allObjects);
        allObjects = filterByThrown(allObjects);
        if (allObjects.Count <= 0) { return null; }

        ObjectBehaviour selectedObject = allObjects[Random.Range(0, allObjects.Count)];

        return selectedObject;
    }

    public List<ObjectBehaviour> filterByThrown(List<ObjectBehaviour> objectList)
    {
        List<ObjectBehaviour> newList = new List<ObjectBehaviour>();

        foreach (ObjectBehaviour obj in objectList)
        {
            if (!obj.thrown)
            {
                newList.Add(obj);
            }
        }

        return newList;
    }

    public List<ObjectBehaviour> filterByStrength(List<ObjectBehaviour> objectList)
    {
        List<ObjectBehaviour> newList = new List<ObjectBehaviour>();

        foreach (ObjectBehaviour obj in objectList)
        {
            if ((int)obj.size <= strength)
            {
                newList.Add(obj);
            }
        }

        return newList;
    }

    #endregion

    public Transform SelectTarget()
    {
        int playerIndex = Random.Range(0, players.Count);
        int posIndex = Random.Range(0, players[playerIndex].posList.Count);
        
        return players[playerIndex].posList[posIndex];
    }
}
