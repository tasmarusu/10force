/* *************************************************
* CameraManager �J�������Ď�����X�N���v�g
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
        /// ������
        /// </summary>
        public void Init()
        {

        }
    }
}