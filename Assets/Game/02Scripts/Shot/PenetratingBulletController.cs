/* *************************************************
* StageGroup 1つのステージの形全てを管理
*            この子供に StageController が付いている
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

        // オブジェクトを使い終わった事を通知する
        public IObservable<Unit> OnFinishedAsync => this.finishedSubject.Take(1);


        /* *************************************************
        * 初期化
        ************************************************* */
        public void Init(Vector2 initPos, Quaternion initRotate, Color initColor)
        {
            this.transform.position = initPos;
            this.transform.rotation = initRotate;
            this.render2D.color = initColor;

            // Update の開始
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    this.Move();
                    this.OnHit();
                });
        }


        /* *************************************************
        * 当たり判定
        ************************************************* */
        private void OnHit()
        {
            // 地形のエリア外
            if (StageManager.Instance.IsStageOutPos(this.transform.position))
            {
                this.finishedSubject.OnNext(Unit.Default);
            }
        }


        /* *************************************************
        * 当たり判定
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
        * 消える時に実行
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