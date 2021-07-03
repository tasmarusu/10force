/* *************************************************
* PlayerManager プレイヤーを監視するスクリプト
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


        // ステート管理
        private static PlayerStatePenetrating statePenetrating = new PlayerStatePenetrating();  // 貫通玉
        private static PlayerStateWin stateWin = new PlayerStateWin();                          // 全ての敵を撃破
        private static PlayerStateLose stateLose = new PlayerStateLose();                       // 敵に当たったもしくは玉に当たった
        private PlayerStateBase currentState = statePenetrating;    // プレイヤーの現在のステート

        // private 変数
        private bool isClicking = false;
        private PlayerInput input = null;
        private PlayerModel model = null;



        /***************************************************
        * 初期化
        ************************************************** */
        public void Init(PlayerModel model)
        {
            this.input = MainSceneUI.Instance.PlayerInput;
            this.model = model;
            this.ChangeState(statePenetrating);

            // input関連を取得
            this.input.OnDown().Subscribe(_ => this.isClicking = true);
            this.input.OnUp().Subscribe(_ => this.isClicking = false);
        }

        /***************************************************
        * プレイヤーの更新
        ************************************************** */
        public void OnUpdate()
        {
            this.Move();
            this.Rotate();

            this.currentState.OnUpdate(this);

            // 移動や角度などを設定する
            //this.model.SetCharaValue()
        }

        /***************************************************
        * ステートの切り替え
        ************************************************** */
        private void ChangeState(PlayerStateBase nextState)
        {
            this.currentState.OnExit(this, nextState);
            nextState.OnEnter(this, this.currentState);
            currentState = nextState;
        }


        /***************************************************
        * プレイヤーの移動と回転
        ************************************************** */
        private void Move()
        {
            float moveValue = GameConfig.Instance.Player.speed;

            Vector2 pos = this.transform.position;
            pos += this.input.GetAxis() * moveValue;        // 仮移動
            pos = StageManager.Instance.ReplaceOutPlayerPos(pos);    // 移動値が抜けていれば位置修正する
            this.transform.position = pos;
        }
        private void Rotate()
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 vec = (pos - (Vector2)this.transform.position).normalized;
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
        }

        /***************************************************
        * プレイヤーの射撃
        ************************************************** */
        public void Shot()
        {

        }
    }
}