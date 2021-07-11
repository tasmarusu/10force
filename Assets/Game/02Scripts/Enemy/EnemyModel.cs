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


        /// ステート
        public enum StateConfig
        {
            /// <summary>
            /// 出現していなく、待機状態
            /// </summary>
            Wait = 0,
            /// <summary>
            /// 出現して、生存している
            /// </summary>
            Arrive = 5,
            /// <summary>
            /// 死亡
            /// </summary>
            Des = 10,
        }

        [System.Serializable]
        public class DataConfig
        {
            public int ID = -1;             // ID(EnemyConfig.Typeではない)
            public int NowHP = 0;           // 現在のHP
            public int MaxHP = 0;           // 最大HP
            public Vector2 Pos = Vector2.zero;  // 座標
            public StateConfig State { get; private set; } = StateConfig.Wait;

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

                // 死亡
                if (this.NowHP <= 0.0f)
                {
                    this.State = StateConfig.Des;
                }
            }


            /// <summary>
            /// 出現
            /// </summary>
            public void Appear()
            {
                this.State = StateConfig.Arrive;
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



        /// <summary>
        /// アクティブな敵の数
        /// </summary>
        /// <returns></returns>
        public int GetActiveCount()
        {
            int count = 0;

            for (int i = 0; i < this.Data.Length; i++)
            {
                if (this.Data[i].State == EnemyModel.StateConfig.Arrive)
                {
                    count++;
                }
            }

            return count;
        }



    }
}