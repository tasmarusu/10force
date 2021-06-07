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

        private PlayerInput playerInput = null;


        /***************************************************
        * 初期化
        ************************************************** */
        public void Init(PlayerInput playerInput)
        {
            this.playerInput = playerInput;


        }

        /***************************************************
        * プレイヤーの更新
        ************************************************** */
        public void OnUpdate()
        {
            this.currentState.OnUpdate(this);
        }

        /***************************************************
        * ステートの切り替え
        ************************************************** */
        public void ChangeState(PlayerStateBase nextState)
        {
            this.currentState.OnExit(this, nextState);
            nextState.OnEnter(this, this.currentState);
            currentState = nextState;
        }
    }
}