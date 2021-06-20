/* *************************************************
* PlayerManager プレイヤーを監視するスクリプト
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class PlayerController : MonoBehaviour
    {
        // ステート管理
        private static PlayerStatePenetrating statePenetrating = new PlayerStatePenetrating();  // 貫通玉
        private static PlayerStateWin stateWin = new PlayerStateWin();                          // 全ての敵を撃破
        private static PlayerStateLose stateLose = new PlayerStateLose();                       // 敵に当たったもしくは玉に当たった
        private PlayerStateBase currentState = statePenetrating;    // プレイヤーの現在のステート

        // private 変数
        private PlayerInput input = null;
        private PlayerModel model = null;
        private StageManager stageManager = null;


        /***************************************************
        * 初期化
        ************************************************** */
        public void Init(PlayerModel model, StageManager stageManager)
        {
            this.input = MainSceneUI.Instance.PlayerInput;
            this.model = model;
            this.stageManager = stageManager;
        }

        /***************************************************
        * プレイヤーの更新
        ************************************************** */
        public void OnUpdate()
        {
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
        * プレイヤーの移動
        ************************************************** */
        // 移動
        private void Move()
        {
            float moveValue = GameConfig.Instance.Player.speed;

            Vector2 pos = this.transform.position;
            pos += this.input.GetAxis() * moveValue;        // 仮移動
            pos = stageManager.ReplaceOutPlayerPos(pos);    // 移動値が抜けていれば位置修正する
            this.transform.position = pos;
        }
    }
}