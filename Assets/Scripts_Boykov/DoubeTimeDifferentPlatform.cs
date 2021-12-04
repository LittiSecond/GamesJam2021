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

        private float _startSpeed;
        private TimeState _state;

        private bool _isInitialized;

        #endregion


        #region Methods

        private void Initialize()
        {
            _startSpeed = _mover.speed;
            _isInitialized = true;
        }

        #endregion


        #region IInterfaces

        public override void SwitchTimeState(TimeState newState)
        {
            if (!_isInitialized)
            {
                Initialize();
            }

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

                if (!(_moveInNormalTime ^ (newState == TimeState.Normal)))
                {
                    _mover.speed = _startSpeed;
                }
                else
                {
                    _mover.speed = 0.0f;
                }

                _state = newState;
            }
        }

        #endregion
    }
}