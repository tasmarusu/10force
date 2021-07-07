/* *************************************************
* EnemyAppearPattern 敵の出現パターンを決める
************************************************* */
namespace MainForce
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemyAppearPattern : MonoBehaviour
    {
        [field: SerializeField] public List<EnemyController> UseEnemys { get; private set; } = new List<EnemyController>();

        [field: SerializeField] public List<Order> Orders { get; private set; } = new List<Order>();

        [System.Serializable]
        public class Order
        {
            // 敵の種類
            [field: SerializeField] public EnemyController enemy { get; private set; } = null;
            // 出現する秒数
            [field: SerializeField] public float timer { get; private set; } = 0.0f;
            // 強化アイテムを落とすかどうか
            [field: SerializeField] public bool isDrop { get; private set; } = false;
            // 開始座標
            [field: SerializeField] public Vector2 pos { get; private set; } = Vector2.zero;
            // 開始角度
            [field: SerializeField] public float rotate { get; private set; } = 0.0f;

            /// <summary>
            /// Order のデータを設定する
            /// </summary>
            public void SetData(float timer, bool isDrop)
            {
                this.timer = timer;
                this.isDrop = isDrop;
            }

            public void SetEnemy(EnemyController enemy)
            {
                this.enemy = enemy;
            }
        }


        private void OnStart()
        {
            
        }
    }
}


