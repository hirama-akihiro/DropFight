using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DropFight.Fader
{
	class FadeOut
	{
		private ContentManager content;

		private Texture2D background;

		// フェードアウト
		private float m_alpha;
		private float m_alphaIncAmout = 0.03f;

		/// <summary>
		/// フェードアウト処理が開始しているかどうか,True:開始中
		/// </summary>
		private bool m_isFadeOut = false;

		private Rectangle screenBound;
		private Color color;
		private float maxm_alpha = 1.0f;

		/// <summary>
		/// フェードアウト処理が終了したときTrue
		/// </summary>
		public bool EndFadeOut { get; set; }

		/// <summary>
		/// ゲームクリア:True,GameOver:False
		/// </summary>
		public bool ClearFlag { get; set; }

		/// <summary>
		/// フェードアウト管理クラス
		/// </summary>
		/// <param name="conent"></param>
		/// <param name="graphicsDevice"></param>
		public FadeOut(ContentManager content)
		{
			this.content = content;

			LoadContent();

			//フェードアウトの初期化
			m_alpha = 0.0f;

			//フェードアウト描画サイズ
			screenBound = new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight);
			//黒色でフェードアウト
			color = new Color(0.0f, 0.0f, 0.0f, m_alpha);
			EndFadeOut = false;
		}

		/// <summary>
		/// コンテンツ読み込み
		/// </summary>
		private void LoadContent()
		{
			background = content.Load<Texture2D>("Fader/BackGround");
		}

		internal void Draw(SpriteBatch spriteBatch)
		{
			//spriteBatch.Begin();
			//フェードアウト画面描画
			if (m_isFadeOut)
			{
				color = new Color(0.0f, 0.0f, 0.0f, m_alpha);
				spriteBatch.Draw(background, screenBound, color);
			}
			//spriteBatch.End();
		}

		internal void Update()
		{
			//フェードアウトの更新
			if (m_isFadeOut && !EndFadeOut)
			{
				updateFadeOut();
			}
		}

		/// <summary>
		/// フェードアウト中か否か
		/// </summary>
		/// <returns></returns>
		public bool IsFadeOut
		{
			get { return m_isFadeOut; }
		}

		/// <summary>
		/// ゲーム終了時に呼ばれるメソッド
		/// </summary>
		/// <param name="clearflag"></param>
		public void StartFadeOut(bool clearflag)
		{
			m_isFadeOut = true;
			ClearFlag = clearflag;
		}

		/// <summary>
		/// ゲーム終了時に呼ばれるメソッド
		/// </summary>
		public void StartFadeOut()
		{
			m_isFadeOut = true;
		}

		/// <summary>
		/// フェードアウト処理
		/// </summary>
		private void updateFadeOut()
		{
			m_alpha += m_alphaIncAmout;

			if (m_alpha >= maxm_alpha)
			{
				m_alpha = maxm_alpha;
				EndFadeOut = true;
			}
		}
	}
}
