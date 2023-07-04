/**
[CustomHI5_InertiaInstance ]
Copyright (c) 2018 neon-izm
This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HI5
{
    /// <summary>
    /// HI5のデフォルトスクリプトをベースに、まともに使うためのアレコレを機能追加したカスタム版
    /// </summary>
    /// 実行順は後ろの方が良い(たぶん、他のIK処理より後ろが良い)ので31000としています
    [DefaultExecutionOrder(31000)]
    public class CustomHI5_InertiaInstance : HI5_Instance
    {
        [Header("カスタム部分はここから")] [SerializeField] [Multiline(8)]
        private string _boneDescription =
            "HandBoneは1に手首 \r\n2-4は親指 \n6-8は人差し指(1,2,3) \n 10-12は中指(1,2,3)\n 14-16は薬指(1,2,3)\n 18-20は小指(1,2,3)";


        //#region 親指の向きを無理やり補正するためのVector3(Euler)

        //要するに、Hi5のデフォルトの手が親指の向きが手刀っぽいのを想定していて
        //実際のキャラ物はパーを想定している
        //その差をなんとか補正するためのつらい変数群
        //とりあえず決め打ちの値を入れてください

        
        //指根本補正
        [SerializeField] private Vector3 _thumbPreEuler = Vector3.zero;

        //指先補正
        [SerializeField] private Vector3 _thumbAfterEuler = Vector3.zero;
       
        //#endregion

        public bool IsValid
        {
            get { return isValid; }
        }

        private bool isValid = false;

        private HI5_GloveStatus m_Status;
        private static int m_INDEX_Hand = (int) Bones.Hand;

        [SerializeField] private Vector3[] rawEulerAngles = new Vector3[21];

        [SerializeField] [Tooltip("手首の動きをどの程度反映させるか、磁気ノイズが多ければ0.5とかに減らす")] [Range(0, 1)]
        private float _handRotationWeight = 1.0f;

        [SerializeField] [Tooltip("Unity上でのTポーズ+手をまっすぐにするキャリブレーションのキー")]
        private KeyCode _calibrationKey = KeyCode.F2;

        [SerializeField] [Tooltip("上のキャリブレーションキーを押してから実行するまでの秒数。ワンオペなら4秒")]
        private float _calibrationWaitSecond = 4.0f;

        /// <summary>
        /// 元のスクリプトでは、モデルの手指の骨構造というか初期姿勢に凄く制約があった
        /// なので、ここに補正用の姿勢を格納する変数を追加して、Unity側でキャリブレーションを行い
        /// どんな手でも使えるようにした
        /// _coecifientQuaternions[0]は常にidentity
        /// _coecifientQuaternions[1]は手のひらの姿勢のグローバル座標系での補正姿勢
        /// _coecifientQuaternions[2]より後は、各指のボーンのローカル座標系での補正姿勢が入る
        /// </summary>
        private readonly List<Quaternion> _coecifientQuaternions = new List<Quaternion>();


        /// <summary>
        /// 手首の初期のlocalRotation、大体のモデルでidentityなので、まあとりあえず決め打ちで
        /// </summary>
        private Quaternion _handBoneDefaultRocalRotation = Quaternion.identity;

        new void OnEnable()
        {
            base.OnEnable();
            //補正変数を初期化
            for (int i = 0; i < (int) Bones.NumOfHI5Bones; i++)
            {
                _coecifientQuaternions.Add(Quaternion.identity);
            }
        }

        new void OnDisable()
        {
            base.OnDisable();
        }

        private void Start()
        {
            m_Status = HI5_Manager.GetGloveStatus();
        }

        /// <summary>
        /// HI5の値の反映部分はここ
        /// 元はUpdateで処理していましたが
        /// VRIKやIKinemaOrionのアレコレを考慮するとLateUpdateで指位置の適用を行う方が良いと判断して
        /// LateUpdateに変更しています。
        /// 物を掴む処理などの関係で、LateUpdateでは都合が悪い場合は
        /// Updateに戻して下さい
        /// </summary>
        void LateUpdate()
        {
            base.Update();


            if (Input.GetKeyDown(_calibrationKey))
            {
                Invoke("ResetBoneRotationToEuler", _calibrationWaitSecond);
            }

            //HI5が生きてないときは飛ばす！
            if (m_BindSource != null && HI5_Manager.IsConnected &&
                HI5_Manager.GetGloveStatus().Status == GloveStatus.BothGloveAvailable)
            {
                ApplyHandMotion_Rotation(m_BindSource);

                ApplyFingerMotion(m_BindSource);
            }
        }


        /// <summary>
        /// 無理矢理キャリブレーションするやつ。パーで手をまっすぐにしてから呼んでね
        /// たぶん、一度呼んだ時に両手が特定の向きを向くので、次は手をその向きに合わせて再度呼んで下さい。
        /// だいたい、Tポーズをして呼ぶ、で良いはず
        /// </summary>
        public void ResetBoneRotationToEuler()
        {
            if (m_BindSource == null)
            {
                return;
            }

            //i=0を飛ばすのは、ForeArmは常に0が返ってくるから計算しなくて良い為です
            for (int i = 1; i < 21; i++)
            {
                //Handは世界座標系での処理なので場合分け
                if (i == 1)
                {
                    var handQuaternionWithGlobal =
                        Quaternion.Euler(
                            HI5_DataTransform.ToUnityEulerAngles(
                                m_BindSource.GetReceivedRotation(m_INDEX_Hand, HandType)));

                    //from a(current) to b(identity) 
                    //relativeはinv(a)*b
                    Quaternion relative = Quaternion.Inverse(handQuaternionWithGlobal) * Quaternion.identity;
                    _coecifientQuaternions[1] = relative;
                }
                //各指の姿勢はローカル座標系で呼ばれる
                else
                {
                    Quaternion fingerQuaternionWithLocal =
                        Quaternion.Euler(m_BindSource.GetReceivedRotation(i, HandType));
                    if (!float.IsNaN(fingerQuaternionWithLocal.x) && !float.IsNaN(fingerQuaternionWithLocal.y) &&
                        !float.IsNaN(fingerQuaternionWithLocal.z) && !float.IsNaN(fingerQuaternionWithLocal.w))
                    {
                        //from a(current) to b(identity) 
                        //relativeはinv(a)*b
                        Quaternion relative = Quaternion.Inverse(fingerQuaternionWithLocal) * Quaternion.identity;
                        _coecifientQuaternions[i] = relative;
                    }
                    else
                    {
                        Debug.LogError("Invalid Quaternion please retry ");
                    }
                }
            }

            //親指はTポーズキャリブレーションの範囲から外すので2-4番目のcoefficientを無効にする
            _coecifientQuaternions[2] = _coecifientQuaternions[3] = _coecifientQuaternions[4] = Quaternion.identity;
            
            Debug.Log("TPose Calibration Complete!");
        }

        private void CheckIsValid()
        {
            bool isAvailable = m_Status.IsGloveAvailable(HandType);
            bool isBinded = HI5_BindInfoManager.IsGloveBinded(HandType);
            if (isAvailable && isBinded)
                isValid = true;
            else
                isValid = false;

            if (HI5_Calibration.IsCalibratingBPose)
                isValid = false;
        }


        private void ApplyFingerMotion(HI5_Source source)
        {
            for (int i = (m_INDEX_Hand + 1); i < (int) Bones.NumOfHI5Bones && i < HandBones.Length; i++)
            {
                if (HandBones[i] != null)
                {
                    SetRotation(HandBones, i, source.GetReceivedRotation(i, HandType));
                }
            }
        }


        private void ApplyHandMotion_Rotation(HI5_Source source)
        {
            if (HandBones[m_INDEX_Hand] != null)
            {
                Vector3 eulerRawData =
                    HI5_DataTransform.ToUnityEulerAngles(source.GetReceivedRotation(m_INDEX_Hand, HandType));


                //元の式と比べてcoefficient分だけ変更している
                HandBones[m_INDEX_Hand].rotation =
                    _coecifientQuaternions[1] *
                    Quaternion.Euler(eulerRawData);

                //手首の動きの反映度（つまり、磁気ノイズが強いときは0.2とかにすると、手首は演者さんの動かした手首角度の20%だけ動く
                //0%にしておくと、手首が固定されてる感出て違和感出ちゃうので、0.1は入れて置いた方が良いと思う。
                //TODO:HI5 SDKがメチャクチャつらい仕様(オイラー角しか取得できない)な為、
                //nフレーム目にx軸が179度になっててもn+1フレーム目にx軸が-179度になっていることがある
                //そうなると、Quaternion.Slerpしたとしてもx軸の値が20度から-20度にガクっと変わることがある
                //めちゃくちゃ違和感があるのでなんとかしたいんだけど、パッと思いつく手段はだめだった…
                if (_handRotationWeight < 0.99f)
                {
                    HandBones[m_INDEX_Hand].localRotation = Quaternion.Slerp(_handBoneDefaultRocalRotation,
                        HandBones[m_INDEX_Hand].localRotation, _handRotationWeight);
                }
            }
        }

        private void ApplyHandMotion_Position(Vector3 pos, Quaternion rot)
        {
            Vector3 offset = HandType == Hand.LEFT ? HI5_Manager.LeftOffset : HI5_Manager.RightOffset;
            Vector3 handPos = pos + rot * offset;

            if (HandBones[m_INDEX_Hand] != null)
            {
                HandBones[m_INDEX_Hand].localPosition = handPos;
            }
        }

        private void SetPosition(Transform[] bones, int bone, Vector3 position)
        {
            Transform t = bones[(int) bone];
            if (t != null)
            {
                Vector3 pos = position;
                if (!float.IsNaN(pos.x) && !float.IsNaN(pos.y) && !float.IsNaN(pos.z))
                {
                    t.localPosition = pos;
                }
            }
        }

        private void SetRotation(Transform[] bones, int bone, Vector3 rotation)
        {
            Transform t = bones[(int) bone];
            if (t != null)
            {
                Quaternion rot = Quaternion.Euler(rotation);
                if (!float.IsNaN(rot.x) && !float.IsNaN(rot.y) && !float.IsNaN(rot.z) && !float.IsNaN(rot.w))
                {
                    //元の式と比べてcoefficient分だけ変更している
                    rawEulerAngles[(int) bone] = rotation;
                    if (bone == 2)
                    {
                        t.localRotation = Quaternion.Euler(_thumbPreEuler) *
                                          _coecifientQuaternions[(int) bone] *
                                          rot * Quaternion.Euler(_thumbAfterEuler);
                    }
                    else
                    {
                        t.localRotation = _coecifientQuaternions[(int) bone] *
                                          rot;
                    }
                }
            }
        }

        /// <summary>
        /// モデルを変えたときに HandBone をセットし直すのが面倒だったため、自動的にアタッチしてくれる関数
        /// HandBones[0] に対象モデルのRoot（Animatorがアタッチされてるオブジェクト）を入れてから実行する。
        /// </summary>
        [ContextMenu("Automatic Set HandBone")]
        void AutomaticSetHandBone()
        {
            if (HandBones[0] == null)
            {
                Debug.LogError("HandBones[0] にモデルのRoot（Animatorとかあるオブジェクト）を入れてください。");
                return;
            }

            //Animator からボーン情報を持ってきたいので、Animator を取得
            var animator = HandBones[0].GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator が見つかりません。");
                return;
            }

            //念の為初期化
            HandBones = new Transform[(int) Bones.NumOfHI5Bones];

            //左手と右手で取得すべきボーンが違うから判定
            switch (HandType)
            {
                case Hand.LEFT:
                    HandBones[1] = animator.GetBoneTransform(HumanBodyBones.LeftHand);
                    HandBones[2] = animator.GetBoneTransform(HumanBodyBones.LeftThumbProximal);
                    HandBones[3] = animator.GetBoneTransform(HumanBodyBones.LeftThumbIntermediate);
                    HandBones[4] = animator.GetBoneTransform(HumanBodyBones.LeftThumbDistal);

                    HandBones[6] = animator.GetBoneTransform(HumanBodyBones.LeftIndexProximal);
                    HandBones[7] = animator.GetBoneTransform(HumanBodyBones.LeftIndexIntermediate);
                    HandBones[8] = animator.GetBoneTransform(HumanBodyBones.LeftIndexDistal);

                    HandBones[10] = animator.GetBoneTransform(HumanBodyBones.LeftMiddleProximal);
                    HandBones[11] = animator.GetBoneTransform(HumanBodyBones.LeftMiddleIntermediate);
                    HandBones[12] = animator.GetBoneTransform(HumanBodyBones.LeftMiddleDistal);

                    HandBones[14] = animator.GetBoneTransform(HumanBodyBones.LeftRingProximal);
                    HandBones[15] = animator.GetBoneTransform(HumanBodyBones.LeftRingIntermediate);
                    HandBones[16] = animator.GetBoneTransform(HumanBodyBones.LeftRingDistal);

                    HandBones[18] = animator.GetBoneTransform(HumanBodyBones.LeftLittleProximal);
                    HandBones[19] = animator.GetBoneTransform(HumanBodyBones.LeftLittleIntermediate);
                    HandBones[20] = animator.GetBoneTransform(HumanBodyBones.LeftLittleDistal);
                    break;

                case Hand.RIGHT:
                    HandBones[1] = animator.GetBoneTransform(HumanBodyBones.RightHand);
                    HandBones[2] = animator.GetBoneTransform(HumanBodyBones.RightThumbProximal);
                    HandBones[3] = animator.GetBoneTransform(HumanBodyBones.RightThumbIntermediate);
                    HandBones[4] = animator.GetBoneTransform(HumanBodyBones.RightThumbDistal);

                    HandBones[6] = animator.GetBoneTransform(HumanBodyBones.RightIndexProximal);
                    HandBones[7] = animator.GetBoneTransform(HumanBodyBones.RightIndexIntermediate);
                    HandBones[8] = animator.GetBoneTransform(HumanBodyBones.RightIndexDistal);

                    HandBones[10] = animator.GetBoneTransform(HumanBodyBones.RightMiddleProximal);
                    HandBones[11] = animator.GetBoneTransform(HumanBodyBones.RightMiddleIntermediate);
                    HandBones[12] = animator.GetBoneTransform(HumanBodyBones.RightMiddleDistal);

                    HandBones[14] = animator.GetBoneTransform(HumanBodyBones.RightRingProximal);
                    HandBones[15] = animator.GetBoneTransform(HumanBodyBones.RightRingIntermediate);
                    HandBones[16] = animator.GetBoneTransform(HumanBodyBones.RightRingDistal);

                    HandBones[18] = animator.GetBoneTransform(HumanBodyBones.RightLittleProximal);
                    HandBones[19] = animator.GetBoneTransform(HumanBodyBones.RightLittleIntermediate);
                    HandBones[20] = animator.GetBoneTransform(HumanBodyBones.RightLittleDistal);
                    break;

                default:
                    Debug.LogError("HandType が不正なものです。");
                    break;
            }
        }
    }
}