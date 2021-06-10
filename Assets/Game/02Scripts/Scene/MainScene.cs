/* *************************************************
* InGameの始まりから終わりまでを監視する
************************************************* */
namespace MainForce
{
    using System;
    using System.Threading;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;
    using UnityEngine.SceneManagement;
    using System.Threading.Tasks;
    using Cysharp.Threading.Tasks;

    //using Cysharp.Threading.Tasks;

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
            StartCoroutine(this.LoadMainUI());
            this.playerInput.Init();
            this.playerManager.Init(this.playerInput);
            this.SelectChara();
        }


        /***************************************************
        * メインゲームのUIをロードする
        ************************************************** */
        private IEnumerator LoadMainUI()
        {
            yield return SceneManager.LoadSceneAsync("MainSceneUI", LoadSceneMode.Additive);
        }

        /***************************************************
        * ゲームが開始してキャラ選択するまでの時間
        ************************************************** */
        private void InScene()
        {

        }

        /***************************************************
        * InGame で使う 自機 を決める
        ************************************************** */
        private void SelectChara()
        {
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

        /***************************************************
        * InGame 時間経過し始めた時点で使う関数などの呼び出し
        ************************************************** */
        private void InGamePlay()
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {
                Debug.Log($"frameCount {Time.frameCount}");
            }).AddTo(disposables);
        }
    }
}

