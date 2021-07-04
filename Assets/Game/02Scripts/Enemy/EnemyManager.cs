/* *************************************************
* EnemyManager �G�̃}�l�[�W���[
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

        public void Init(TimeManager time)
        {
            this.time = time;

            // �G�̏o���p�^�[�����擾����
            // TODO ���͓G���ʂɏo���Ă�
            enemy.Init();
        }


        public void OnUpdate()
        {
            enemy.OnUpdate();
        }
    }
}