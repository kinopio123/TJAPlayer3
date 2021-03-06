﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using FDK;

namespace DTXMania
{
    internal class CAct演奏DrumsRunner : CActivity
    {
        /// <summary>
        /// ランナー
        /// </summary>
        public CAct演奏DrumsRunner()
        {
            base.b活性化してない = true;
        }

        // ランナー画像のサイズ。 X, Y
        private int[] Size = CDTXMania.Skin.Game_Runner_Size;
        // ランナーのコマ数
        private int Ptn = CDTXMania.Skin.Game_Runner_Ptn;
        // ランナーのキャラクターのバリエーション(ミス時を含まない)。
        private int Type = CDTXMania.Skin.Game_Runner_Type;
        // スタート地点のX座標 1P, 2P
        private int[] StartPoint_X = CDTXMania.Skin.Game_Runner_StartPoint_X;
        // スタート地点のY座標 1P, 2P
        private int[] StartPoint_Y = CDTXMania.Skin.Game_Runner_StartPoint_Y;

        public void Start(int Player, bool IsMiss, CDTX.CChip pChip)
        {
            if (CDTXMania.Tx.Runner != null)
            {
                for (int i = 0; i < 128; i++)
                {
                    if (pChip.nチャンネル番号 < 0x15 || (pChip.nチャンネル番号 >= 0x1A))
                    {
                        if (!stRunners[i].b使用中)
                        {
                            stRunners[i].b使用中 = true;
                            stRunners[i].nPlayer = Player;
                            if (IsMiss == true)
                            {
                                stRunners[i].nType = 0;
                            }
                            else
                            {
                                stRunners[i].nType = random.Next(1, Type + 1);
                            }
                            stRunners[i].ct進行 = new CCounter(0, 1280, 16, CDTXMania.Timer);
                            stRunners[i].nOldValue = 0;
                            stRunners[i].fX = 0;
                            break;
                        }
                    }
                }
            }
        }

        public override void On活性化()
        {
            for (int i = 0; i < 128; i++)
            {
                stRunners[i] = new STRunner();
                stRunners[i].b使用中 = false;
                stRunners[i].ct進行 = new CCounter();
            }
            base.On活性化();
        }

        public override void On非活性化()
        {
            for (int i = 0; i < 128; i++)
            {
                stRunners[i].ct進行 = null;
            }
            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
        {
            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            for (int i = 0; i < 128; i++)
            {
                if (stRunners[i].b使用中)
                {
                    stRunners[i].nOldValue = stRunners[i].ct進行.n現在の値;
                    stRunners[i].ct進行.t進行();
                    if (stRunners[i].ct進行.b終了値に達した || stRunners[i].fX > 1280)
                    {
                        stRunners[i].ct進行.t停止();
                        stRunners[i].b使用中 = false;
                    }
                    if (stRunners[i].nOldValue < stRunners[i].ct進行.n現在の値)
                    {
                        stRunners[i].fX += (float)CDTXMania.stage演奏ドラム画面.actPlayInfo.dbBPM / 18;
                        int Width = 1280 / Ptn;
                        stRunners[i].nNowPtn = (int)stRunners[i].fX / Width;
                    }
                    if (CDTXMania.Tx.Runner != null)
                    {
                        if (stRunners[i].nPlayer == 0)
                        {
                            CDTXMania.Tx.Runner.t2D描画(CDTXMania.app.Device, (int)(StartPoint_X[0] + stRunners[i].fX), StartPoint_Y[0], new Rectangle(stRunners[i].nNowPtn * Size[0], stRunners[i].nType * Size[1], Size[0], Size[1]));
                        }
                        else
                        {
                            CDTXMania.Tx.Runner.t2D描画(CDTXMania.app.Device, (int)(StartPoint_X[1] + stRunners[i].fX), StartPoint_Y[1], new Rectangle(stRunners[i].nNowPtn * Size[0], stRunners[i].nType * Size[1], Size[0], Size[1]));
                        }
                    }
                }
            }
            return base.On進行描画();
        }

        #region[ private ]
        //-----------------
        [StructLayout(LayoutKind.Sequential)]
        private struct STRunner
        {
            public bool b使用中;
            public int nPlayer;
            public int nType;
            public int nOldValue;
            public int nNowPtn;
            public float fX;
            public CCounter ct進行;
        }
        private STRunner[] stRunners = new STRunner[128];
        Random random = new Random();
        //-----------------
        #endregion
    }
}