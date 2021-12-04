using UnityEngine;


namespace Gamekit2D
{
    public class DoubeTimeDifferentPlatform : BaseTimeDifferentObject
    {
        #region Fields

        [SerializeField] private GameObject[] _normalTimeObjects;
        [SerializeField] private GameObject[] _anomalousTimeObjects;
        [SerializeField] private MovingPlatform _mover;
        [SerializeField] private bool _moveInNormalTime;

        private TimeState _state;

        #endregion


        #region IInterfaces

        public override void SwitchTimeState(TimeState newState)
        {
            if (_state != newState)
            {
                for (int i = 0; i < _normalTimeObjects.Length; i++)
                {
                    _normalTimeObjects[i].SetActive( newState == TimeState.Normal );
                }

                for (int i = 0; i < _anomalousTimeObjects.Length; i++)
                {
                    _anomalousTimeObjects[i].SetActive(newState == TimeState.Anomalous);
                }

                _mover.enabled = !(_moveInNormalTime ^ (newState == TimeState.Normal));

                _state = newState;
            }
        }

        #endregion
    }
}