using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	Vector3 offset;

    [SerializeField]
    private float xMin;
    [SerializeField]
    private float xMax;

    [SerializeField]
    private float yMin;
    [SerializeField]
    private float yMax;

    private Transform target;

	void Start()
	{
        target = GameObject.Find("Player").transform;
		//offset = transform.position - player.transform.position;
	}

	void LateUpdate()
	{
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax),transform.position.z);
		//transform.position = player.transform.position + offset;
        //Debug.Log("Transform position " + transform.position);

    }
}
