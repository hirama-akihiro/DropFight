using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DropFight.Games;
using DropFight.Games.Indicator;
using DropFight.Games.Players;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DropFight.Fader;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace DropFight.Scenes
{
    /// <summary>
    /// ゲーム場面
    /// </summary>
    public class GameScene : Scene
    {
        /// <summary>
        /// ゲームの制限時間
        /// </summary>
#if GAME_DEBUG
        public readonly TimeSpan LimitTime = new TimeSpan(0, 1, 30);
#else
        public readonly TimeSpan LimitTime = new TimeSpan(0,0,5);
#endif

        /// <summary>
        /// ゲームの経過時間を計測するタイマー
        /// </summary>
        private Stopwatch timer = new Stopwatch();

        /// <summary>
        /// ゲーム中かどうか
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return timer.IsRunning;
            }
        }

        /// <summary>
        /// ゲームが終了したかどうか
        /// </summary>
        public bool HasFinished
        {
            get
            {
                PlayerManager playerManager = ManagerSet.PlayerManager;
                IEnumerable<Player> players = playerManager.Players;
                IEnumerable<Player> alivePlayers = players.Where(player => !player.IsDead);
                if (alivePlayers.Count() <= 1)
                {
                    return true;
                }

                if (CurrentTime >= LimitTime)
                {
                    return true;
                }

                return false;
            }
        }
        
        /// <summary>
        /// ゲームの経過時間
        /// </summary>
        public TimeSpan CurrentTime
        {
            get
            {
                return timer.Elapsed;
            }
        }

        /// <summary>
        /// マネージャー一式
        /// </summary>
        public ManagerSet ManagerSet
        {
            get;
            private set;
        }

        /// <summary>
        /// カメラ
        /// </summary>
        private Camera camera = new Camera();

        private IndicatorManager indicatorManager;

        public CountDownDrawer countDown;
        
        public FinishIndicator finishIndicator;

        private ContentManager content;

		private FadeOut fadeout;

		private SoundEffect gameEndSE;
		private Song bgm;

        /// <param name="managerSet">マネージャー一式</param>
        public GameScene(ContentManager content, ManagerSet managerSet)
        {
            this.content = content;
            this.ManagerSet = managerSet;

            camera.FieldOfViewRadian = MathHelper.PiOver4;
            camera.Target = new Vector3(3.5f, 0, 4);
            camera.Position = new Vector3(3.5f, 9, 9);
            PlayerManager playerManager = managerSet.PlayerManager;
            indicatorManager = new IndicatorManager(playerManager, content);

            countDown = new CountDownDrawer(content);
            finishIndicator = new FinishIndicator(content);
			fadeout = new FadeOut(content);

			gameEndSE = content.Load<SoundEffect>("SE/gameend");
			bgm = content.Load<Song>("BGM/game2");
			MediaPlayer.Stop();
        }

        /// <summary>
        /// 更新する
        /// </summary>
        /// <returns>次の場面</returns>
        public Scene Update(GameTime gameTime,Input input)
        {
            // TODO カウントダウンに応じてスタートする
            if (countDown.Update() && !timer.IsRunning && !HasFinished)
            {
				MediaPlayer.Play(bgm);
                timer.Start();
            }
            
            GameInfo gameInfo = new GameInfo(this, gameTime);
            ManagerSet.Update(gameInfo, input);
            indicatorManager.Update(gameInfo);

            if (HasFinished)
            {
                if (timer.IsRunning)
                {
					MediaPlayer.Stop();
					gameEndSE.Play();
                    timer.Stop();
                }

                if (!finishIndicator.Update())
                {
                    // TODO クレジットシーンができたらコメントを外す
                    fadeout.StartFadeOut();
                }

				if (fadeout.EndFadeOut)
				{
					return new ResultScene(content, ManagerSet);
                }
				
				fadeout.Update();
            }


            // TODO
            return this;
        }

        /// <summary>
        /// 描画する
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            GraphicsDevice graphics = spriteBatch.GraphicsDevice;
            Viewport viewport = graphics.Viewport;
            camera.AspectRatio = (float)viewport.Width / (float)viewport.Height;

            // TODO 背景を描画する

            graphics.DepthStencilState = DepthStencilState.Default;
            ManagerSet.Draw(camera);

            indicatorManager.Draw(spriteBatch, camera);
            countDown.Draw(spriteBatch);

            if (HasFinished)
            {
                finishIndicator.Draw(spriteBatch);
            }

			fadeout.Draw(spriteBatch);
        }
    }
}
