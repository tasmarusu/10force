/* *************************************************
* PlayerState �v���C���[�X�e�[�g�̊e�p����
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class PlayerController
    {
        public class PlayerStateBase
        {
            /* *************************************************
            * �X�e�[�g���؂�ւ�������ɌĂ΂��(�������Ƃ�)
            ************************************************* */
            public virtual void OnEnter(PlayerController owner, PlayerStateBase prevState) { }
            /* *************************************************
            * Update���s
            ************************************************* */
            public virtual void OnUpdate(PlayerController owner) { }
            /* *************************************************
            * ���̃X�e�[�g����o�鎞�ɌĂ΂��
            ************************************************* */
            public virtual void OnExit(PlayerController owner, PlayerStateBase nextState) { }
            /* *************************************************
            * �V���b�g
            ************************************************* */
            protected virtual void OnShot(PlayerController owner) 
            {
                owner.Shot();
            }
        }
    } 
}