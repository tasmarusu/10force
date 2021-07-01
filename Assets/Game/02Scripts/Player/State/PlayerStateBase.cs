/* *************************************************
* PlayerState プレイヤーステートの各継承元
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
            * ステートが切り替わった時に呼ばれる(初期化とか)
            ************************************************* */
            public virtual void OnEnter(PlayerController owner, PlayerStateBase prevState) { }
            /* *************************************************
            * Update実行
            ************************************************* */
            public virtual void OnUpdate(PlayerController owner) { }
            /* *************************************************
            * このステートから出る時に呼ばれる
            ************************************************* */
            public virtual void OnExit(PlayerController owner, PlayerStateBase nextState) { }
            /* *************************************************
            * ショット
            ************************************************* */
            protected virtual void OnShot(PlayerController owner) 
            {
                owner.Shot();
            }
        }
    } 
}