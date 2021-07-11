/* *************************************************
* PlayerModel �v���C���[�̏���ێ�
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
            public int ID = -1;             // ID(EnemyConfig.Type�ł͂Ȃ�)
            public int NowHP = 0;           // ���݂�HP
            public int MaxHP = 0;           // �ő�HP
            public bool IsArrive = true;    // �����Ă��邩(NowHP>0���ǂ���)
            public bool IsAppeard = false;  // �o���������ǂ���

            public DataConfig(int ID, int HP)
            {
                this.ID = ID;
                this.NowHP = HP;
                this.MaxHP = HP;
            }


            /// <summary>
            /// ID �̓G�� damage ��^����
            /// </summary>
            /// <param name="ID"></param>
            /// <param name="damage"></param>
            public void Damage(int damage)
            {
                this.NowHP -= damage;
            }
        }


        /// <summary>
        /// �f�[�^�𐶐����ēn��
        /// </summary>
        /// <param name="count"> �G�̐� </param>
        /// <param name="HPs"> �G��HP </param>
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