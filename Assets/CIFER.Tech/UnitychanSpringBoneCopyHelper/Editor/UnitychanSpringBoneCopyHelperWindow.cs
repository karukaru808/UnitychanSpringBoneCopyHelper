using System.Linq;
using UnityEditor;
using UnityEngine;
using UTJ;

namespace CIFER.Tech.UnitychanSpringBoneCopyHelper
{
    public class UnitychanSpringBoneCopyHelperWindow : EditorWindow
    {
        public SpringBone[] beforeSpringBone = new SpringBone[0];
        public SpringBone[] afterSpringBone = new SpringBone[0];

        private Vector2 _beforeScrollPos = Vector2.zero;
        private Vector2 _afterScrollPos = Vector2.zero;

        [MenuItem("CIFER.Tech/UnitychanSpringBoneCopyHelper")]
        private static void Open()
        {
            var window = GetWindow<UnitychanSpringBoneCopyHelperWindow>("USBCopyHelper");
            window.minSize = new Vector2(650f, 300f);
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandHeight(true));
            {
                //コピー元
                EditorGUILayout.BeginVertical(GUI.skin.box);
                {
                    if (GUILayout.Button("選択中のオブジェクト以下のSpringBoneを登録する"))
                    {
                        var selectSpringBones = FindFollowerSpringBone();
                        if (selectSpringBones != null)
                            beforeSpringBone = selectSpringBones;
                    }

                    EditorGUILayout.Space();

                    _beforeScrollPos = EditorGUILayout.BeginScrollView(_beforeScrollPos, GUI.skin.box);
                    {
                        ArrayApplyModifiedProperties(nameof(beforeSpringBone), "コピー元");
                    }
                    EditorGUILayout.EndScrollView();
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.Space();

                //コピー先
                EditorGUILayout.BeginVertical(GUI.skin.box);
                {
                    if (GUILayout.Button("選択中のオブジェクト以下のSpringBoneを登録する"))
                    {
                        var selectSpringBones = FindFollowerSpringBone();
                        if (selectSpringBones != null)
                            afterSpringBone = selectSpringBones;
                    }

                    EditorGUILayout.Space();

                    _afterScrollPos = EditorGUILayout.BeginScrollView(_afterScrollPos, GUI.skin.box);
                    {
                        ArrayApplyModifiedProperties(nameof(afterSpringBone), "コピー先");
                    }
                    EditorGUILayout.EndScrollView();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();

            //エラー、警告判定
            if (beforeSpringBone.Length <= 0 || afterSpringBone.Length <= 0)
            {
                EditorGUILayout.HelpBox("リストの状態が不正です。", MessageType.Error);
                return;
            }

            if (beforeSpringBone.Any(bsb => bsb == null) || afterSpringBone.Any(asb => asb == null))
            {
                EditorGUILayout.HelpBox("Nullが含まれています。\r\n該当要素はスキップされます。", MessageType.Warning);
            }

            if (beforeSpringBone.Length != afterSpringBone.Length)
            {
                EditorGUILayout.HelpBox("配列のサイズが違います。\r\n足りない要素分はスキップされます。", MessageType.Warning);
            }

            //コピー！
            if (GUILayout.Button("コピー！"))
            {
                var copyData = new UnitychanSpringBoneCopyHelperData()
                {
                    Before = beforeSpringBone,
                    After = afterSpringBone
                };
                UnitychanSpringBoneCopyHelper.SpringBoneCopy(copyData);
            }
        }

        private void ArrayApplyModifiedProperties(string varName, string dispName)
        {
            ScriptableObject target = this;
            var so = new SerializedObject(target);
            var sp = so.FindProperty(varName);
            EditorGUILayout.PropertyField(sp, new GUIContent(dispName), true, GUILayout.ExpandWidth(true));
            so.ApplyModifiedProperties();
        }

        private static SpringBone[] FindFollowerSpringBone()
        {
            if (Selection.transforms.Length <= 0)
                return null;

            var selectTransform = Selection.transforms.First();
            return selectTransform.GetComponentsInChildren<SpringBone>();
        }
    }
}