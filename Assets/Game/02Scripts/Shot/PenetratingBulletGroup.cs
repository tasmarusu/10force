/* *************************************************
* PenetratingBulletGroup 貫通弾のプール
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
        /// アクティブ化していない 貫通弾 を Pool から取得
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