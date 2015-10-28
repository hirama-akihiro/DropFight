using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using DropFight.Games.Players;
using System;

namespace DropFight.Scenes
{
    /// <summary>
    /// ゲーム時のクレジット場面
    ///時間的に厳しいのでufobattleの物を流用します
    /// </summary>
    public class CreditScene : Scene
    {
        // 背景
        private readonly Texture2D background;
        private readonly Vector2 backPos = new Vector2(0, 0);

        //画像
        private List<Texture2D> imageList = new List<Texture2D>();


        // 画像位置
        private Vector2 creditMov = new Vector2(0.0f, -1.0f);              // クレジット移動距離
        private readonly Vector2 creditSpeed = new Vector2(0.0f, -1.5f);  // クレジット移動速度
        //private readonly Vector2 creditSpeed = new Vector2(0.0f, -5.0f);
        private readonly Vector2[] imagePos = {
            new Vector2(0, 1600), //title
            new Vector2(0, 1900), //staff
            new Vector2(0, 2100), //mainProgramer
            new Vector2(0, 2200), //takenami
            new Vector2(0, 2300), //tsuji
            new Vector2(0, 2500), //3DModeling
            new Vector2(0, 2600), //hirama
            new Vector2(0, 2800), //ProgrammingSupport
            new Vector2(0, 2900), //kawai
            new Vector2(0, 3000), //suzuki
            new Vector2(0, 3300), //materialProvided
            new Vector2(0, 3500), //mp1name
            new Vector2(0, 3600), //mp1url
            new Vector2(0, 3800), //mp2name
            new Vector2(0, 3900), //mp2url
            new Vector2(0, 4100), //mp3name
            new Vector2(0, 4200), //mp3url
            new Vector2(0, 4400), //mp4name
            new Vector2(0, 4500), //mp4url
            new Vector2(0, 4700), //mp5name
            new Vector2(0, 4800), //mp5url
            new Vector2(0, 5100), //speciallthanks
            new Vector2(0, 5300), //yamazaki
            new Vector2(0, 5400), //koide
            new Vector2(0, 5800), //ASD
            new Vector2(0, 5900), //cclubtut
        };

        // 時間
        private Stopwatch stopwatch = new Stopwatch();

        // 画像スケール調整
        private Vector2 scale;

        // フェードイン&アウト
        private float m_alpha;
        private float m_alphaIncAmout;
        private bool m_isFadeIn = true;
        private bool m_isFadeOut = false;
		private bool isStart;
        private Rectangle screenBound;
        private Color color;
        private ContentManager content;
        public CreditScene(ContentManager content)
        {
            this.content=content;
            // フェードアウトの初期化
            m_alpha = 1.0f;
            m_alphaIncAmout = 0.008f;
            screenBound = new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight); // フェードアウト描画サイズ
            color = new Color(0.0f, 0.0f, 0.0f, m_alpha);   // 黒色でフェードアウト

            // ストップウォッチの初期化
            stopwatch.Reset();

            //画像
            background = content.Load<Texture2D>("Scene/Credit/back");
            imageList.Add(content.Load<Texture2D>("Scene/Credit/title"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/staff"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/mainProgram"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/takenami"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/tsuji"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/3Dmodeling"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/hirama"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/programmingsupport"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/kawai"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/suzuki"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/materialProvided"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/mp1name"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/mp1url"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/mp2name"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/mp2url"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/mp3name"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/mp3url"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/mp4name"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/mp4url"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/mp5name"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/mp5url"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/specialthanks"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/yamazaki"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/koide"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/asd"));
            imageList.Add(content.Load<Texture2D>("Scene/Credit/cclubtut"));



            scale = new Vector2((float)Game1.ScreenWidth / background.Width, (float)Game1.ScreenHeight / background.Height);

        }

        /// <summary>
        /// 更新する
        /// </summary>
        /// <param name="gameTime">ゲーム内部の時間</param>
        /// <returns>次の場面</returns>
        public Scene Update(GameTime gameTime,Input input)
        {
            // TODO
			foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
			{
				//誰のどんなボタンでも開始できるようにする。
				if (isStart) break;
				isStart = input.PushABXY(index) || input.PushStart(index) || input.PushMouseLeftButton();
			}

            if (!stopwatch.IsRunning)
            {
                stopwatch.Start();
            }

            if (stopwatch.ElapsedMilliseconds > 1 && m_isFadeOut == false)
            {
                creditMov = Vector2.Add(creditMov, creditSpeed);
                stopwatch.Restart();
            }

            if (m_isFadeIn == true && creditMov.Y < -200.0f)
            {
                m_isFadeIn = false;
            }

            // ある時間を超えるとフェードアウト処理に移行
            if (m_isFadeOut == false && creditMov.Y < -6200.0f)
            {
                m_isFadeOut = true;
                stopwatch.Restart();
            }

            if (m_isFadeOut == true && stopwatch.ElapsedMilliseconds > 5000)
            {
                stopwatch.Stop();
                return new TitleScene(content);
                Console.WriteLine("a");

            }
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                if (input.PushABXY(index) || input.PushStart(index))
                {
                    return new TitleScene(content);
                }
            }

            return this;
        }

        /// <summary>
        /// 描画する
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            // TODO
            // フェードアウト描画
            //spriteBatch.Draw(background, ScalingRect(background.Bounds, Vector2.Add(titlePos, creditMov)), Color.White);
            spriteBatch.Draw(background, ScalingRect(background.Bounds, backPos), Color.White);
            for (int i = 0; i < imageList.Count; i++)
            {
                spriteBatch.Draw(imageList[i], ScalingRect(imageList[i].Bounds, Vector2.Add(imagePos[i], creditMov)), Color.White);
            }

            if (m_isFadeIn)
            {
                color = new Color(0.0f, 0.0f, 0.0f, m_alpha);
                spriteBatch.Draw(background, screenBound, color);
                updateFadeIn();
            }
            else if (m_isFadeOut)
            {
                color = new Color(0.0f, 0.0f, 0.0f, m_alpha);
                spriteBatch.Draw(background, screenBound, color);
                updateFadeOut();
            }
        }

        /// <summary>フェードイン処理</summary>
        private void updateFadeIn()
        {
            m_alpha -= m_alphaIncAmout;
            if (m_alpha <= 0.0f)
            {
                m_alpha = 0.0f;
            }
        }

        /// <summary>フェードアウト処理</summary>
        private void updateFadeOut()
        {
            m_alpha += m_alphaIncAmout;
            if (m_alpha >= 1.0f)
            {
                m_alpha = 1.0f;
            }
        }

        /// <summary>
        /// 良い感じにスケーリングしてくれ
        /// </summary>
        /// <param name="rect">width,heightだけ見る</param>
        /// <param name="position">位置</param>
        /// <returns>GameMain.ScreenWidthの大きさに合したRect</returns>
        private Rectangle ScalingRect(Rectangle rect, Vector2 position)
        {
            return new Rectangle((int)(position.X * scale.X), (int)(position.Y * scale.Y),
                (int)(rect.Width * scale.X), (int)(rect.Height * scale.Y));
        }
    }
}
