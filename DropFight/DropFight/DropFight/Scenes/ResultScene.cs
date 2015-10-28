using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using DropFight.ModelUtils;
using System.Collections.Generic;
using DropFight.Games;
using DropFight.GameResult;
using Microsoft.Xna.Framework.Audio;
using DropFight.Fader;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace DropFight.Scenes
{
    /// <summary>
    /// ゲームの結果場面
    /// </summary>
    public class ResultScene : Scene
    {
		private ContentManager content;
		private ManagerSet managerSet;

		private Camera camera = new Camera();

		private List<Texture2D> imageList = new List<Texture2D>();
		private List<Vector2> imagePos = new List<Vector2>();

		private Stopwatch timer = new Stopwatch();
		private TimeSpan goNextSceneTime = new TimeSpan(0, 0, 3);

		private ResultDrawer resultDrawer;

		private FadeOut fadeout;
		private SoundEffect se;

		public ResultScene(ContentManager content,ManagerSet managerSet)
		{
			this.content = content;
			this.managerSet = managerSet;

			resultDrawer = new ResultDrawer(managerSet.PlayerManager, content);

			// カメラセッティング
			camera.FieldOfViewRadian = MathHelper.PiOver4;
			camera.Target = new Vector3(0.0f, 0.0f, 0.0f);
			camera.Position = new Vector3(0.0f, 0.0f, 5.0f);

			LoadContent(content);
			SetPos();

			// 時間測定開始
			timer.Start();

			fadeout = new FadeOut(content);
			se = content.Load<SoundEffect>("SE/fanfare");
			se.Play();
			se = content.Load<SoundEffect>("SE/ok");
		}

        /// <summary>
        /// 更新する
        /// </summary>
        /// <param name="gameTime">ゲーム内部の時間</param>
        /// <returns>次の場面</returns>
        public Scene Update(GameTime gameTime, Input input)
        {
			resultDrawer.Update(gameTime);
			
			fadeout.Update();
            bool isEnd = false;
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                //誰のどんなボタンでも開始できるようにする。
                if (isEnd) break;
                isEnd = input.PushABXY(index) || input.PushStart(index) || input.PushMouseLeftButton();
            }

            if (isEnd && timer.Elapsed > goNextSceneTime)
            {
                // フェードアウト開始
				se.Play();
				fadeout.StartFadeOut();
			}

			// フェードアウト終了後タイトルシーンに遷移
			if (fadeout.EndFadeOut)
			{
				return new TitleScene(content);
			}

            return this;
        }

        /// <summary>
        /// 描画する
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
			// テクスチャの描画
			for (int array = 0; array < imageList.Count; array++)
			{
				spriteBatch.Draw(imageList[array], imagePos[array] ,Color.White);
			}

			GraphicsDevice graphics = spriteBatch.GraphicsDevice;
			Viewport viewport = graphics.Viewport;
			camera.AspectRatio = (float)viewport.Width / (float)viewport.Height;
			graphics.DepthStencilState = DepthStencilState.Default;

			// コンテンツの描画
			resultDrawer.Draw(camera, spriteBatch);

			fadeout.Draw(spriteBatch);
        }

		/// <summary>
		/// 座標設定
		/// </summary>
		private void SetPos()
		{
			imagePos.Add(new Vector2(0.0f, 0.0f));
		}

		/// <summary>
		/// コンテンツ読み込み
		/// </summary>
		/// <param name="content"></param>
		private void LoadContent(ContentManager content)
		{
			imageList.Add(content.Load<Texture2D>("Scene/Result/Title"));
		}
    }
}
