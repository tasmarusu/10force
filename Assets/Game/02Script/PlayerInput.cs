/* *************************************************
* InGameの始まりから終わりまでを監視する
************************************************* */



namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;

    public class PlayerInput : MonoBehaviour
    {
        /// <summary>
        /// Input系の初期化
        /// </summary>
        public void Init()
        {
            Debug.Log($"{this} の初期化");
        }

        /// <summary>
        /// クリックテスト
        /// </summary>
        /// <returns></returns>
        public bool IsClick()
        {
            return Input.GetKey(KeyCode.S);
        }
    }
}