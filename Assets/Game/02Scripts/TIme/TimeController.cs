/* *************************************************
* TimeManager ���Ԃ��Ď�����X�N���v�g
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    public class TimeController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeText = null;


        public void Init()
        {

        }


        public void ChangeText(int num)
        {
            this.timeText.text = $"{num}";
        }
    }
}