using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Agent : MonoBehaviour
{
    public float _speedFactor = 1;
    public Queue<Tilemap> tilemap;

    public void SetPath(Queue<Tilemap> path)
    {
        tilemap = path;
        StopAllCoroutines();
        StartCoroutine(MoveAlongPath(path));
    }

    private IEnumerator MoveAlongPath(Queue<Tilemap> path)
    {
        yield return new WaitForSeconds(1);
        Vector3 lastPosition = transform.position;
        while (path.Count > 0)
        {
            Tilemap nextTile = path.Dequeue();
            float lerpVal = 0;
            transform.LookAt(nextTile.transform, Vector3.up);

            while (lerpVal < 1)
            {
                lerpVal += Time.deltaTime * _speedFactor;
                transform.position = Vector3.Lerp(lastPosition, nextTile.transform.position, lerpVal);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.5f / _speedFactor);
            lastPosition = nextTile.transform.position;
        }
    }

}
