﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using FDK;

namespace DTXMania
{
    internal class CAct演奏DrumsMob : CActivity
    {
        /// <summary>
        /// 踊り子
        /// </summary>
        public CAct演奏DrumsMob()
        {
            base.b活性化してない = true;
        }

        public override void On活性化()
        {
            ctMob = new CCounter();
            ctMobPtn = new CCounter();
            base.On活性化();
        }

        public override void On非活性化()
        {
            ctMob = null;
            ctMobPtn = null;
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
            if(!CDTXMania.stage演奏ドラム画面.bDoublePlay)
            {
                if (ctMob != null) ctMob.t進行LoopDb();
                if (ctMobPtn != null) ctMobPtn.t進行LoopDb();

                //CDTXMania.act文字コンソール.tPrint(0, 0, C文字コンソール.Eフォント種別.白, ctMob.db現在の値.ToString());
                //CDTXMania.act文字コンソール.tPrint(0, 10, C文字コンソール.Eフォント種別.白, Math.Sin((float)this.ctMob.db現在の値 * (Math.PI / 180)).ToString());

                if (CDTXMania.stage演奏ドラム画面.actGauge.db現在のゲージ値[0] >= 100)
                {
                    if (CDTXMania.Tx.Mob[(int)ctMobPtn.db現在の値] != null)
                    {
                        CDTXMania.Tx.Mob[(int)ctMobPtn.db現在の値].t2D描画(CDTXMania.app.Device, 0, (720 - (CDTXMania.Tx.Mob[0].szテクスチャサイズ.Height - 70)) + -((float)Math.Sin((float)this.ctMob.db現在の値 * (Math.PI / 180)) * 70));
                    }
                }
            }
            return base.On進行描画();
        }
        #region[ private ]
        //-----------------
        public CCounter ctMob;
        public CCounter ctMobPtn;
        //-----------------
        #endregion
    }
}
