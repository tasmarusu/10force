/* *************************************************
* PlayerStatePenetrating プレイヤーが 貫通玉 を打つ
************************************************* */
namespace MainForce
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UniRx;
    using UnityEngine;

    public partial class PlayerController
    {
        public class PlayerStatePenetrating : PlayerStateBase
        {
            private PenetratingBulletPool penetratingPool;

            public override void OnEnter(PlayerController owner, PlayerStateBase prevState)
            {
                base.OnEnter(owner, prevState);

                // 貫通弾の Pool を取得
                this.penetratingPool = owner.poolProvider.GetPenetrating();

                Debug.Log($"ショット");

                // 取り合えず定期的に関数呼び出し
                float intervale = GameConfig.Instance.Shot.Penetrating.interval;
                Observable.Interval(TimeSpan.FromSeconds(intervale))
                    .Where(_=> owner.isClicking == true)
                    .Subscribe(_ =>
                    {
                        this.OnShot(owner);
                    }).AddTo(owner);
            }

            public override void OnUpdate(PlayerController owner)
            {
                base.OnUpdate(owner);

            }

            public override void OnExit(PlayerController owner, PlayerStateBase prevState)
            {
                base.OnExit(owner, prevState);
            }

            protected override void OnShot(PlayerController owner)
            {
                base.OnShot(owner);

                // shotObj に Rent() で借りてきて、Unit.Default が来たら返すと行ける
                PenetratingBulletController shot = this.penetratingPool.Rent();  // インスタンスを取得
                shot.Init(owner.transform.position, owner.transform.rotation, Color.white);
                shot.OnFinishedAsync
                    .Take(1)
                    .Subscribe(_ =>
                    {
                        this.penetratingPool.Return(shot);
                    });

                Debug.Log($"ショット1");
            }
        }
    }
}