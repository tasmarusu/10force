/* *************************************************
* PlayerStateLose ÉvÉåÉCÉÑÅ[Ç™ïâÇØÇΩéû
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class PlayerController
    {
        public class PlayerStateLose : PlayerStateBase
        {
            public override void OnEnter(PlayerController owner, PlayerStateBase prevState)
            {
                base.OnEnter(owner, prevState);

            }

            public override void OnUpdate(PlayerController owner)
            {
                base.OnUpdate(owner);

            }

            public override void OnExit(PlayerController owner, PlayerStateBase nextState)
            {
                base.OnExit(owner, nextState);


            }
        }
    }
}