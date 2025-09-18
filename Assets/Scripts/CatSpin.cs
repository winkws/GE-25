using UnityEngine;

public class CatSpin : MonoBehaviour
{
    public float spinSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, spinSpeed);
    }
}
