/* *************************************************
* EnemyAppearPatternEditor EnemyAppearPattern �̊g���N���X
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using System;
    using System.Linq;

    [CustomEditor(typeof(EnemyAppearPattern))]//�g������N���X���w��
    public class EnemyAppearPatternEditor : Editor
    {
#if UNITY_EDITOR

        /// <summary>
        /// Inspector��GUI���X�V
        /// </summary>
        public override void OnInspectorGUI()
        {
            ////����Inspector������\��
            //base.OnInspectorGUI();

            // EnemyAppearPattern �̎擾
            EnemyAppearPattern pattern = target as EnemyAppearPattern;

            for (int i = 0; i < pattern.UseEnemys.Count; i++)
            {
                EnemyController enemy = (EnemyController)EditorGUILayout.ObjectField(pattern.UseEnemys[i], typeof(EnemyController), true);

                // ���̃A�C�e���̍폜
                if (GUILayout.Button("�폜"))
                {
                    pattern.UseEnemys.Remove(enemy);
                }
            }



            // ��̒ǉ�
            this.AddEnemy(pattern);
            // �S�Ă̓G�f�[�^�̍폜
            this.AllDelete(pattern);
            GUILayout.Space(20);

            for (int i = 0; i < pattern.Orders.Count; i++)
            {
                EnemyAppearPattern.Order item = pattern.Orders[i];

                GUILayout.Space(5);

                // ���ԂƋ����A�C�e���ƓG�ݒ�
                using (new EditorGUILayout.HorizontalScope())
                {
                    {
                        // �z��̈ړ�
                        EditorGUI.BeginDisabledGroup(i == 0);
                        if (GUILayout.Button("��"))
                        {
                            pattern.Orders.RemoveAt(i);
                            pattern.Orders.Insert(i - 1, item);
                        }
                        EditorGUI.EndDisabledGroup();

                        EditorGUI.BeginDisabledGroup(i == pattern.Orders.Count - 1);
                        if (GUILayout.Button("��"))
                        {
                            pattern.Orders.RemoveAt(i);
                            pattern.Orders.Insert(i + 1, item);
                        }
                        EditorGUI.EndDisabledGroup();

                        // �e�f�[�^�̕\��
                        GUILayout.Label("���");
                        EnemyController enemy = (EnemyController)EditorGUILayout.ObjectField(item.enemy, typeof(EnemyController), true);
                        GUILayout.Label("����");
                        float timer = EditorGUILayout.FloatField(item.timer);
                        GUILayout.Label("����");
                        bool isDrop = EditorGUILayout.Toggle(item.isDrop);

                        // ���̃A�C�e���̍폜
                        if (GUILayout.Button("�폜"))
                        {
                            pattern.Orders.Remove(item);
                        }

                        // ��̐ݒ�𔽉f
                        item.SetData(timer, isDrop);
                    }

                }
            }


        }



        /// <summary>
        /// ��̒ǉ�
        /// </summary>
        private void AddEnemy(EnemyAppearPattern pattern)
        {
            if (GUILayout.Button("1�̒ǉ�"))
            {
                EnemyAppearPattern.Order item = new EnemyAppearPattern.Order();
                pattern.Orders.Add(item);
            }
        }

        /// <summary>
        /// �S�Ă̓G�f�[�^�̍폜
        /// </summary>
        private void AllDelete(EnemyAppearPattern pattern)
        {
            if (GUILayout.Button("�S�폜"))
            {
                pattern.Orders.Clear();
            }
        }

#endif
    }
}