using UnityEngine;

public class CatSpin : MonoBehaviour
{
    public float spinSpeed;

    private void Update()
    {
        transform.Rotate(0, 0, spinSpeed);
    }
}
