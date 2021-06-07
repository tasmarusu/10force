/* *************************************************
* PlayerInput 移動
************************************************* */
namespace MainForce
{
    using System;   
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using UniRx;
    using UniRx.Triggers;

    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Image judgeImage = null;   // 判定用画像

        /// <summary>
        /// Input系の初期化
        /// </summary>
        public void Init()
        {
            Debug.Log($"{this} の初期化");
        }


        /// <summary>
        /// クリックをした時
        /// </summary>
        /// <returns></returns>
        public IObservable<PointerEventData> OnClick()
        {
            return judgeImage.OnPointerClickAsObservable();
        }
    }
}