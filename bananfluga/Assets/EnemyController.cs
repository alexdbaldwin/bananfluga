using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public enum AIState {
        Wander,
        Attack,
        Chase
    }

    Rigidbody rigidBody;
    BoxCollider boxCollider;
    AIState currentState = AIState.Wander;
    Vector3 targetLocation;
    float timeUntilChangeTarget = 0.0f;
    float targetSpeed = 4.0f;

    public float turnSpeed = 1.0f;
    public float changeTargetInterval = 2.0f;
    public float mergeRange = 0.05f;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

        //if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded))
        //{
        //    // always move along the camera forward as it is the direction that it being aimed at
        //    Vector3 desiredMove = cam.transform.forward * input.y + cam.transform.right * input.x;
        //    desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

        //    desiredMove.x = desiredMove.x * movementSettings.CurrentTargetSpeed;
        //    desiredMove.z = desiredMove.z * movementSettings.CurrentTargetSpeed;
        //    desiredMove.y = desiredMove.y * movementSettings.CurrentTargetSpeed;
        //    if (m_RigidBody.velocity.sqrMagnitude <
        //        (movementSettings.CurrentTargetSpeed * movementSettings.CurrentTargetSpeed))
        //    {
        //        m_RigidBody.AddForce(desiredMove * SlopeMultiplier(), ForceMode.Impulse);
        //    }
        //}

	
	// Update is called once per frame
	void FixedUpdate () {
        switch (currentState)
        {
            case AIState.Wander:
                Wander();
                break;
            case AIState.Attack:
                break;
            case AIState.Chase:
                break;
            default:
                break;
        }
        TransitionState();
    }

    void Wander() {
        timeUntilChangeTarget -= Time.deltaTime;
        if(timeUntilChangeTarget <= 0)
        {
            timeUntilChangeTarget += changeTargetInterval;
            PickRandomWanderTarget();
        }

        transform.forward = Vector3.Lerp(transform.forward, Vector3.Normalize(targetLocation - transform.position), turnSpeed * Time.deltaTime);

        rigidBody.velocity = transform.forward * targetSpeed;

        GameObject mergeTarget = GetNearestAlly(3f);
        if(mergeTarget != null)
        {
            //Debug.Log("SET PHASERS TO KILL");
            if (mergeTarget.transform.localScale.magnitude > transform.localScale.magnitude)
            {
                mergeTarget.GetComponent<EnemyController>().SizeUp();
                Destroy(gameObject);
            }
            else
            {
                Destroy(mergeTarget);
                SizeUp();
            }
        }
    }

    void PickRandomWanderTarget()
    {
        float wanderX = Random.Range(transform.position.x - 10.0f, transform.position.x + 10.0f);
        float wanderZ = Random.Range(transform.position.z - 10.0f, transform.position.z + 10.0f);
        targetLocation = new Vector3(wanderX, transform.position.y, wanderZ);
    }

    void Attack() {

    }

    void SizeUp() {
        transform.position += new Vector3(0, boxCollider.bounds.size.y/2.0f, 0);
        transform.localScale = transform.localScale * 2.0f;
        targetLocation.y = transform.position.y;

    }

    void Chase() {

    }

    void TransitionState()
    {
        switch (currentState)
        {
            case AIState.Wander:
                
                break;
            case AIState.Attack:
                break;
            case AIState.Chase:
                break;
            default:
                break;
        }
    }

    GameObject GetNearestAlly(float range) {
        float nearestDist = float.PositiveInfinity;
        GameObject nearest = null;
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float dist = Vector3.Distance(go.transform.position, transform.position);
            //Debug.Log(dist);
            if (dist < nearestDist && dist < range && go != gameObject)
            {
                nearestDist = dist;
                nearest = go;
            }
        }
        return nearest;
    }
}
