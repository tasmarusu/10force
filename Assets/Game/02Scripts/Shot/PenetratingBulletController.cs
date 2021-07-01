/* *************************************************
* StageGroup 1�̃X�e�[�W�̌`�S�Ă��Ǘ�
*            ���̎q���� StageController ���t���Ă���
************************************************* */
namespace MainForce
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;
    using UniRx.Triggers;

    public class PenetratingBulletController : BulletController
    {
        private readonly Subject<Unit> finishedSubject = new Subject<Unit>();

        // �I�u�W�F�N�g���g���I���������ʒm����
        public IObservable<Unit> OnFinishedAsync => this.finishedSubject.Take(1);


        /* *************************************************
        * ������
        ************************************************* */
        public void Init(Vector2 initPos, Quaternion initRotate)
        {
            // �����蔻��J�n
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(collider2D =>
                {
                    this.OnHit(collider2D);
                });

            // Update �̊J�n
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {

                });
        }


        /* *************************************************
        * �����蔻��
        ************************************************* */
        private void OnHit(Collider2D collider)
        {

        }


        /* *************************************************
        * �����鎞�Ɏ��s
        ************************************************* */
        private void OnFinish()
        {
            this.finishedSubject.OnNext(Unit.Default);
        }


        private void OnDestroy()
        {
            this.finishedSubject.Dispose();
        }
    }
}