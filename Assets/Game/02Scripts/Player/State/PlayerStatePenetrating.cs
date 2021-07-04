/* *************************************************
* PlayerStatePenetrating �v���C���[�� �ђʋ� ��ł�
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

                // �ђʒe�� Pool ���擾
                this.penetratingPool = owner.poolProvider.GetPenetrating();

                Debug.Log($"�V���b�g");

                // ��荇��������I�Ɋ֐��Ăяo��
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

                // shotObj �� Rent() �Ŏ؂�Ă��āAUnit.Default ��������Ԃ��ƍs����
                PenetratingBulletController shot = this.penetratingPool.Rent();  // �C���X�^���X���擾
                shot.Init(owner.transform.position, owner.transform.rotation, Color.white);
                shot.OnFinishedAsync
                    .Take(1)
                    .Subscribe(_ =>
                    {
                        this.penetratingPool.Return(shot);
                    });

                Debug.Log($"�V���b�g1");
            }
        }
    }
}