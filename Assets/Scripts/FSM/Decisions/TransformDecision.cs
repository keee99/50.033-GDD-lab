
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "PluggableSM/Decisions/Transform")]
public class TransformDecision : Decision
{
    public StateTransformMap[] map;

    public override bool Decide(StateController controller)
    {
        MarioStateController m = (MarioStateController)controller;
        // we assume that the state is named (string matched) after one of possible values in MarioState
        // convert between current state name into MarioState enum value using custom class EnumExtension
        // you are free to modify this to your own use
        MarioState toCompareState = EnumExtension.ParseEnum<MarioState>(m.currentState.name);

        // loop through state transform and see if it matches the current transformation we are looking for
        for (int i = 0; i < map.Length; i++)
        {
            if (toCompareState == map[i].fromState && m.currentPowerupType == map[i].powerupCollected)
            {
                return true;
            }
        }

        return false;

    }
}

[System.Serializable]
public struct StateTransformMap
{
    public MarioState fromState;
    public PowerupType powerupCollected;
}



public static class EnumExtension
{
    public static T ParseEnum<T>(string input) where T : struct
    {
        // return the enum value given a string
        T enumValue;
        if (Enum.TryParse(input, true, out enumValue))
        {
            return enumValue;
        }
        return default(T);
    }

    public static int ParseEnumToInteger<T>(string input) where T : struct
    {
        // return the integer enum given a string
        T enumValue = ParseEnum<T>(input);
        if (Enum.IsDefined(typeof(T), enumValue))
        {
            return Convert.ToInt32(enumValue);
        }
        return -1;
    }

}
