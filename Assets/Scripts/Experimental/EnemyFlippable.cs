using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlippable : Flippable
{

    private EnemyMovement3D enemyMovement3D;
    private EnemyMovement enemyMovement;

    private void Start()
    {
        enemyMovement3D = Object3D.GetComponent<EnemyMovement3D>();
        enemyMovement = Object2D.GetComponent<EnemyMovement>();
    }
    public new void Flip(bool flipTo3D)
    {
        if ((flipTo3D && enemyMovement.isActiveAndEnabled == false) || (!flipTo3D && enemyMovement3D.isActiveAndEnabled == false))
        {
            return;
        }
        base.Flip(flipTo3D);

        if (flipTo3D)
        {
            enemyMovement3D.moveRightState = enemyMovement.moveRightState;
            enemyMovement3D.ComputeVelocity();
        }
        else
        {
            enemyMovement.moveRightState = enemyMovement3D.moveRightState;
            enemyMovement.ComputeVelocity();
        }



    }
}
