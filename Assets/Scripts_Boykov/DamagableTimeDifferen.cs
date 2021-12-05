namespace Gamekit2D
{
    public class DamagableTimeDifferen : Damageable
    {
        #region Fields

        private const float TIME_SWITCH_HEALTH_MULTIPLER = 1.0f/3.0f;

        private TimeState _state = TimeState.Normal;

        #endregion


        #region Methods

        public void SwitchTimeState(TimeState newState)
        {
            if (newState != _state)
            {
                if (newState == TimeState.Anomalous)
                {
                    m_CurrentHealth = (int)(m_CurrentHealth * TIME_SWITCH_HEALTH_MULTIPLER);
                    if (m_CurrentHealth == 0)
                    {
                        m_CurrentHealth = 1;
                    }
                }
                else
                {
                    m_CurrentHealth = startingHealth;
                }
                _state = newState;
            }
        }

        #endregion
    }
}