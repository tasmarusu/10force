/* *************************************************
* PlayerManager �v���C���[���Ď�����X�N���v�g
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;
    using UnityEngine.Diagnostics;

    public partial class PlayerController : MonoBehaviour
    {
        [SerializeField] private BulletPoolProvider poolProvider = null;


        // �X�e�[�g�Ǘ�
        private static PlayerStatePenetrating statePenetrating = new PlayerStatePenetrating();  // �ђʋ�
        private static PlayerStateWin stateWin = new PlayerStateWin();                          // �S�Ă̓G�����j
        private static PlayerStateLose stateLose = new PlayerStateLose();                       // �G�ɓ��������������͋ʂɓ�������
        private PlayerStateBase currentState = statePenetrating;    // �v���C���[�̌��݂̃X�e�[�g

        // private �ϐ�
        private bool isClicking = false;
        private PlayerInput input = null;
        private PlayerModel model = null;



        /***************************************************
        * ������
        ************************************************** */
        public void Init(PlayerModel model)
        {
            this.input = MainSceneUI.Instance.PlayerInput;
            this.model = model;
            this.ChangeState(statePenetrating);

            // input�֘A���擾
            this.input.OnDown().Subscribe(_ => this.isClicking = true);
            this.input.OnUp().Subscribe(_ => this.isClicking = false);
        }

        /***************************************************
        * �v���C���[�̍X�V
        ************************************************** */
        public void OnUpdate()
        {
            this.Move();
            this.Rotate();

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
        * �v���C���[�̈ړ��Ɖ�]
        ************************************************** */
        private void Move()
        {
            float moveValue = GameConfig.Instance.Player.speed;

            Vector2 pos = this.transform.position;
            pos += this.input.GetAxis() * moveValue;        // ���ړ�
            pos = StageManager.Instance.ReplaceOutPlayerPos(pos);    // �ړ��l�������Ă���Έʒu�C������
            this.transform.position = pos;
        }
        private void Rotate()
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 vec = (pos - (Vector2)this.transform.position).normalized;
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
        }

        /***************************************************
        * �v���C���[�̎ˌ�
        ************************************************** */
        public void Shot()
        {

        }
    }
}