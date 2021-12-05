using UnityEngine;


namespace Gamekit2D
{
    public class SimpleTimeDifferentObstacle : BaseTimeDifferentObject
    {
        #region Fields

        [SerializeField] private bool _isExistInNormalTime;

        private TimeState _state;

        #endregion


        #region IInterfaces

        public override void SwitchTimeState(TimeState newState)
        {
            if (_state != newState)
            {
                if (newState == TimeState.Anomalous)
                {
                    gameObject.SetActive(!_isExistInNormalTime);
                }
                else
                {
                    gameObject.SetActive(_isExistInNormalTime);
                }

                _state = newState;
            }
        }

        #endregion

    }
}