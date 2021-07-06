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


        public EnemyModel Model { get; private set; } = null;


        public void Init(TimeManager time)
        {
            this.time = time;

            // 敵の出現パターンを取得する
            // TODO 今は敵普通に出してる
            this.Model = new EnemyModel();
            this.Model.SetData();
            this.enemy.Init(this.Model);
        }


        public void OnUpdate()
        {
            enemy.OnUpdate();
        }
    }
}