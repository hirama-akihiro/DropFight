using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Diagnostics;
using DropFight.Games;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using DropFight.Fader;

namespace DropFight.Scenes
{
    /// <summary>
    /// タイトル場面
    /// </summary>
    public class TitleScene : Scene
    {
		ContentManager content;
		Texture2D backgroundTexture;
		Texture2D startgame1Texture;
		Texture2D startgame2Texture;
		SoundEffect se;
        bool isStart = false;
		Song bgm;
		FadeOut fadeout;
		private Stopwatch stopwatch = new Stopwatch();

		public TitleScene(ContentManager content){
			this.content = content;
			backgroundTexture = content.Load<Texture2D>("Scene/Title/title");
			startgame1Texture = content.Load<Texture2D>("Scene/Title/gamestart1");
			startgame2Texture = content.Load<Texture2D>("Scene/Title/gamestart2");
			se = content.Load<SoundEffect>("SE/ok");
			bgm = content.Load<Song>("BGM/op");
			fadeout = new FadeOut(content);
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Play(bgm);
			//ストップウォッチの初期化
			stopwatch.Reset();
		}

        /// <summary>
        /// 更新する
        /// </summary>
        /// <param name="gameTime">ゲーム内部の時間</param>
        /// <returns>次の場面</returns>
        public Scene Update(GameTime gameTime, Input input)
        {
            // TODO
			// Aボタン:Pressed→Released
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                //誰のどんなボタンでも開始できるようにする。
                if (isStart) break;
                isStart = input.PushABXY(index) || input.PushStart(index) ||input.PushMouseLeftButton();
            }
            if (isStart)
			{
				if (!fadeout.IsFadeOut)
				{
					fadeout.StartFadeOut();
					se.Play();
				}
			}

			if (!stopwatch.IsRunning)
			{
				stopwatch.Start();
			}

			fadeout.Update();

			if (stopwatch.ElapsedMilliseconds > 3000)
			{
				if (!fadeout.IsFadeOut)
				{
					fadeout.StartFadeOut();
				}
			}

			if (fadeout.EndFadeOut && stopwatch.ElapsedMilliseconds > 30000)
			{
				return new CreditScene(content);
			}

			if (fadeout.EndFadeOut)
			{
				return new SelectScene(content);
			}

			

			return this;
        }

        /// <summary>
        /// 描画する
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
			// 画面サイズ / 背景テクスチャサイズでテクスチャのスケールを統一する.
			Vector2 textureScale = new Vector2(
										(float)spriteBatch.GraphicsDevice.Viewport.Width / backgroundTexture.Width,
										(float)spriteBatch.GraphicsDevice.Viewport.Height / backgroundTexture.Height);
			// startgame[12]Textureの描画位置を画面中央少し下に設定.
			Vector2 startgameTexturePos = (new Vector2(spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height) - new Vector2(startgame1Texture.Width * textureScale.X, startgame1Texture.Height * textureScale.Y) + new Vector2(0, 200)) / 2;

			Rectangle startgameTextureRect = new Rectangle((int)startgameTexturePos.X, (int)startgameTexturePos.Y, (int)(startgame1Texture.Width * textureScale.X), (int)(startgame1Texture.Height * textureScale.Y));
            // TODO
			// 背景を描画
			spriteBatch.Draw(backgroundTexture,new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height), Color.White);

			// Aボタン:Released→Pressed or Pressed→Pressed
            if (isStart)
			{
				Console.WriteLine("0");
				spriteBatch.Draw(startgame2Texture, startgameTextureRect, Color.White);
			}
			else
			{
				spriteBatch.Draw(startgame1Texture, startgameTextureRect, Color.White);
			}

			fadeout.Draw(spriteBatch);
        }
    }
}
