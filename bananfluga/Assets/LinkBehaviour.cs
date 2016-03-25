using UnityEngine;
using System.Collections;

public class LinkBehaviour : MonoBehaviour
{

    public GameObject obj0;
    public GameObject obj1;
    public float MinDistance = 5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var start = obj0.transform.position;
        var end = obj1.transform.position;
        var offset = end - start;

        if (offset.sqrMagnitude > MinDistance * MinDistance)
        {
            GetComponent<MeshRenderer>().enabled = false;
            return;
        }

        GetComponent<MeshRenderer>().enabled = true;
        var scale = new Vector3(0.5f, offset.magnitude / 2.0f, 0.5f);
        var newPosition = start + Vector3.Scale(offset, new Vector3(0.5f, 0.5f, 0.5f));
        transform.position = newPosition;
        transform.up = offset;
        transform.localScale = scale;

        RaycastHit rayCastHit = new RaycastHit();
        if (Physics.SphereCast(start, obj0.transform.localScale.x / 2,offset, out rayCastHit, offset.magnitude))
        {
            var gameObj = rayCastHit.transform.gameObject;
            if (gameObj.tag == "Enemy")
            {
                Destroy(gameObj);
                obj0.transform.localScale = obj0.transform.localScale * 1.10f;
                obj1.transform.localScale = obj1.transform.localScale * 1.10f;
                MinDistance *= 2;
                GetComponent<CapsuleCollider>().radius *= 2;    
            }
        }
    }
}
