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
        // �X�e�[�g�Ǘ�
        private static PlayerStatePenetrating statePenetrating = new PlayerStatePenetrating();  // �ђʋ�
        private static PlayerStateWin stateWin = new PlayerStateWin();                          // �S�Ă̓G�����j
        private static PlayerStateLose stateLose = new PlayerStateLose();                       // �G�ɓ��������������͋ʂɓ�������
        private PlayerStateBase currentState = statePenetrating;    // �v���C���[�̌��݂̃X�e�[�g

        // private �ϐ�
        private PlayerInput input = null;
        private PlayerModel model = null;
        private StageManager stageManager = null;


        /***************************************************
        * ������
        ************************************************** */
        public void Init(PlayerModel model, StageManager stageManager)
        {
            this.input = MainSceneUI.Instance.PlayerInput;
            this.model = model;
            this.stageManager = stageManager;
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
        private void ChangeState(PlayerStateBase nextState)
        {
            this.currentState.OnExit(this, nextState);
            nextState.OnEnter(this, this.currentState);
            currentState = nextState;
        }


        /***************************************************
        * �v���C���[�̈ړ�
        ************************************************** */
        // �ړ�
        private void Move()
        {
            float moveValue = GameConfig.Instance.Player.speed;

            Vector2 pos = this.transform.position;
            pos += this.input.GetAxis() * moveValue;        // ���ړ�
            pos = stageManager.ReplaceOutPlayerPos(pos);    // �ړ��l�������Ă���Έʒu�C������
            this.transform.position = pos;
        }
    }
}