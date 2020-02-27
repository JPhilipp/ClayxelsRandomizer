using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    // Constantly rotates in a given direction.

    [SerializeField] Vector3 rotation = Vector3.zero;

    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }

    public void SetRotation(float x = 0f, float y = 0f, float z = 0f)
    {
        this.rotation = new Vector3(x, y, z);
    }
}
