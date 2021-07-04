/* *************************************************
* MainSceneUI InGame ÇÃ UIï\é¶
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MainSceneUI : SingletonMono<MainSceneUI>
    {
        [SerializeField] private PlayerInput playerInput = null;

        public PlayerInput PlayerInput { get { return this.playerInput; } }


        /***************************************************
        * èâä˙âª
        ************************************************** */
        public void Init()
        {
            this.playerInput.Init();
        }
    }
}