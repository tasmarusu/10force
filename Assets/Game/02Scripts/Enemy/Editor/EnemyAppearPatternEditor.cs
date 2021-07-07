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

            for (int i = 0; i < pattern.UseEnemys.Count; i++)
            {
                EnemyController enemy = (EnemyController)EditorGUILayout.ObjectField(pattern.UseEnemys[i], typeof(EnemyController), true);

                // このアイテムの削除
                if (GUILayout.Button("削除"))
                {
                    pattern.UseEnemys.Remove(enemy);
                }
            }



            // 一体追加
            this.AddEnemy(pattern);
            // 全ての敵データの削除
            this.AllDelete(pattern);
            GUILayout.Space(20);

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


        }



        /// <summary>
        /// 一体追加
        /// </summary>
        private void AddEnemy(EnemyAppearPattern pattern)
        {
            if (GUILayout.Button("1体追加"))
            {
                EnemyAppearPattern.Order item = new EnemyAppearPattern.Order();
                pattern.Orders.Add(item);
            }
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