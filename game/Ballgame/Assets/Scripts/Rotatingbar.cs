using Unity.Mathematics;
using UnityEngine;

public class Rotatingbar : MonoBehaviour
{
    public Transform trans;
    public float speed;
    Vector3 rotation;
    private void Update()
    {
        trans.rotation = quaternion.Euler(rotation);
        rotation.z = rotation.z + 1 * speed * Time.deltaTime;
    }
}
