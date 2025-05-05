using UnityEngine;
using UnityEngine.AI;

public class MovePlayerInput : MonoBehaviour
{
    public Transform target; // The target object the animal will move towards
    private NavMeshAgent agent; // Reference to the NavMeshAgent component

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent attached to this GameObject
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position); // Set the destination to the target's position
        }
    }

    // Function to change the animal's target dynamically
    public void SetNewTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
