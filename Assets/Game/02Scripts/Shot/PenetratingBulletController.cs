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

        private float speedTime = 0.0f;
        private IDisposable speedTimeDisposable = null;
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
            this.speedTime = 0.0f;

            // Update �̊J�n
            this.speedTimeDisposable = this.UpdateAsObservable()
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
                this.speedTimeDisposable.Dispose();
                this.finishedSubject.OnNext(Unit.Default);
            }
        }


        /* *************************************************
        * �����蔻��
        ************************************************* */
        private void Move()
        {
            //float speed = GameConfig.Instance.Player.GetType(GameConfig.PlayerShotType.Penetrating).speed;
            float param = GameConfig.Instance.Shot.Penetrating.SpeedParam;
            AnimationCurve curve = GameConfig.Instance.Shot.Penetrating.SpeedCurve;
            this.speedTime += Time.deltaTime;
            float speed = curve.Evaluate(this.speedTime) * param;

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