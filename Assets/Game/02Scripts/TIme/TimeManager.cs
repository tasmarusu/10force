/* *************************************************
* TimeManager ���Ԃ��Ď�����X�N���v�g
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private TimeController tenSeconds = null;  // XY:ZW �� X
        [SerializeField] private TimeController oneSeconds = null;  // XY:ZW �� Y
        [SerializeField] private TimeController tenDecimal = null;  // XY:ZW �� Z
        [SerializeField] private TimeController oneDecimal = null;  // XY:ZW �� W

        private float remainTime = 80.0f;


        public int FrameCount { get; private set; } = -1;

        /***************************************************
        * ������
        ************************************************** */
        public void Init()
        {
            this.tenSeconds.Init();
            this.oneSeconds.Init();
            this.tenDecimal.Init();
            this.oneDecimal.Init();

            this.FrameCount = 0;
        }

        /***************************************************
        * �Q�[���J�n
        ************************************************** */
        public void StartGame()
        {

        }


        /***************************************************
        * ���Ԋ֘A
        ************************************************** */
        public void OnUpdate()
        {
            this.remainTime -= Time.deltaTime;
            this.FrameCount++;
            this.Calculation();
        }
        // XY:ZW�̎��Ԃ��o��
        private void Calculation()
        {
            float t = this.remainTime;

            // �����l�v�Z
            int x = Mathf.FloorToInt(t / 10.0f);
            int y = Mathf.FloorToInt(t - (x * 10.0f));

            // �����l�v�Z
            float decimals = t - Mathf.Floor(t);
            int z = Mathf.FloorToInt(decimals * 10.0f);
            int w = Mathf.FloorToInt((decimals * 100.0f) - (z * 10.0f));
            Debug.Log($"{t} {x} {y} {z} {w}");
            this.SetTimer(x, y, z, w);
        }
        // XY:ZW�̎��Ԃ��e�L�X�g�ɕ\��
        private void SetTimer(int tens, int ones, int tend, int oned)
        {
            this.tenSeconds.ChangeText(tens);
            this.oneSeconds.ChangeText(ones);
            this.tenDecimal.ChangeText(tend);
            this.oneDecimal.ChangeText(oned);
        }
    }
}