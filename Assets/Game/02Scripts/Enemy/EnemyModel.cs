/* *************************************************
* PlayerModel プレイヤーの情報を保持
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;

    public class EnemyModel
    {
        public DataConfig[] Data { get; private set; } = null;

        [System.Serializable]
        public class DataConfig
        {
            public int ID = -1;             // ID(EnemyConfig.Typeではない)
            public int NowHP = 0;           // 現在のHP
            public int MaxHP = 0;           // 最大HP
            public bool IsArrive = true;    // 生きているか(NowHP>0かどうか)
            public bool IsAppeard = false;  // 出現したかどうか

            public DataConfig(int ID, int HP)
            {
                this.ID = ID;
                this.NowHP = HP;
                this.MaxHP = HP;
            }


            /// <summary>
            /// ID の敵に damage を与える
            /// </summary>
            /// <param name="ID"></param>
            /// <param name="damage"></param>
            public void Damage(int damage)
            {
                this.NowHP -= damage;
            }
        }


        /// <summary>
        /// データを生成して渡す
        /// </summary>
        /// <param name="count"> 敵の数 </param>
        /// <param name="HPs"> 敵のHP </param>
        public EnemyModel(IList<EnemyAppearPattern.Order> roOrders)
        {
            this.Data = new DataConfig[roOrders.Count];
            for (int i = 0; i < this.Data.Length; i++)
            {
                this.Data[i] = new DataConfig((i + 1), GameConfig.Instance.Enemy.GetStartHP(roOrders[i].enemyObj.TypeID));
            }
        }


        public void Appear(int ID)
        {
            this.Data[0].IsAppeard = true;
        }



    }
}