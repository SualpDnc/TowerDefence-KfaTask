using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        // Kamera hep karşısında olacak
        transform.forward = Camera.main.transform.forward;
    }
}