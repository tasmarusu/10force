/* *************************************************
* EnemyAppearPattern �G�̏o���p�^�[�������߂�
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

        // �G�f�[�^
        [System.Serializable]
        public class Order
        {
            // �G�̎��(Prefab)
            [field: SerializeField] public EnemyController enemyPrefab { get; private set; } = null;
            // �G�̎��(Object) �J�n���W�Ɗp�x�͂���Q�Ƃ���
            [field: SerializeField] public EnemyController enemyObj { get; set; } = null;
            // �o������b��
            [field: SerializeField] public float timer { get; set; } = 0.0f;
            // �����A�C�e���𗎂Ƃ����ǂ���
            [field: SerializeField] public bool isDrop { get; set; } = false;

            /// <summary>
            /// Order �̃f�[�^��ݒ肷��
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
        /// ������
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
        /// �X�V����
        /// </summary>
        public void OnUpdate()
        {
            for (int i = 0; i < this.Orders.Count; i++)
            {
                EnemyModel.DataConfig data = this.enemyManager.Model.Data[i];

                // ���̓G���o���������ǂ���
                if (data.State == EnemyModel.StateConfig.Arrive)
                {
                    this.Orders[i].enemyObj.OnUpdate();
                }
                // �܂��o�����Ă��Ȃ�
                else if (data.State == EnemyModel.StateConfig.Wait)
                {
                    // ���Ԃ����Ă���΃A�N�e�B�u���ɂ���
                    if (this.enemyManager.Time.ProgressTime >= this.Orders[i].timer)
                    {
                        data.Appear();
                        this.Orders[i].enemyObj.OnStart();
                    }
                }
            }
        }


        /// <summary>
        /// �J�n���鎞�ɌĂ΂��
        /// </summary>
        public void OnStart()
        {

        }



        /// <summary>
        /// �^�C�}�[���J�n
        /// </summary>
        private void StartTimer()
        {
            this.timeDisposable = Observable.EveryUpdate().Subscribe(_ =>
            {

            });
        }
    }
}


