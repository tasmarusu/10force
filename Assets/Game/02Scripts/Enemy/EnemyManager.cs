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
        [SerializeField] private EnemyController enemy = null;
        private TimeManager time;

        public void Init(TimeManager time)
        {
            this.time = time;

            // 敵の出現パターンを取得する
            // TODO 今は敵普通に出してる
            enemy.Init();
        }


        public void OnUpdate()
        {
            enemy.OnUpdate();
        }
    }
}