/* *************************************************
* InGameの始まりから終わりまでを監視する
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;

    public class MainScene : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput = null;
        [SerializeField] private PlayerManager playerManager = null;
        [SerializeField] private CameraManager cameraManager = null;

        private CompositeDisposable disposables = new CompositeDisposable();


        private void Start()
        {
            this.Init();
        }


        /// <summary>
        /// 全体の初期化
        /// </summary>
        private void Init()
        {
            this.playerInput.Init();

            this.SelectChara();
        }


        /// <summary>
        /// InGame で使う 自機 を決める
        /// </summary>
        private void SelectChara()
        {
            Debug.Log($"SelectChara {Time.frameCount}");

            Observable.EveryUpdate().Subscribe(_ =>
            {
                // 自機決定したら次へ
                this.playerInput.OnClick().Subscribe(_ =>
                {
                    this.disposables.Clear();
                    this.InGamePlay();

                }).AddTo(disposables);
            }).AddTo(disposables);
        }


        /// <summary>
        /// InGame で使う関数などの呼び出し
        /// </summary>
        private void InGamePlay()
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {
                Debug.Log($"frameCount {Time.frameCount}");
            }).AddTo(disposables);
        }
    }
}

