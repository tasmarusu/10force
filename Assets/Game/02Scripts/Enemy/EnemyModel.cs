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


        /// �X�e�[�g
        public enum StateConfig
        {
            /// <summary>
            /// �o�����Ă��Ȃ��A�ҋ@���
            /// </summary>
            Wait = 0,
            /// <summary>
            /// �o�����āA�������Ă���
            /// </summary>
            Arrive = 5,
            /// <summary>
            /// ���S
            /// </summary>
            Des = 10,
        }

        [System.Serializable]
        public class DataConfig
        {
            public int ID = -1;             // ID(EnemyConfig.Type�ł͂Ȃ�)
            public int NowHP = 0;           // ���݂�HP
            public int MaxHP = 0;           // �ő�HP
            public Vector2 Pos = Vector2.zero;  // ���W
            public StateConfig State { get; private set; } = StateConfig.Wait;

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

                // ���S
                if (this.NowHP <= 0.0f)
                {
                    this.State = StateConfig.Des;
                }
            }


            /// <summary>
            /// �o��
            /// </summary>
            public void Appear()
            {
                this.State = StateConfig.Arrive;
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



        /// <summary>
        /// �A�N�e�B�u�ȓG�̐�
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