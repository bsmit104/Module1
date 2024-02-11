//tried uploading pixel art for billboarding, didnt fully work, but thought id share
using UnityEngine;

public class SyncPosition : MonoBehaviour
{
    public Transform target3D;

    void Update()
    {
        transform.position = new Vector3(target3D.position.x, target3D.position.y, transform.position.z);
    }
}
