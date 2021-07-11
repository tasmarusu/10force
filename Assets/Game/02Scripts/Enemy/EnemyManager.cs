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
        [SerializeField] private EnemyAppearPattern[] enemyPatterns = null;


        private EnemyAppearPattern useEnemys = null;

        public EnemyModel Model { get; private set; } = null;
        public TimeManager Time { get; private set; } = null;


        public void Init(TimeManager time, int stageNum)
        {
            this.Time = time;

            // �G�̏o���p�^�[�����擾����
            // TODO ���͓G���ʂɏo���Ă�

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