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
        private static PlayerStatePenetrating statePenetrating = new PlayerStatePenetrating();  // 貫通玉
        private static PlayerStateWin stateWin = new PlayerStateWin();                          // 全ての敵を撃破
        private static PlayerStateLose stateLose = new PlayerStateLose();                       // 敵に当たったもしくは玉に当たった
        private PlayerStateBase currentState = statePenetrating;    // プレイヤーの現在のステート

        private PlayerInput input = null;
        private PlayerModel model = null;


        /***************************************************
        * 初期化
        ************************************************** */
        public void Init(PlayerModel model)
        {
            this.input = MainSceneUI.Instance.PlayerInput;
            this.model = model;
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
        private void Move()
        {
            float moveValue = 2.0f;

            Vector2 pos = this.transform.position;
            pos += this.input.GetAxis() * moveValue;
            this.transform.position = pos;
        }
    }
}