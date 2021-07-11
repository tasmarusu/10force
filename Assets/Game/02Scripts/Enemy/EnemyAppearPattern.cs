/* *************************************************
* EnemyAppearPattern 敵の出現パターンを決める
************************************************* */
namespace MainForce
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;

    public class EnemyAppearPattern : MonoBehaviour
    {
        [field: SerializeField] public List<Order> Orders { get; private set; } = new List<Order>();

        private EnemyManager enemyManager = null;
        private IDisposable timeDisposable = null;

        // 敵データ
        [System.Serializable]
        public class Order
        {
            // 敵の種類(Prefab)
            [field: SerializeField] public EnemyController enemyPrefab { get; private set; } = null;
            // 敵の種類(Object) 開始座標と角度はこれ参照する
            [field: SerializeField] public EnemyController enemyObj { get; set; } = null;
            // 出現する秒数
            [field: SerializeField] public float timer { get; set; } = 0.0f;
            // 強化アイテムを落とすかどうか
            [field: SerializeField] public bool isDrop { get; set; } = false;

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
                this.enemyPrefab = enemy;
            }
        }



        /// <summary>
        /// 初期化
        /// </summary>
        public void Init(EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;
            for (int i = 0; i < this.Orders.Count; i++)
            {
                this.Orders[i].enemyObj.Init(enemyManager.Model.Data[i]);
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void OnUpdate()
        {
            for (int i = 0; i < this.Orders.Count; i++)
            {
                EnemyModel.DataConfig data = this.enemyManager.Model.Data[i];

                // この敵が出現したかどうか
                if (data.State == EnemyModel.StateConfig.Arrive)
                {
                    this.Orders[i].enemyObj.OnUpdate();
                }
                // まだ出現していない
                else if (data.State == EnemyModel.StateConfig.Wait)
                {
                    // 時間が来ていればアクティブ化にする
                    if (this.enemyManager.Time.ProgressTime >= this.Orders[i].timer)
                    {
                        data.Appear();
                        this.Orders[i].enemyObj.OnStart();
                    }
                }
            }
        }


        /// <summary>
        /// 開始する時に呼ばれる
        /// </summary>
        public void OnStart()
        {

        }



        /// <summary>
        /// タイマーを開始
        /// </summary>
        private void StartTimer()
        {
            this.timeDisposable = Observable.EveryUpdate().Subscribe(_ =>
            {

            });
        }
    }
}


