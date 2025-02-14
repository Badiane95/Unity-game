using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Void Event Channel", menuName = "Scriptable Objects/Events/Void Event Channel")]
public class VoidEventChannel : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void Raise()
    { if (OnEventRaised != null)
          OnEventRaised?.Invoke();
    }
}
