
using UnityEngine;

public class Flippable : MonoBehaviour
{
    public GameObject Object3D;
    public GameObject Object2D;

    public void Flip(bool flipTo3D)
    {
        Object3D.SetActive(flipTo3D);
        Object2D.SetActive(!flipTo3D);

        if (flipTo3D)
        {
            Vector3 newPos = new Vector3(Object2D.transform.position.x, Object2D.transform.position.y, Object3D.transform.position.z);
            Object3D.transform.position = newPos;
        }
        else
        {
            Vector2 newPos = new Vector2(Object3D.transform.position.x, Object3D.transform.position.y);
            Object2D.transform.position = newPos;
        }


    }
}

