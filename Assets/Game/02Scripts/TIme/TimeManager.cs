/* *************************************************
* TimeManager 時間を監視するスクリプト
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private TimeController tenSeconds = null;  // XY:ZW の X
        [SerializeField] private TimeController oneSeconds = null;  // XY:ZW の Y
        [SerializeField] private TimeController tenDecimal = null;  // XY:ZW の Z
        [SerializeField] private TimeController oneDecimal = null;  // XY:ZW の W

        private float startTime = 80.0f; 
        private float remainTime = 0.0f;


        public int FrameCount { get; private set; } = -1;
        public float ProgressTime { get { return this.startTime - this.remainTime; } }

        /***************************************************
        * 初期化
        ************************************************** */
        public void Init()
        {
            this.tenSeconds.Init();
            this.oneSeconds.Init();
            this.tenDecimal.Init();
            this.oneDecimal.Init();

            this.FrameCount = 0;
            this.remainTime = this.startTime;
        }

        /***************************************************
        * ゲーム開始
        ************************************************** */
        public void StartGame()
        {

        }


        /***************************************************
        * 時間関連
        ************************************************** */
        public void OnUpdate()
        {
            this.remainTime -= Time.deltaTime;
            this.FrameCount++;
            this.Calculation();
        }
        // XY:ZWの時間を出す
        private void Calculation()
        {
            float t = this.remainTime;

            // 整数値計算
            int x = Mathf.FloorToInt(t / 10.0f);
            int y = Mathf.FloorToInt(t - (x * 10.0f));

            // 小数値計算
            float decimals = t - Mathf.Floor(t);
            int z = Mathf.FloorToInt(decimals * 10.0f);
            int w = Mathf.FloorToInt((decimals * 100.0f) - (z * 10.0f));
            this.SetTimer(x, y, z, w);
        }
        // XY:ZWの時間をテキストに表示
        private void SetTimer(int tens, int ones, int tend, int oned)
        {
            this.tenSeconds.ChangeText(tens);
            this.oneSeconds.ChangeText(ones);
            this.tenDecimal.ChangeText(tend);
            this.oneDecimal.ChangeText(oned);
        }
    }
}