using UnityEngine;
using System.Collections;

public class TopDownCamera : MonoBehaviour {

    GameObject ball1, ball2;
    public float cameraSpeed = 2.0f;
    public GameObject ground;
    public float heightFactor = 4.0f;

	// Use this for initialization
	void Start () {
        ball1 = GameObject.FindGameObjectsWithTag("Player")[0];
        ball2 = GameObject.FindGameObjectsWithTag("Player")[1];
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 ballCenter = (ball1.transform.position + ball2.transform.position)/2.0f;
        ballCenter.y = ground.GetComponent<MeshCollider>().bounds.max.y + Vector3.Distance(ball1.transform.position, ball2.transform.position) * Mathf.Log(heightFactor);
        transform.position = Vector3.Lerp(transform.position, ballCenter, Time.deltaTime * cameraSpeed);
	}
}
