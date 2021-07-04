/* *************************************************
* EnemyManager 敵のマネージャー
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemyManager : MonoBehaviour
    {

        private TimeManager time;

        public void Init(TimeManager time)
        {
            this.time = time;
        }


        public void OnUpdate()
        {

        }
    }
}