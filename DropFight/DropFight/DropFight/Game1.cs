using System;
using DropFight.Games;
using DropFight.Games.Attacks;
using DropFight.Games.Blocks;
using DropFight.Games.Players;
using DropFight.ModelUtils;
using DropFight.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DropFight
{
    /// <summary>
    /// 基底 Game クラスから派生した、ゲームのメイン クラスです。
    /// </summary>
    public class Game1 : Game
    {
        private SpriteBatch spriteBatch;

        /// <summary>
        /// 現在の場面
        /// </summary>
        private Scene scene;
        private Input input;
        [Obsolete]
        private static readonly int defaultScreenWidth = 1600;

        /// <summary>
        /// スクリーンの幅
        /// </summary>
        [Obsolete]
        public static readonly int ScreenWidth = 800;
        // public static readonly int ScreenWidth = defaultScreenWidth;

        [Obsolete]
        public static readonly float ScreenWidthScale = (float)ScreenWidth / (float)defaultScreenWidth;

        [Obsolete]
        private static readonly int defaultScreenHeight = 1200;

        /// <summary>
        /// スクリーンの高さ
        /// </summary>
        [Obsolete]
        public static readonly int ScreenHeight = 600;
        // public static readonly int ScreenHeight = defaultScreenHeight;

        [Obsolete]
        public static readonly float ScreenHeightScale = (float)ScreenHeight / (float)defaultScreenHeight;

        public Game1()
        {
            // ウィンドウの大きさを変更する
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            input = new Input(this);
            Content.RootDirectory = "Content";
			graphics.ToggleFullScreen();
			
        }

        /// <summary>
        /// ゲームが実行を開始する前に必要な初期化を行います。
        /// ここで、必要なサービスを照会して、関連するグラフィック以外のコンテンツを
        /// 読み込むことができます。base.Initialize を呼び出すと、使用するすべての
        /// コンポーネントが列挙されるとともに、初期化されます。
        /// </summary>
        protected override void Initialize()
        {
            // TODO: ここに初期化ロジックを追加します。
            
			//scene = new GameScene(Content, managerSet);
			//scene = new CreditScene(Content, controller1);
            //scene = new SelectScene(Content);
			scene = new TitleScene(Content);
			//scene = new ResultScene(Content);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent はゲームごとに 1 回呼び出され、ここですべてのコンテンツを
        /// 読み込みます。
        /// </summary>
        protected override void LoadContent()
        {
            // 新規の SpriteBatch を作成します。これはテクスチャーの描画に使用できます。
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: this.Content クラスを使用して、ゲームのコンテンツを読み込みます。
        }

        /// <summary>
        /// UnloadContent はゲームごとに 1 回呼び出され、ここですべてのコンテンツを
        /// アンロードします。
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: ここで ContentManager 以外のすべてのコンテンツをアンロードします。
        }

        /// <summary>
        /// ワールドの更新、衝突判定、入力値の取得、オーディオの再生などの
        /// ゲーム ロジックを、実行します。
        /// </summary>
        /// <param name="gameTime">ゲームの瞬間的なタイミング情報</param>
        protected override void Update(GameTime gameTime)
        {
            // ゲームの終了条件をチェックします。
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: ここにゲームのアップデート ロジックを追加します。
            input.Update(gameTime);
            scene = scene.Update(gameTime, input);

            base.Update(gameTime);
        }

        /// <summary>
        /// ゲームが自身を描画するためのメソッドです。
        /// </summary>
        /// <param name="gameTime">ゲームの瞬間的なタイミング情報</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: ここに描画コードを追加します。
            spriteBatch.Begin();
            scene.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
