using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyLocations enemyLocations;
    public IntVariable currentWorld;

    public GameObject goombaPrefab;

    public Transform endLimit;
    public Transform startLimit;

    public void ResetEnemies()
    {
        // foreach (Transform child in transform)
        // {
        //     child.GetChild(0).GetComponentInChildren<EnemyRbMovement>().Reset();
        //     child.GetChild(0).GetComponentInChildren<EnemyDeath>().Reset();
        // }
        SpawnEnemies(false);
    }

    void Awake()
    {
        GameManager.Instance.gameRestart.AddListener(ResetEnemies);
        GameManager.Instance.gameStart.AddListener(SpawnEnemies);

    }



    public void SpawnEnemies()
    {
        Debug.Log("Spawn");
        SpawnEnemies(true);
    }

    public void SpawnEnemies(bool incre)
    {
        if (enemyLocations.goombaLocations.Length == 0 || currentWorld.Value <= 2)
        {
            InitEnemies();
        }
        else if (incre && enemyLocations.goombaLocations.Length != currentWorld.Value)
        {
            Debug.Log("Increment Enemies!");
            int increment = currentWorld.Value;
            AddEnemyLocations(increment);
        }

        // Delete all children
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }


        for (int i = 0; i < enemyLocations.goombaLocations.Length; i++)
        {
            GameObject goomba = Instantiate(goombaPrefab, this.transform);
            goomba.transform.position = enemyLocations.goombaLocations[i];
        }
    }


    private void InitEnemies()
    {
        // Fill enemy locations with children's transform positions
        enemyLocations.goombaLocations = new Vector2[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            enemyLocations.goombaLocations[i] = transform.GetChild(i).position;
        }
    }

    private void AddEnemyLocations(int increment)
    {
        Vector2[] newGoombaLocations = new Vector2[enemyLocations.goombaLocations.Length + increment];

        CopyEnemies(newGoombaLocations);

        for (int i = enemyLocations.goombaLocations.Length; i < newGoombaLocations.Length; i++)
        {
            newGoombaLocations[i] = new Vector2(GetXCoord(), GetYCoord());
        }
        // newGoombaLocations[newGoombaLocations.Length - 1] = new Vector2(GetXCoord(), GetYCoord());

        enemyLocations.goombaLocations = newGoombaLocations;
    }

    private void CopyEnemies(Vector2[] populated)
    {

        for (int i = 0; i < enemyLocations.goombaLocations.Length; i++)
        {
            populated[i] = enemyLocations.goombaLocations[i];
        }
    }

    private float GetXCoord() => Random.Range(startLimit.position.x, endLimit.position.x);
    private float GetYCoord() => Random.Range(startLimit.position.y, endLimit.position.y);

}
