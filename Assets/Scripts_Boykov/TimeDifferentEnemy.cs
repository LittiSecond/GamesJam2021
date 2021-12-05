using UnityEngine;


namespace Gamekit2D
{
    public class TimeDifferentEnemy : BaseTimeDifferentObject
    {
        #region Fields

        [SerializeField] private DamagableTimeDifferen _damagable;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            TimeJumpSceneManager tsm = TimeJumpSceneManager.Instance;
            if (tsm != null)
            {
                tsm.Add(this);
            }
        }

        private void OnDisable()
        {
            TimeJumpSceneManager tsm = TimeJumpSceneManager.Instance;
            if (tsm != null)
            {
                tsm.Remove(this);
            }
        }

        #endregion


        #region ITimeDifferentObject

        public override void SwitchTimeState(TimeState newState)
        {
            _damagable.SwitchTimeState(newState);
        }

        #endregion

    }
}