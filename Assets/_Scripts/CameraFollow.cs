using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]Transform target;
    [SerializeField]float smoothTime = 0.5f;
    void LateUpdate()
    {
        Vector2 desiredPosition = new (target.position.x, target.position.y);
        Vector2 position = Vector2.Lerp(transform.position, desiredPosition, smoothTime * Time.deltaTime);
        transform.position = new (position.x,position.y,transform.position.z);
    }
}
