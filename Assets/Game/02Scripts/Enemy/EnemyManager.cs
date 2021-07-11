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
        [SerializeField] private EnemyAppearPattern[] enemyPatterns = null;


        private EnemyAppearPattern useEnemys = null;

        public EnemyModel Model { get; private set; } = null;
        public TimeManager Time { get; private set; } = null;


        public void Init(TimeManager time, int stageNum)
        {
            this.Time = time;

            // 敵の出現パターンを取得する
            // TODO 今は敵普通に出してる

            this.useEnemys = Instantiate(this.enemyPatterns[stageNum], this.transform);
            IList<EnemyAppearPattern.Order> roOrders = this.useEnemys.Orders.AsReadOnly();
            this.Model = new EnemyModel(roOrders);
            this.useEnemys.Init(this);
        }


        public void OnUpdate()
        {
            this.useEnemys.OnUpdate();
        }
    }
}