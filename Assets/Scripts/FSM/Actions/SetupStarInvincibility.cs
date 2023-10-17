
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/SetupStarInvincibility")]
public class StarInvincibleAction : Action
{
    public override void Act(StateController controller)
    {
        BuffStateController m = (BuffStateController)controller;
        m.SetRendererEffect();
        m.SetAudioEffect();
        m.SetInvincibility();
    }
}
