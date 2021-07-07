/* *************************************************
* EnemyAppearPattern �G�̏o���p�^�[�������߂�
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
            // �G�̎��
            [field: SerializeField] public EnemyController enemy { get; private set; } = null;
            // �o������b��
            [field: SerializeField] public float timer { get; private set; } = 0.0f;
            // �����A�C�e���𗎂Ƃ����ǂ���
            [field: SerializeField] public bool isDrop { get; private set; } = false;
            // �J�n���W
            [field: SerializeField] public Vector2 pos { get; private set; } = Vector2.zero;
            // �J�n�p�x
            [field: SerializeField] public float rotate { get; private set; } = 0.0f;

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
                this.enemy = enemy;
            }
        }


        private void OnStart()
        {
            
        }
    }
}


