using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace DropFight
{
    /// <summary>
    /// IUpdateable インターフェイスを実装したゲーム コンポーネントです。
    /// 全シーンで使う
    /// </summary>
    public class Input : Microsoft.Xna.Framework.GameComponent
    {
        /// <summary>
        /// 現在のコントローラの状態
        /// </summary>
        private GamePadState[] nowGamePad = new GamePadState[4];
        /// <summary>
        /// 1フレーム前のコントローラの状態
        /// </summary>
        private GamePadState[] previousGamePad = new GamePadState[4];
        /// <summary>
        /// 現在のキーボードの状態
        /// </summary>
        private KeyboardState nowKeyboard;
        /// <summary>
        /// 1フレーム前のキーボードの状態
        /// </summary>
        private KeyboardState previousKeyboard;
        /// 現在のマウス状態
        /// </summary>
        private MouseState nowMouse;
        /// <summary>
        /// 1フレーム前のマウス状態
        /// </summary>
        private MouseState previousMouse;

        public Input(Game game)
            : base(game)
        {
        }

        /// <summary>
        /// ゲーム コンポーネントの初期化を行います。
        /// ここで、必要なサービスを照会して、使用するコンテンツを読み込むことができます。
        /// </summary>
        public override void Initialize()
        {
            // TODO: ここに初期化のコードを追加します。
            for (int i = 0; i < 4; i++)
                nowGamePad[i] = GamePad.GetState(PlayerIndex.One + i);

            nowKeyboard = Keyboard.GetState();
            nowMouse = Mouse.GetState();
            previousMouse = Mouse.GetState();
            base.Initialize();
        }

        /// <summary>
        /// ゲーム コンポーネントが自身を更新するためのメソッドです。
        /// </summary>
        /// <param name="gameTime">ゲームの瞬間的なタイミング情報</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: ここにアップデートのコードを追加します。
            for (int i = 0; i < 4; i++)
            {
                previousGamePad[i] = nowGamePad[i];
                nowGamePad[i] = GamePad.GetState(PlayerIndex.One + i);
            }

            previousKeyboard = nowKeyboard;
            nowKeyboard = Keyboard.GetState();
            previousMouse = nowMouse;
            nowMouse = Mouse.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// ゲームパッドの接続の有無
        /// </summary>
        public bool GamePadConnect(PlayerIndex index)
        {
            int i = index - PlayerIndex.One;
            return nowGamePad[i].IsConnected;
        }
        
        /// <summary>
        /// 左スティック値を返す(Y)
        /// </summary>
        /// <param name="index">プレイヤーのインデッスク</param>
        /// <returns>左スティックの値</returns>
        public Vector2 LeftStick(PlayerIndex index)
        {
			if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
				return -Vector2.UnitX;
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
				return Vector2.UnitX;
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
				return Vector2.UnitY;
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
				return -Vector2.UnitY;
			}
			
			if (index < PlayerIndex.One || index > PlayerIndex.Four)
                return Vector2.Zero;

            int i = index - PlayerIndex.One;
            if (!nowGamePad[i].IsConnected)
                return Vector2.Zero;

            return nowGamePad[i].ThumbSticks.Left;

        }

        /// <summary>
        /// 右スティック値を返す(Y)
        /// </summary>
        /// <param name="index">プレイヤーのインデッスク</param>
        /// <returns>右スティックの値</returns>
        public double RightStick(PlayerIndex index)
        {
            if (index < PlayerIndex.One || index > PlayerIndex.Four)
                return 0;

            int i = index - PlayerIndex.One;
            if (!nowGamePad[i].IsConnected)
                return 0;

            return nowGamePad[i].ThumbSticks.Right.Y;

        }

        /// <summary>
        /// 左トリガーが押されたか
        /// </summary>
        /// <param name="index">プレイヤーのインデックス</param>
        /// <returns>押されたらtrue、押されていないまたは押しっぱなしならfalse</returns>
        public bool LeftTrigger(PlayerIndex index)
        {
            if (index < PlayerIndex.One || index > PlayerIndex.Four)
                return false;

            int i = index - PlayerIndex.One;
            return nowGamePad[i].IsConnected && previousGamePad[i].IsConnected
                && nowGamePad[i].Triggers.Left > 0 && previousGamePad[i].Triggers.Left == 0;
        }

        /// <summary>
        /// 右トリガーが押されたか
        /// </summary>
        /// <param name="index">プレイヤーのインデックス</param>
        /// <returns>押されたらtrue、押されていないまたは押しっぱなしならfalse</returns>
        public bool RightTrigger(PlayerIndex index)
        {
            if (index < PlayerIndex.One || index > PlayerIndex.Four)
                return false;

            int i = index - PlayerIndex.One;
            return nowGamePad[i].IsConnected && previousGamePad[i].IsConnected
                && nowGamePad[i].Triggers.Right > 0 && previousGamePad[i].Triggers.Right == 0;
        }

        public bool A(PlayerIndex index)
        {
            if (index < PlayerIndex.One || index > PlayerIndex.Four)
                return false;

            int i = index - PlayerIndex.One;
            return nowGamePad[i].IsConnected && nowGamePad[i].Buttons.A == ButtonState.Pressed;
        }

        public bool B(PlayerIndex index)
        {
            if (index < PlayerIndex.One || index > PlayerIndex.Four)
                return false;

            int i = index - PlayerIndex.One;
            return nowGamePad[i].IsConnected && nowGamePad[i].Buttons.B == ButtonState.Pressed;
        }

        /// <summary>
        /// ABXYのどれかのボタンが押されたか
        /// </summary>
        /// <param name="index">プレイヤーのインデックス</param>
        /// <returns>押されたらtrue、押されていないまたは押しっぱなしならfalse</returns>
        public bool PushABXY(PlayerIndex index)
        {
            if (previousKeyboard.IsKeyUp(Keys.A) && nowKeyboard.IsKeyDown(Keys.A))
            {
                return true;
            }
            if (previousKeyboard.IsKeyUp(Keys.B) && nowKeyboard.IsKeyDown(Keys.B))
            {
                return true;
            }
            if (previousKeyboard.IsKeyUp(Keys.X) && nowKeyboard.IsKeyDown(Keys.X))
            {
                return true;
            }
            if (previousKeyboard.IsKeyUp(Keys.Y) && nowKeyboard.IsKeyDown(Keys.Y))
            {
                return true;
            }

            if (index < PlayerIndex.One || index > PlayerIndex.Four)
                return false;

            int i = index - PlayerIndex.One;
            return nowGamePad[i].IsConnected && previousGamePad[i].IsConnected
                && ((nowGamePad[i].Buttons.A == ButtonState.Pressed && previousGamePad[i].Buttons.A != ButtonState.Pressed) 
                || (nowGamePad[i].Buttons.X == ButtonState.Pressed && previousGamePad[i].Buttons.X != ButtonState.Pressed)
                ||(nowGamePad[i].Buttons.Y == ButtonState.Pressed && previousGamePad[i].Buttons.Y != ButtonState.Pressed) 
                || (nowGamePad[i].Buttons.B == ButtonState.Pressed && previousGamePad[i].Buttons.B != ButtonState.Pressed));
        }

        /// <summary>
        /// Aボタンが押されたか
        /// </summary>
        /// <param name="index">プレイヤーのインデックス</param>
        /// <returns>押されたらtrue、押されていないまたは押しっぱなしならfalse</returns>
        public bool PushA(PlayerIndex index)
        {
			if ( previousKeyboard.IsKeyUp(Keys.A) && nowKeyboard.IsKeyDown(Keys.A)) {
				return true;
			}

            if (index < PlayerIndex.One || index > PlayerIndex.Four)
                return false;

            int i = index - PlayerIndex.One;
            return nowGamePad[i].IsConnected && previousGamePad[i].IsConnected
                && nowGamePad[i].Buttons.A == ButtonState.Pressed && previousGamePad[i].Buttons.A != ButtonState.Pressed;
        }
        /// <summary>
        /// Xボタンが押されたか
        /// </summary>
        /// <param name="index">プレイヤーのインデックス</param>
        /// <returns>押されたらtrue、押されていないまたは押しっぱなしならfalse</returns>
        public bool PushX(PlayerIndex index)
        {
            if (index < PlayerIndex.One || index > PlayerIndex.Four)
                return false;

            int i = index - PlayerIndex.One;
            return nowGamePad[i].IsConnected && previousGamePad[i].IsConnected
                && nowGamePad[i].Buttons.X == ButtonState.Pressed && previousGamePad[i].Buttons.X != ButtonState.Pressed;
        }

        /// <summary>
        /// STARTボタンが押されたか
        /// </summary>
        /// <param name="index">プレイヤーのインデックス</param>
        /// <returns>押されたらtrue、押されていないまたは押しっぱなしならfalse</returns>
        public bool PushStart(PlayerIndex index)
        {
            if (index < PlayerIndex.One || index > PlayerIndex.Four)
                return false;

            int i = index - PlayerIndex.One;
            return nowGamePad[i].IsConnected && previousGamePad[i].IsConnected
                && nowGamePad[i].Buttons.Start == ButtonState.Pressed && previousGamePad[i].Buttons.Start != ButtonState.Pressed;
        }


        /// <summary>
        /// マウスの左ボタンが押されたか
        /// </summary>
        /// <returns></returns>
        public bool PushMouseLeftButton()
        {
            return nowMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton != ButtonState.Pressed;
        }
        /// <summary>
        /// マウスの右ボタンが押されたか
        /// </summary>
        /// <returns></returns>
        public bool PushMouseRightButton()
        {
            return nowMouse.RightButton == ButtonState.Pressed && previousMouse.RightButton != ButtonState.Pressed;
        
        }

        //
        public Point MousePosition()
        {
            return new Point(Mouse.GetState().X, Mouse.GetState().Y);
        }

        /// <summary>
        /// 指定したキーが押されたか
        /// </summary>
        /// <param name="key">キーの種類</param>
        /// <returns>押されたらtrue、押されていないまたはおしっぱなしならfalse</returns>
        public bool PushKey(Keys key)
        {
            return nowKeyboard.IsKeyDown(key) && !previousKeyboard.IsKeyDown(key);
        }

        /// <summary>
        /// 指定したキーが押されているか
        /// </summary>
        /// <param name="key">キーの種類</param>
        /// <returns>押されたらtrue、押されていなかったらfalse</returns>
        public bool DownKey(Keys key)
        {
            return nowKeyboard.IsKeyDown(key);
        }
    }
}
