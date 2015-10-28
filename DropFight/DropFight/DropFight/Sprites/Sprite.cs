using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.Sprites
{
    /// <summary>
    /// スプライト
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// フレーム
        /// </summary>
        private class Frame
        {
            /// <summary>
            /// テクスチャ
            /// </summary>
            public Texture2D Texture
            {
                get;
                private set;
            }
            
            /// <summary>
            /// 描画する領域
            /// </summary>
            public Rectangle Region
            {
                get;
                private set;
            }

            /// <summary>
            /// 1フレームの最短時間
            /// </summary>
            public TimeSpan Interval
            {
                get;
                private set;
            }

            /// <param name="texture">テクスチャ</param>
            /// <param name="region">描画する領域</param>
            /// <param name="interval">1フレームの最短時間</param>
            public Frame(Texture2D texture, Rectangle region, TimeSpan interval)
            {
                this.Texture = texture;
                this.Region = region;
                this.Interval = interval;
            }
        }

        /// <summary>
        /// 描画に使用する色
        /// </summary>
        public Color Color = Color.White;

        /// <summary>
        /// 角度(ラジアン)
        /// </summary>
        public float AngleRadian = 0;

        /// <summary>
        /// 拡大率
        /// </summary>
        public Vector2 Scale = Vector2.One;

        /// <summary>
        /// フレームのリスト
        /// </summary>
        private List<Frame> frames = new List<Frame>();

        /// <summary>
        /// タイマー
        /// </summary>
        private Stopwatch timer = new Stopwatch();

        /// <summary>
        /// 現在のフレームの番号
        /// </summary>
        public int CurrentFrameIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// 現在のフレーム
        /// </summary>
        private Frame CurrentFrame
        {
            get
            {
                return frames[CurrentFrameIndex];
            }
        }

        public Sprite()
        {
            ResetWithoutStop();
        }

        /// <summary>
        /// フレームを追加する
        /// フレームを分割する場合、左から右、上から下の順に追加する
        /// </summary>
        /// <param name="texture">テクスチャ</param>
        /// <param name="entireRegion">追加するテクスチャの全領域</param>
        /// <param name="interval">各フレームの最短時間</param>
        /// <param name="rows">全領域のx軸方向の分割数</param>
        /// <param name="columns">全領域のy軸方向の分割数</param>
        public void AddFrame(Texture2D texture, Rectangle entireRegion, TimeSpan interval, int rows = 1, int columns = 1)
        {
            int width = entireRegion.Width / rows;
            int height = entireRegion.Height / columns;
            for (int y = entireRegion.Y; y <= entireRegion.Bottom; y += height)
            {
                for (int x = entireRegion.X; x <= entireRegion.Right; x += width)
                {
                    Rectangle region = new Rectangle(x, y, width, height);
                    Frame frame = new Frame(texture, region, interval);
                    frames.Add(frame);
                }
            }
        }

        /// <summary>
        /// アニメーションを止める
        /// </summary>
        public void Stop()
        {
            timer.Stop();
        }

        /// <summary>
        /// アニメーションを止めずに最初のフレームに戻す
        /// </summary>
        public void ResetWithoutStop()
        {
            CurrentFrameIndex = 0;
        }

        /// <summary>
        /// アニメーションを始める
        /// </summary>
        public void Start()
        {
            timer.Start();
        }

        /// <summary>
        /// 更新する
        /// </summary>
        public void Update()
        {
            if (timer.IsRunning && timer.Elapsed >= CurrentFrame.Interval)
            {
                CurrentFrameIndex++;
                CurrentFrameIndex = CurrentFrameIndex % frames.Count;

                timer.Restart();
            }
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="centerPosition">中心の座標</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 centerPosition)
        {
            Rectangle region = CurrentFrame.Region;
            Vector2 origin = new Vector2(region.Width / 2, region.Height / 2);
            spriteBatch.Draw(CurrentFrame.Texture, centerPosition, region, Color, AngleRadian, origin, Scale, SpriteEffects.None, 0);
        }
    }
}
