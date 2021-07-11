/* *************************************************
* CameraManager カメラを監視するスクリプト
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CameraManager : MonoBehaviour
    {
        [field:SerializeField] public new Camera WorldCamera { get; private set; } = null;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {

        }
    }
}