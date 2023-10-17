using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/BuffChange")]
public class BuffChangeDecision : Decision
{
    public BuffChangeMap[] map;

    public override bool Decide(StateController controller)
    {
        BuffStateController m = (BuffStateController)controller;
        // we assume that the state is named (string matched) after one of possible values in MarioState
        // convert between current state name into MarioState enum value using custom class EnumExtension
        // you are free to modify this to your own use
        BuffState toCompareState = EnumExtension.ParseEnum<BuffState>(m.currentState.name);

        // loop through state transform and see if it matches the current transformation we are looking for
        for (int i = 0; i < map.Length; i++)
        {
            Debug.Log(m.currentState.name);
            Debug.Log(toCompareState.ToString() + ' ' + map[i].fromState.ToString() + ' ' + m.currentBuffType + ' ' + map[i].buffCollected);
            if (toCompareState == map[i].fromState && m.currentBuffType == map[i].buffCollected)
            {

                Debug.Log("BUFF");
                return true;
            }
        }

        return false;

    }
}

[System.Serializable]
public struct BuffChangeMap
{
    public BuffState fromState;
    public BuffType buffCollected;
}
