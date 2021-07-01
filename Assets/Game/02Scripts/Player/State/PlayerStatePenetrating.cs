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

                // ��荇��������I�Ɋ֐��Ăяo��
                Observable.Interval(TimeSpan.FromSeconds(1.0f))
                    .Subscribe(_ =>
                    {
                        this.OnShot(owner);
                    }).AddTo(owner);
            }

            public override void OnUpdate(PlayerController owner)
            {
                base.OnUpdate(owner);

                owner.Move();
            }

            public override void OnExit(PlayerController owner, PlayerStateBase prevState)
            {
                base.OnExit(owner, prevState);
            }

            protected override void OnShot(PlayerController owner)
            {
                base.OnShot(owner);

                // shotObj �� Rent() �Ŏ؂�Ă��āAUnit.Default ��������Ԃ��ƍs����
                var shotObj = this.penetratingPool.Rent();  // �C���X�^���X���擾
                shotObj.Init(Vector2.zero, Quaternion.identity);
                shotObj.OnFinishedAsync
                    .Take(1)
                    .Subscribe(_ =>
                    {
                        this.penetratingPool.Return(shotObj);
                    });

                Debug.Log($"�V���b�g");
            }
        }
    }
}