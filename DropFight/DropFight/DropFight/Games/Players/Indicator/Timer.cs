using System;
using System.Diagnostics;
using DropFight.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.Games.Indicator
{
	public class Timer
	{
		private Stopwatch stopwatch = new Stopwatch();

		private readonly Texture2D textureTime;
		private readonly Vector2 drawPosition;

		private readonly Texture2D textureBack;

		private int minutes;
		private int seconds;

		public Timer(ContentManager content)
		{

			textureTime = content.Load<Texture2D>("Scene/Game/time");
            textureBack = content.Load<Texture2D>("Scene/Game/timerback");

            drawPosition = new Vector2(0, 1200 - textureBack.Height);

		}

		public void Pause()
		{
			stopwatch.Stop();
		}

		public void Start()
		{
			stopwatch.Start();
		}

        /// <summary>
        /// 設定した時間が過ぎていたらtrue
        /// </summary>
        /// <returns></returns>
		public void Update(GameInfo gameInfo)
		{
			if (!stopwatch.IsRunning)
			{
				stopwatch.Start();
			}

            TimeSpan restTime = gameInfo.RestTime;
			minutes = restTime.Minutes;
			seconds = restTime.Seconds;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
            spriteBatch.ScalingDraw(textureBack, new Vector2(drawPosition.X, drawPosition.Y - 20), 1.0f, Color.White);
			DrawNumber(spriteBatch, minutes % 10, drawPosition + new Vector2(34, -4) + new Vector2(29, 0) );
			spriteBatch.ScalingDraw(textureTime, drawPosition + new Vector2(34, -4) + new Vector2(29, 0) + new Vector2(27, 0) + new Vector2(10, 0), 1.0f, new Rectangle(270, 0, 13, 29), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
			DrawNumber(spriteBatch, seconds / 10 % 10, drawPosition + new Vector2(34, -4) + new Vector2(29, 0) + new Vector2(27, 0) + new Vector2(10, 0) + new Vector2(13, 0) + new Vector2(10, 0));
			DrawNumber(spriteBatch, seconds % 10, drawPosition + new Vector2(34, -4) + new Vector2(29, 0) + new Vector2(27, 0) * 2 + new Vector2(10, 0) + new Vector2(13, 0) + new Vector2(10, 0));
		}

		/// <summary>
		/// タイムの数字を表示
		/// </summary>
		/// <param name="spriteBatch"></param>
		private void DrawNumber(SpriteBatch spriteBatch, int number, Vector2 position)
		{
			spriteBatch.ScalingDraw(textureTime, position, 1.0f, new Rectangle(27 * number, 0, 27, 29), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
		}

    }
}
