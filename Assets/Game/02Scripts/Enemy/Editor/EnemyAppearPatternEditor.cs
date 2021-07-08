/* *************************************************
* EnemyAppearPatternEditor EnemyAppearPattern の拡張クラス
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using System;
    using System.Linq;

    [CustomEditor(typeof(EnemyAppearPattern))]//拡張するクラスを指定
    public class EnemyAppearPatternEditor : Editor
    {
#if UNITY_EDITOR

        /// <summary>
        /// InspectorのGUIを更新
        /// </summary>
        public override void OnInspectorGUI()
        {
            ////元のInspector部分を表示
            //base.OnInspectorGUI();

            // EnemyAppearPattern の取得
            EnemyAppearPattern pattern = target as EnemyAppearPattern;


            ////////////////////////////////////////////////////////////////////////////////////////
            /// 敵の追加
            ////////////////////////////////////////////////////////////////////////////////////////
            GUILayout.Label("------敵の追加------");
            // リソースから取得
            string path = "Enemy";
            EnemyController[] enemys = Resources.LoadAll<EnemyController>(path);

            // このアイテムの削除
            for (int i = 0; i < enemys.Length; i++)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EnemyController enemy = (EnemyController)EditorGUILayout.ObjectField(enemys[i], typeof(EnemyController), true);
                    // このアイテムの削除
                    if (GUILayout.Button($"敵{i + 1}追加"))
                    {
                        // 一体追加
                        this.AddEnemy(pattern, enemy);
                    }
                }
            }





            ////////////////////////////////////////////////////////////////////////////////////////
            /// 敵の設定を表示
            ////////////////////////////////////////////////////////////////////////////////////////
            GUILayout.Space(20);
            GUILayout.Label("------敵の情報------");
            for (int i = 0; i < pattern.Orders.Count; i++)
            {
                EnemyAppearPattern.Order item = pattern.Orders[i];

                GUILayout.Space(5);

                // 時間と強化アイテムと敵設定
                using (new EditorGUILayout.HorizontalScope())
                {
                    {
                        // 配列の移動
                        EditorGUI.BeginDisabledGroup(i == 0);
                        if (GUILayout.Button("上"))
                        {
                            pattern.Orders.RemoveAt(i);
                            pattern.Orders.Insert(i - 1, item);
                        }
                        EditorGUI.EndDisabledGroup();

                        EditorGUI.BeginDisabledGroup(i == pattern.Orders.Count - 1);
                        if (GUILayout.Button("下"))
                        {
                            pattern.Orders.RemoveAt(i);
                            pattern.Orders.Insert(i + 1, item);
                        }
                        EditorGUI.EndDisabledGroup();

                        // 各データの表示
                        GUILayout.Label("種類");
                        EnemyController enemy = (EnemyController)EditorGUILayout.ObjectField(item.enemy, typeof(EnemyController), true);
                        GUILayout.Label("時間");
                        float timer = EditorGUILayout.FloatField(item.timer);
                        GUILayout.Label("強化");
                        bool isDrop = EditorGUILayout.Toggle(item.isDrop);

                        // このアイテムの削除
                        if (GUILayout.Button("削除"))
                        {
                            pattern.Orders.Remove(item);
                        }

                        // 上の設定を反映
                        item.SetData(timer, isDrop);
                    }

                }
            }



            ////////////////////////////////////////////////////////////////////////////////////////
            /// ソート各種
            ////////////////////////////////////////////////////////////////////////////////////////
            GUILayout.Space(20);
            GUILayout.Label("------敵の情報ソート------");
            using (new GUILayout.HorizontalScope())
            {
                // 時間ソート
                if (GUILayout.Button($"敵番号 降順"))
                {
                    pattern.Orders.Sort((a, b) => a.enemy.name.CompareTo(b.enemy.name));
                }
                else if (GUILayout.Button($"敵番号 昇順"))
                {
                    pattern.Orders.Sort((a, b) => b.enemy.name.CompareTo(a.enemy.name));
                }
            }
            using (new GUILayout.HorizontalScope())
            {
                // 時間ソート
                if (GUILayout.Button($"時間 降順"))
                {
                    pattern.Orders.Sort((a, b) => a.timer.CompareTo(b.timer));
                }
                else if (GUILayout.Button($"時間 昇順"))
                {
                    pattern.Orders.Sort((a, b) => b.timer.CompareTo(a.timer));
                }
            }
            using (new GUILayout.HorizontalScope())
            {
                // 強化するかしないかソート
                if (GUILayout.Button($"強化 降順"))
                {
                    pattern.Orders.Sort((a, b) => a.isDrop.CompareTo(b.isDrop));
                }
                else if (GUILayout.Button($"強化 昇順"))
                {
                    pattern.Orders.Sort((a, b) => b.isDrop.CompareTo(a.isDrop));
                }
            }



            ////////////////////////////////////////////////////////////////////////////////////////
            /// 全ての敵を生成
            ////////////////////////////////////////////////////////////////////////////////////////
            GUILayout.Space(20);
            GUILayout.Label("------敵の情報削除------");
            this.AllDelete(pattern);


            ////////////////////////////////////////////////////////////////////////////////////////
            /// 全ての敵データの削除
            ////////////////////////////////////////////////////////////////////////////////////////
            GUILayout.Space(20);
            GUILayout.Label("------敵の情報削除------");
            this.AllDelete(pattern);
        }



        /// <summary>
        /// 一体追加 enemy が追加
        /// </summary>
        private void AddEnemy(EnemyAppearPattern pattern, EnemyController enemy)
        {
            EnemyAppearPattern.Order item = new EnemyAppearPattern.Order();
            item.SetEnemy(enemy);
            pattern.Orders.Add(item);
        }

        /// <summary>
        /// 全ての敵データの削除
        /// </summary>
        private void AllDelete(EnemyAppearPattern pattern)
        {
            if (GUILayout.Button("全削除"))
            {
                pattern.Orders.Clear();
            }
        }

#endif
    }
}