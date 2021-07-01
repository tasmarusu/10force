/* *************************************************
* PenetratingBulletGroup �ђʒe�̃v�[��
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PenetratingBulletGroup : MonoBehaviour
    {
        private List<PenetratingBulletController> controllers = new List<PenetratingBulletController>();


        /// <summary>
        /// �A�N�e�B�u�����Ă��Ȃ� �ђʒe �� Pool ����擾
        /// </summary>
        /// <returns></returns>
        public PenetratingBulletController GetNotActiveControllerInPool()
        {
            for (int i = 0; i < controllers.Count; i++)
            {
                if (controllers[i].gameObject.activeSelf == false)
                {
                    return controllers[i];
                }
            }

            return null;
        }
    }
}