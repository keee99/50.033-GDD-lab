using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{

    public GameObject enemy;

    public void Respawn()
    {
        enemy.SetActive(true);
    }
}
