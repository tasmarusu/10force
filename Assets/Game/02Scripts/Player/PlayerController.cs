/* *************************************************
* PlayerManager �v���C���[���Ď�����X�N���v�g
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class PlayerController : MonoBehaviour
    {
        private static PlayerStatePenetrating statePenetrating = new PlayerStatePenetrating();  // �ђʋ�
        private static PlayerStateWin stateWin = new PlayerStateWin();                          // �S�Ă̓G�����j
        private static PlayerStateLose stateLose = new PlayerStateLose();                       // �G�ɓ��������������͋ʂɓ�������
        private PlayerStateBase currentState = statePenetrating;    // �v���C���[�̌��݂̃X�e�[�g

        private PlayerInput input = null;
        private PlayerModel model = null;


        /***************************************************
        * ������
        ************************************************** */
        public void Init(PlayerInput input, PlayerModel model)
        {
            this.input = input;
            this.model = model;

        }

        /***************************************************
        * �v���C���[�̍X�V
        ************************************************** */
        public void OnUpdate()
        {
            this.currentState.OnUpdate(this);

            // �ړ���p�x�Ȃǂ�ݒ肷��
            //this.model.SetCharaValue()
        }

        /***************************************************
        * �X�e�[�g�̐؂�ւ�
        ************************************************** */
        public void ChangeState(PlayerStateBase nextState)
        {
            this.currentState.OnExit(this, nextState);
            nextState.OnEnter(this, this.currentState);
            currentState = nextState;
        }
    }
}