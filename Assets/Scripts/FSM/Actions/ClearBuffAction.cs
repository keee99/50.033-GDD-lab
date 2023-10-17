using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/ClearBuffAction")]
public class ClearBuffAction : Action
{
    public override void Act(StateController controller)
    {
        BuffStateController m = (BuffStateController)controller;
        m.currentBuffType = BuffType.Default;
    }
}
