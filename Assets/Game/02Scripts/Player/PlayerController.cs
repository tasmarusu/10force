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
        private static PlayerStatePenetrating penetratingState = null;  // 貫通玉
        private PlayerStateBase currentState = penetratingState; // プレイヤーの現在のステート

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