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
        // �G�� ID
        public int ID { get; private set; } = -1;

        public void Init()
        {
            this.OnTriggerEnter2DAsObservable().Subscribe(col =>
            {
                this.OnHit(col);
            });
        }


        public void OnUpdate()
        {
            
        }



        private void OnHit(Collider2D col)
        {
            TagName tag = (TagName)Enum.Parse(typeof(TagName), $"{col.tag}");

            switch (tag)
            {
                case TagName.PlayerBullet:
                    // TODO �����̃_���[�W�v�Z GetComponent �Ŏ��̏d������ GameConfig �݂����Ȃ��������@�l���������ǂ��H
                    float damage = col.gameObject.GetComponent<BulletController>().Damage;
                    break;
            }
        }
    }
}