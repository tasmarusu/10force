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
        [SerializeField] private PlayerManager playerManager = null;
        [SerializeField] private CameraManager cameraManager = null;
        [SerializeField] private TimeManager timeManager = null;
        [SerializeField] private EnemyManager enemyManager = null;

        private CompositeDisposable disposables = new CompositeDisposable();



        public enum State
        {
            /// <summary>
            /// 待機状態 基本使わない予定
            /// </summary>
            Wait = 1,
            /// <summary>
            /// MainScene で使う UI のロード
            /// </summary>
            LoadUI = 5,
            /// <summary>
            /// GameMain で使われる物の初期化
            /// </summary>
            Init = 6,
            /// <summary>
            /// キャラが出現する時
            /// </summary>
            CharaLarge = 10,
            /// <summary>
            /// キャラ選択状態
            /// </summary>
            CharaSelect = 20,
            /// <summary>
            /// ゲームをプレイしている時
            /// </summary>
            PlayGame = 30,
        }


        private void Start()
        {
            this.ChangeState(State.LoadUI);
        }



        /***************************************************
        * ステートの切り替え
        * <param name="nextState"> 遷移するステート </param>
        ************************************************** */
        public void ChangeState(State nextState)
        {
            switch (nextState)
            {
                case State.Wait:
                    break;

                case State.LoadUI:
                    this.StartCoroutine(this.LoadMainUI());

                    break;

                case State.Init:
                    this.playerManager.Init();
                    StageManager.Instance.Init();
                    this.timeManager.Init();
                    this.enemyManager.Init(this.timeManager, 0);

                    MainSceneUI.Instance.Init(this.enemyManager.Model);

                    this.ChangeState(State.CharaLarge);

                    break;

                case State.CharaLarge:
                    this.ChangeState(State.PlayGame);


                    break;

                case State.CharaSelect:
                    this.SelectChara();


                    break;

                case State.PlayGame:
                    Observable.EveryUpdate()
                        .Subscribe(_ =>
                        {
                            this.playerManager.OnUpdate();
                            this.timeManager.OnUpdate();
                            this.enemyManager.OnUpdate();
                            MainSceneUI.Instance.OnUpdate();
                        }).AddTo(this.disposables);

                    break;
            }
        }



        /***************************************************
        * メインゲームのUIをロードする
        ************************************************** */
        private IEnumerator LoadMainUI()
        {
            yield return SceneManager.LoadSceneAsync("MainSceneUI", LoadSceneMode.Additive);

            this.ChangeState(State.Init);
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
                MainSceneUI.Instance.PlayerInput.OnClick().Subscribe(_ =>
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

