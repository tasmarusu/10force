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
        private readonly Subject<Unit> finishedSubject = new Subject<Unit>();

        // オブジェクトを使い終わった事を通知する
        public IObservable<Unit> OnFinishedAsync => this.finishedSubject.Take(1);


        /* *************************************************
        * 初期化
        ************************************************* */
        public void Init(Vector2 initPos, Quaternion initRotate)
        {
            // 当たり判定開始
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(collider2D =>
                {
                    this.OnHit(collider2D);
                });

            // Update の開始
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {

                });
        }


        /* *************************************************
        * 当たり判定
        ************************************************* */
        private void OnHit(Collider2D collider)
        {

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