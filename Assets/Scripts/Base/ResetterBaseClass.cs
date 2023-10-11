using UnityEngine;

public interface IResetter
{
    void Reset();

    void ResetSingle(Resettable resettable);

}

public abstract class ResetterBaseClass : MonoBehaviour, IResetter
{
    public virtual void Reset()
    {
        foreach (Resettable obj in GetComponentsInChildren<Resettable>())
        {
            ResetSingle(obj);
        }
    }

    public virtual void ResetSingle(Resettable resettable)
    {
        resettable.Reset();
    }

    private void Awake()
    {
        GameManager.Instance.gameRestart.AddListener(Reset);
    }
}



public abstract class Resettable : MonoBehaviour
{
    public abstract void Reset();

}