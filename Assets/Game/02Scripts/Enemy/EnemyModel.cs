/* *************************************************
* PlayerModel ÉvÉåÉCÉÑÅ[ÇÃèÓïÒÇï€éù
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
            public int ID = -1;
            public int NowHP = 0;
            public int MaxHP = 0;
            public bool IsArrive = true;
            public bool IsAppear = false;

            public DataConfig(int ID, int HP)
            {
                this.ID = ID;
                this.NowHP = HP;
                this.MaxHP = HP;
            }
        }


        public void SetData()
        {
            this.Data = new DataConfig[1] { new DataConfig(1, 100) };
        }


        public void Appear(int ID)
        {
            this.Data[0].IsAppear = true;
        }


        /// <summary>
        /// ID ÇÃìGÇ… damage Çó^Ç¶ÇÈ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="damage"></param>
        public void Damage(int ID, int damage)
        {
            this.Data[0].NowHP -= damage;
        }
    }
}