using UnityEngine;


namespace Gamekit2D
{
    public class Recharger : MonoBehaviour
    {
        #region Fields

        [SerializeField] private LayerMask layers;

        #endregion

        #region UnityMethods


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (layers.Contains(collision.gameObject))
            {
                TimeJumpSkill skill = collision.GetComponent<TimeJumpSkill>();

                if (skill != null)
                {
                    skill.Rechage();
                }

            }
        }

        #endregion
    }
}