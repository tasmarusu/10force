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

        private bool isBaseInspector = false;

        /// <summary>
        /// Inspector��GUI���X�V
        /// </summary>
        public override void OnInspectorGUI()
        {
            ////����Inspector������\��
            //base.OnInspectorGUI();
            string baseWord = this.isBaseInspector == true ? "��\��" : "����Inspector������\��";
            this.isBaseInspector = EditorGUILayout.ToggleLeft(baseWord, this.isBaseInspector);
            if (this.isBaseInspector == true)
            {
                EditorGUILayout.LabelField("");
                base.OnInspectorGUI();
                EditorGUILayout.Space(120);
            }

            //EditorGUILayout.HelpBox("�������ā`�`", MessageType.None);
            //EditorGUILayout.HelpBox("�������ā`�`", MessageType.Info);
            //EditorGUILayout.HelpBox("�������ā`�`", MessageType.Error);
            //EditorGUILayout.HelpBox("�������ā`�`", MessageType.Warning);

            //EditorGUI.BeginDisabledGroup(true);
            //EditorGUILayout.ObjectField("EnemyAppearPatternEditor", MonoScript.FromMonoBehaviour((MonoBehaviour)target), typeof(MonoScript), false);
            //EditorGUI.EndDisabledGroup();

            // EnemyAppearPattern �̎擾
            EnemyAppearPattern pattern = target as EnemyAppearPattern;

            // ���\�[�X����擾
            string path = "Enemy";
            EnemyController[] enemys = Resources.LoadAll<EnemyController>(path);



            ////////////////////////////////////////////////////////////////////////////////////////
            /// �G�̐����A�폜
            ////////////////////////////////////////////////////////////////////////////////////////
            if (GUILayout.Button($"�S�Ă̓G���Đ���"))
            {
                this.CreateEnemy(pattern);
            }
            //if (GUILayout.Button($"�\�����Ă���G�S�폜"))
            //{
            //    this.AllDeleteEnemy(pattern);
            //}

            ////////////////////////////////////////////////////////////////////////////////////////
            /// �G�̒ǉ�
            ////////////////////////////////////////////////////////////////////////////////////////
            GUILayout.Label("------�G�̒ǉ�------");

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
                        GUILayout.Label("�Q��");
                        EnemyController enemyPrefab = (EnemyController)EditorGUILayout.ObjectField(item.enemyPrefab, typeof(EnemyController), true);
                        GUILayout.Label("�G");
                        EnemyController enemyObj = (EnemyController)EditorGUILayout.ObjectField(item.enemyObj, typeof(EnemyController), true);
                        GUILayout.Label("����");
                        float timer = EditorGUILayout.FloatField(item.timer);
                        GUILayout.Label("����");
                        bool isDrop = EditorGUILayout.Toggle(item.isDrop);

                        // ���̃A�C�e���̍폜
                        if (GUILayout.Button("�폜"))
                        {
                            Destroy(item.enemyObj);
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
                    pattern.Orders.Sort((a, b) => a.enemyPrefab.name.CompareTo(b.enemyPrefab.name));
                }
                else if (GUILayout.Button($"�G�ԍ� ����"))
                {
                    pattern.Orders.Sort((a, b) => b.enemyPrefab.name.CompareTo(a.enemyPrefab.name));
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
            this.AllDeleteData(pattern);
        }



        /// <summary>
        /// ��̒ǉ� enemy ���ǉ�
        /// </summary>
        private void AddEnemy(EnemyAppearPattern pattern, EnemyController enemy)
        {
            EnemyAppearPattern.Order item = new EnemyAppearPattern.Order();
            item.SetEnemy(enemy);
            pattern.Orders.Add(item);
            pattern.Orders[pattern.Orders.Count - 1].enemyObj = Instantiate(enemy, pattern.transform);
        }

        /// <summary>
        /// �S�Ă̓G�f�[�^�̍폜
        /// </summary>
        private void AllDeleteData(EnemyAppearPattern pattern)
        {
            if (GUILayout.Button("�S�폜"))
            {
                this.AllDeleteEnemy(pattern);
                pattern.Orders.Clear();
            }
        }


        /// <summary>
        /// �G�̑S�f�[�^����
        /// </summary>
        /// <param name="pattern"></param>
        private void CreateEnemy(EnemyAppearPattern pattern)
        {
            this.AllDeleteEnemy(pattern);

            for (int i = 0; i < pattern.Orders.Count; i++)
            {
                if (pattern.Orders[i].enemyPrefab != null)
                {
                    pattern.Orders[i].enemyObj = Instantiate(pattern.Orders[i].enemyPrefab, pattern.transform);
                }
                else
                {
                    Debug.LogError($"{i}�ԍ��̓G���ݒ肳��Ă��܂���");
                }
            }
        }


        /// <summary>
        /// �\�����Ă���G�̑S�폜
        /// </summary>
        /// <param name="pattern"></param>
        private void AllDeleteEnemy(EnemyAppearPattern pattern)
        {
            int count = pattern.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                DestroyImmediate(pattern.transform.GetChild(0).gameObject);
            }
        }

#endif
    }
}