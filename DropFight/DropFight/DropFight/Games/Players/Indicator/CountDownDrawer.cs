using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace DropFight.Games{
	/// <summary>
	/// ゲーム開始前の描画
    /// </summary>
	public class CountDownDrawer {
		// テクスチャ
		private Texture2D blackTexture;
		private Texture2D countTexture;
		private Texture2D circleTexture;
		// 領域
		private Rectangle[] countRect = new Rectangle[6];
		// 時間
		private Stopwatch stopwatch = new Stopwatch();
		// 音
		private SoundEffect countdownSE;
		private SoundEffect gamestartSE;
		// 音を再生する時間
		private double seStartTime;
		private double seEndTime;
		/// <summary>
		/// コンストラクト
		/// </summary>
		/// <param name="content">コンテントマネージャ</param>
		/// <param name="graphicsDevice">グラフィクスデバイス</param>
		public CountDownDrawer(ContentManager content) {
			// テクスチャロード
            blackTexture = content.Load<Texture2D>("Scene/Game/black");
			countTexture = content.Load<Texture2D>("Scene/Game/countdown");
			circleTexture = content.Load<Texture2D>("Scene/Game/waitCircle");

			// レクトアングル生成
			int numStep = countTexture.Width / 10;
			for (int i = 0; i < 5; i++) {
				countRect[i] = new Rectangle(numStep * i, 0, numStep, countTexture.Height);
			}
			countRect[5] = new Rectangle(numStep * 5, 0, numStep * 5, countTexture.Height);

			// 音ロード
			countdownSE = content.Load<SoundEffect>("SE/countdown");
			gamestartSE = content.Load<SoundEffect>("SE/gamestart");
			// 音を再生する時間
			seStartTime = 0;
			seEndTime = 4000;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <returns>trueで描画終了</returns>
		public bool Update() {
			if (!stopwatch.IsRunning) {
				stopwatch.Start();
			}
			if (stopwatch.ElapsedMilliseconds > 4000)
			{
				return stopwatch.ElapsedMilliseconds > 4000;
			}

			if (seStartTime < stopwatch.ElapsedMilliseconds && seStartTime < seEndTime)
			{
				if (seEndTime < seStartTime + 1000)
					gamestartSE.Play();
				else countdownSE.Play();

				seStartTime = stopwatch.ElapsedMilliseconds + 1000;
			}

			return stopwatch.ElapsedMilliseconds > 4000;
		}

		/// <summary>
		/// 描画
		/// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
			if (stopwatch.ElapsedMilliseconds > 4000) {
				return;
			}

            GraphicsDevice graphicsDevice = spriteBatch.GraphicsDevice;
            spriteBatch.Draw(blackTexture, graphicsDevice.Viewport.Bounds, Color.White);

            Viewport viewport = graphicsDevice.Viewport;
            int screenWidth = viewport.Width;
            int screenHeight = viewport.Height;

            if (stopwatch.ElapsedMilliseconds < 3000)
            {
                Point circleSize = new Point(400, 400);
                Rectangle circleRect = new Rectangle(screenWidth / 2, screenHeight / 2, circleSize.X, circleSize.Y);

                float rot = stopwatch.ElapsedMilliseconds / 1000.0f * MathHelper.Pi * 2.0f;
                spriteBatch.Draw(circleTexture, circleRect, null, Color.White, rot, new Vector2(circleTexture.Width / 2, circleTexture.Height / 2), SpriteEffects.None, 0.0f);
                int time = (int)(stopwatch.ElapsedMilliseconds / 1000);
                spriteBatch.Draw(countTexture, new Vector2(screenWidth / 2 - countRect[0].Width / 2, screenHeight / 2 - countRect[0].Height / 2), countRect[time + 2], Color.White);
            }
            else
            {
                spriteBatch.Draw(countTexture, new Vector2(screenWidth / 2 - countRect[5].Width / 2, screenHeight / 2 - countRect[5].Height / 2), countRect[5], Color.White);
            }
		}
	}
}
