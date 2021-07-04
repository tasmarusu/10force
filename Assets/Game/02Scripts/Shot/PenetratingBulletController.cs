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
        [SerializeField] private SpriteRenderer render2D = null;

        private readonly Subject<Unit> finishedSubject = new Subject<Unit>();

        // �I�u�W�F�N�g���g���I���������ʒm����
        public IObservable<Unit> OnFinishedAsync => this.finishedSubject.Take(1);


        /* *************************************************
        * ������
        ************************************************* */
        public void Init(Vector2 initPos, Quaternion initRotate, Color initColor)
        {
            this.transform.position = initPos;
            this.transform.rotation = initRotate;
            this.render2D.color = initColor;

            // Update �̊J�n
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    this.Move();
                    this.OnHit();
                });
        }


        /* *************************************************
        * �����蔻��
        ************************************************* */
        private void OnHit()
        {
            // �n�`�̃G���A�O
            if (StageManager.Instance.IsStageOutPos(this.transform.position))
            {
                this.finishedSubject.OnNext(Unit.Default);
            }
        }


        /* *************************************************
        * �����蔻��
        ************************************************* */
        private void Move()
        {
            //float speed = GameConfig.Instance.Player.GetType(GameConfig.PlayerShotType.Penetrating).speed;
            float speed = GameConfig.Instance.Shot.Penetrating.speed;

            Vector2 pos = this.transform.position;
            pos += (Vector2)this.transform.up * speed;
            this.transform.position = pos;
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