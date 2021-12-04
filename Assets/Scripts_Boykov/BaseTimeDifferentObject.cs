using UnityEngine;


namespace Gamekit2D
{
    public abstract class BaseTimeDifferentObject : MonoBehaviour, ITimeDifferentObject
    {
        public abstract void SwitchTimeState(TimeState newState);
    }
}