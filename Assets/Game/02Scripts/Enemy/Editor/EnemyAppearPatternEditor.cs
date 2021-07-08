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


            ////////////////////////////////////////////////////////////////////////////////////////
            /// �G�̒ǉ�
            ////////////////////////////////////////////////////////////////////////////////////////
            GUILayout.Label("------�G�̒ǉ�------");
            // ���\�[�X����擾
            string path = "Enemy";
            EnemyController[] enemys = Resources.LoadAll<EnemyController>(path);

            // ���̃A�C�e���̍폜
            for (int i = 0; i < enemys.Length; i++)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EnemyController enemy = (EnemyController)EditorGUILayout.ObjectField(enemys[i], typeof(EnemyController), true);
                    // ���̃A�C�e���̍폜
                    if (GUILayout.Button($"�G{i + 1}�ǉ�"))
                    {
                        // ��̒ǉ�
                        this.AddEnemy(pattern, enemy);
                    }
                }
            }





            ////////////////////////////////////////////////////////////////////////////////////////
            /// �G�̐ݒ��\��
            ////////////////////////////////////////////////////////////////////////////////////////
            GUILayout.Space(20);
            GUILayout.Label("------�G�̏��------");
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



            ////////////////////////////////////////////////////////////////////////////////////////
            /// �\�[�g�e��
            ////////////////////////////////////////////////////////////////////////////////////////
            GUILayout.Space(20);
            GUILayout.Label("------�G�̏��\�[�g------");
            using (new GUILayout.HorizontalScope())
            {
                // ���ԃ\�[�g
                if (GUILayout.Button($"�G�ԍ� �~��"))
                {
                    pattern.Orders.Sort((a, b) => a.enemy.name.CompareTo(b.enemy.name));
                }
                else if (GUILayout.Button($"�G�ԍ� ����"))
                {
                    pattern.Orders.Sort((a, b) => b.enemy.name.CompareTo(a.enemy.name));
                }
            }
            using (new GUILayout.HorizontalScope())
            {
                // ���ԃ\�[�g
                if (GUILayout.Button($"���� �~��"))
                {
                    pattern.Orders.Sort((a, b) => a.timer.CompareTo(b.timer));
                }
                else if (GUILayout.Button($"���� ����"))
                {
                    pattern.Orders.Sort((a, b) => b.timer.CompareTo(a.timer));
                }
            }
            using (new GUILayout.HorizontalScope())
            {
                // �������邩���Ȃ����\�[�g
                if (GUILayout.Button($"���� �~��"))
                {
                    pattern.Orders.Sort((a, b) => a.isDrop.CompareTo(b.isDrop));
                }
                else if (GUILayout.Button($"���� ����"))
                {
                    pattern.Orders.Sort((a, b) => b.isDrop.CompareTo(a.isDrop));
                }
            }



            ////////////////////////////////////////////////////////////////////////////////////////
            /// �S�Ă̓G�𐶐�
            ////////////////////////////////////////////////////////////////////////////////////////
            GUILayout.Space(20);
            GUILayout.Label("------�G�̏��폜------");
            this.AllDelete(pattern);


            ////////////////////////////////////////////////////////////////////////////////////////
            /// �S�Ă̓G�f�[�^�̍폜
            ////////////////////////////////////////////////////////////////////////////////////////
            GUILayout.Space(20);
            GUILayout.Label("------�G�̏��폜------");
            this.AllDelete(pattern);
        }



        /// <summary>
        /// ��̒ǉ� enemy ���ǉ�
        /// </summary>
        private void AddEnemy(EnemyAppearPattern pattern, EnemyController enemy)
        {
            EnemyAppearPattern.Order item = new EnemyAppearPattern.Order();
            item.SetEnemy(enemy);
            pattern.Orders.Add(item);
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