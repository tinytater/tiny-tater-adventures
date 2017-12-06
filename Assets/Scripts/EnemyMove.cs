using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    public int deltaX;
    [SerializeField]
    public int deltaY;
    private Vector3 pointB;


    IEnumerator Start()
    {
        var pointA = transform.position;
        var pointB = new Vector3(pointA.x+deltaX , pointA.y+deltaY, 0);
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
            transform.Rotate(0, 180, 0);
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
            transform.Rotate(0, 180, 0);

        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
