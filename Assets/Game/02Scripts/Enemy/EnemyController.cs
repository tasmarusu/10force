/* *************************************************
* EnemyController �G�̃R���g���[���[
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;
    using UniRx.Triggers;
    using System;

    public class EnemyController : MonoBehaviour
    {
        // �G���g�������Ă�^�C�v
        [field: SerializeField] public GameConfig.EnemyConfig.Type TypeID { get; private set; } = GameConfig.EnemyConfig.Type.None;

        private EnemyModel.DataConfig model = null;


        /// <summary>
        /// ������
        /// </summary>
        /// <param name="model"> ���̃��f���̃f�[�^ </param>
        public void Init(EnemyModel.DataConfig model)
        {
            this.model = model;

            this.OnTriggerEnter2DAsObservable().Subscribe(col =>
            {
                this.OnHit(col);
            });

            this.gameObject.SetActive(false);
        }

        
        /// <summary>
        /// �����n�߂鎞�ɌĂ΂��
        /// </summary>
        public void OnStart()
        {
            this.gameObject.SetActive(true);
        }


        public void OnUpdate()
        {
            this.model.Pos = this.transform.position;
        }



        private void OnHit(Collider2D col)
        {
            TagName tag = (TagName)Enum.Parse(typeof(TagName), $"{col.tag}");

            switch (tag)
            {
                case TagName.PlayerBullet:
                    // TODO �����̃_���[�W�v�Z GetComponent �Ŏ��̏d������ GameConfig �݂����Ȃ��������@�l���������ǂ��H
                    float damage = col.gameObject.GetComponent<BulletController>().Damage;
                    this.model.Damage(10);

                    // HP�������Ȃ����玀��
                    this.Des();

                    break;
            }
        }



        /// <summary>
        /// HP�������Ȃ���
        /// </summary>
        private void Des()
        {
            if (this.model.State == EnemyModel.StateConfig.Des)
            {
                gameObject.SetActive(false);
            }
        }
    }
}