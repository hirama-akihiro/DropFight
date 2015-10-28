using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DropFight.CharactorSelect;
using Microsoft.Xna.Framework.Content;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using DropFight.Games.Attacks;
using DropFight.Games.Blocks;
using DropFight.Games.Controllers;
using DropFight.ModelUtils;
using DropFight.Games.Players;
using DropFight.Games;
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace DropFight.Scenes
{
    /// <summary>
    /// ゲーム時のキャラセレクト場面
    /// </summary>
    public class SelectScene : Scene
    {
        //一回目のUpdate
        private bool isFirst = true;
        //このシーンの設定を格納する
        CharactorSelectDatas data;
        //箱○コントローラのポインターの動くスピード
        private static float pointerMoveSpeed = 10;
        //コントローラのリスト
        private LinkedList<Pointer> pointers = new LinkedList<Pointer>();
        private LinkedList<CharactirSelectParts> partsList = new LinkedList<CharactirSelectParts>();
        private ContentManager content;
		private SoundEffect okSE;
		private SoundEffect selectSE;
		private Song bgm;
		private Texture2D back;
        public SelectScene(ContentManager content)
        {
            this.content = content;
			//背景
			back = content.Load<Texture2D>("Scene/Select/back2");

            //ポインタの追加
            pointers.AddFirst(new Pointer(new Rectangle(0, 0, 50, 50), PlayerIndex.One, content.Load<Texture2D>("Scene/Select/Pointer1")));
            pointers.AddFirst(new Pointer(new Rectangle(0, 0, 50, 50), PlayerIndex.Two, content.Load<Texture2D>("Scene/Select/Pointer2")));
            pointers.AddFirst(new Pointer(new Rectangle(0, 0, 50, 50), PlayerIndex.Three, content.Load<Texture2D>("Scene/Select/Pointer3")));
            pointers.AddFirst(new Pointer(new Rectangle(0, 0, 50, 50), PlayerIndex.Four, content.Load<Texture2D>("Scene/Select/Pointer4")));
            //モデルセレクトの追加
            partsList.AddFirst(new ModelTypeSelecter(new Rectangle(20, 125, 50, 50), content.Load<Texture2D>("Scene/Select/left"), ModelTypeSelecter.ModelSelectType.LEFT, PlayerIndex.One));
            partsList.AddFirst(new ModelTypeSelecter(new Rectangle(280, 125, 50, 50), content.Load<Texture2D>("Scene/Select/right"), ModelTypeSelecter.ModelSelectType.RIGHT, PlayerIndex.One));
            partsList.AddFirst(new ModelTypeSelecter(new Rectangle(470, 125, 50, 50), content.Load<Texture2D>("Scene/Select/left"), ModelTypeSelecter.ModelSelectType.LEFT, PlayerIndex.Two));
            partsList.AddFirst(new ModelTypeSelecter(new Rectangle(730, 125, 50, 50), content.Load<Texture2D>("Scene/Select/right"), ModelTypeSelecter.ModelSelectType.RIGHT, PlayerIndex.Two));
            partsList.AddFirst(new ModelTypeSelecter(new Rectangle(20, 425, 50, 50), content.Load<Texture2D>("Scene/Select/left"), ModelTypeSelecter.ModelSelectType.LEFT, PlayerIndex.Three));
            partsList.AddFirst(new ModelTypeSelecter(new Rectangle(280, 425, 50, 50), content.Load<Texture2D>("Scene/Select/right"), ModelTypeSelecter.ModelSelectType.RIGHT, PlayerIndex.Three));
            partsList.AddFirst(new ModelTypeSelecter(new Rectangle(470, 425, 50, 50), content.Load<Texture2D>("Scene/Select/left"), ModelTypeSelecter.ModelSelectType.LEFT, PlayerIndex.Four));
            partsList.AddFirst(new ModelTypeSelecter(new Rectangle(730, 425, 50, 50), content.Load<Texture2D>("Scene/Select/right"), ModelTypeSelecter.ModelSelectType.RIGHT, PlayerIndex.Four));
            //プレイヤー番号
			partsList.AddFirst(new CharactorNumber(new Rectangle(75, 5, 100, 40), content.Load<Texture2D>("Scene/Select/1p"), PlayerIndex.One));
			partsList.AddFirst(new CharactorNumber(new Rectangle(525, 5, 100, 40), content.Load<Texture2D>("Scene/Select/2p"), PlayerIndex.Two));
			partsList.AddFirst(new CharactorNumber(new Rectangle(75, 305, 100, 40), content.Load<Texture2D>("Scene/Select/3p"), PlayerIndex.Three));
			partsList.AddFirst(new CharactorNumber(new Rectangle(525, 305, 100, 40), content.Load<Texture2D>("Scene/Select/4p"), PlayerIndex.Four));
			//プレイヤーの種類の選択の追加
            partsList.AddFirst(new CharactorTypeSelecter(new Rectangle(175, 5, 100, 40), content.Load<Texture2D>("Scene/Select/human"), content.Load<Texture2D>("Scene/Select/CPU"), PlayerIndex.One));
            partsList.AddFirst(new CharactorTypeSelecter(new Rectangle(625, 5, 100, 40), content.Load<Texture2D>("Scene/Select/human"), content.Load<Texture2D>("Scene/Select/CPU"), PlayerIndex.Two));
            partsList.AddFirst(new CharactorTypeSelecter(new Rectangle(175, 305, 100, 40), content.Load<Texture2D>("Scene/Select/human"), content.Load<Texture2D>("Scene/Select/CPU"), PlayerIndex.Three));
            partsList.AddFirst(new CharactorTypeSelecter(new Rectangle(625, 305, 100, 40), content.Load<Texture2D>("Scene/Select/human"), content.Load<Texture2D>("Scene/Select/CPU"), PlayerIndex.Four));
            //カラー選択の表示
            partsList.AddFirst(new ColorSelecter(new Rectangle(75, 260, 30, 30), content.Load<Texture2D>("Scene/Select/blue"), ModelColor.BLUE, PlayerIndex.One));
            partsList.AddFirst(new ColorSelecter(new Rectangle(115, 260, 30, 30), content.Load<Texture2D>("Scene/Select/red"), ModelColor.RED, PlayerIndex.One));
            partsList.AddFirst(new ColorSelecter(new Rectangle(155, 260, 30, 30), content.Load<Texture2D>("Scene/Select/orenge"), ModelColor.ORANGE, PlayerIndex.One));
            partsList.AddFirst(new ColorSelecter(new Rectangle(195, 260, 30, 30), content.Load<Texture2D>("Scene/Select/green"), ModelColor.GREEN, PlayerIndex.One));

            partsList.AddFirst(new ColorSelecter(new Rectangle(525, 260, 30, 30), content.Load<Texture2D>("Scene/Select/blue"), ModelColor.BLUE, PlayerIndex.Two));
            partsList.AddFirst(new ColorSelecter(new Rectangle(565, 260, 30, 30), content.Load<Texture2D>("Scene/Select/red"), ModelColor.RED, PlayerIndex.Two));
            partsList.AddFirst(new ColorSelecter(new Rectangle(605, 260, 30, 30), content.Load<Texture2D>("Scene/Select/orenge"), ModelColor.ORANGE, PlayerIndex.Two));
            partsList.AddFirst(new ColorSelecter(new Rectangle(645, 260, 30, 30), content.Load<Texture2D>("Scene/Select/green"), ModelColor.GREEN, PlayerIndex.Two));

            partsList.AddFirst(new ColorSelecter(new Rectangle(75, 560, 30, 30), content.Load<Texture2D>("Scene/Select/blue"), ModelColor.BLUE, PlayerIndex.Three));
            partsList.AddFirst(new ColorSelecter(new Rectangle(115, 560, 30, 30), content.Load<Texture2D>("Scene/Select/red"), ModelColor.RED, PlayerIndex.Three));
            partsList.AddFirst(new ColorSelecter(new Rectangle(155, 560, 30, 30), content.Load<Texture2D>("Scene/Select/orenge"), ModelColor.ORANGE, PlayerIndex.Three));
            partsList.AddFirst(new ColorSelecter(new Rectangle(195, 560, 30, 30), content.Load<Texture2D>("Scene/Select/green"), ModelColor.GREEN, PlayerIndex.Three));

            partsList.AddFirst(new ColorSelecter(new Rectangle(525, 560, 30, 30), content.Load<Texture2D>("Scene/Select/blue"), ModelColor.BLUE, PlayerIndex.Four));
            partsList.AddFirst(new ColorSelecter(new Rectangle(565, 560, 30, 30), content.Load<Texture2D>("Scene/Select/red"), ModelColor.RED, PlayerIndex.Four));
            partsList.AddFirst(new ColorSelecter(new Rectangle(605, 560, 30, 30), content.Load<Texture2D>("Scene/Select/orenge"), ModelColor.ORANGE, PlayerIndex.Four));
            partsList.AddFirst(new ColorSelecter(new Rectangle(645, 560, 30, 30), content.Load<Texture2D>("Scene/Select/green"), ModelColor.GREEN, PlayerIndex.Four));
			
            //プレビューの追加
            partsList.AddFirst(new CharactorPreview(new Rectangle(75, 50, 200, 200), content.Load<Texture2D>("Scene/Select/previewback"), content.Load<Texture2D>("Scene/Select/ng"), PlayerIndex.One));
            partsList.AddFirst(new CharactorPreview(new Rectangle(525, 50, 200, 200), content.Load<Texture2D>("Scene/Select/previewback"), content.Load<Texture2D>("Scene/Select/ng"), PlayerIndex.Two));
            partsList.AddFirst(new CharactorPreview(new Rectangle(75, 350, 200, 200), content.Load<Texture2D>("Scene/Select/previewback"), content.Load<Texture2D>("Scene/Select/ng"), PlayerIndex.Three));
            partsList.AddFirst(new CharactorPreview(new Rectangle(525, 350, 200, 200), content.Load<Texture2D>("Scene/Select/previewback"), content.Load<Texture2D>("Scene/Select/ng"), PlayerIndex.Four));

            //OKボタン
            partsList.AddFirst(new OKButton(new Rectangle(300, 250, 200, 100), content.Load<Texture2D>("Scene/Select/ok")));

            //色選択の追加
            data = new CharactorSelectDatas(content);

			// 音
			okSE = content.Load<SoundEffect>("SE/ok");
			selectSE = content.Load<SoundEffect>("SE/cursormove");
        }
        /// <summary>
        /// 更新する
        /// </summary>
        /// <param name="gameTime">ゲーム内部の時間</param>
        /// <returns>次の場面</returns>
        public Scene Update(GameTime gameTime, Input input)
        {
            // TODO
            #region ポインタの更新処理ｌクリック処理

            //ポインタの最初の設定。刺さってるコントローラを認識してHumanとCpuの割り当てを行う
            if (isFirst)
            {
                foreach(PlayerIndex index in Enum.GetValues(typeof(PlayerIndex))){
                    //刺さってたら人間。刺さってなかったらCPU
                    data.setPlayerType(index,input.GamePadConnect(index) ? PlayerType.HUMAN :PlayerType.CPU);
                }


                //一つも刺さってないなら1PだけHumanに
                if (!input.GamePadConnect(PlayerIndex.One))
                {
                    data.setPlayerType(PlayerIndex.One, PlayerType.HUMAN);
                }
            }
            isFirst = false;

            //ポインタを動かす
            foreach (Pointer pointer in pointers){
                pointer.Position = new Point(pointer.Position.X + (int)(input.LeftStick(pointer.PointerPlayerIndex).X * pointerMoveSpeed),
                    pointer.Position.Y - (int)(input.LeftStick(pointer.PointerPlayerIndex).Y*pointerMoveSpeed));

				// マウスポインタが画面外へ行く不具合解決
				if (pointer.Position.X < 0) { pointer.Position = new Point(0, pointer.Position.Y); }
				if (pointer.Position.Y < 0) { pointer.Position = new Point(pointer.Position.X, 0); }
				if (Game1.ScreenWidth - 10 < pointer.Position.X) { pointer.Position = new Point(Game1.ScreenWidth - 10, pointer.Position.Y); }
				if (Game1.ScreenHeight - 10 < pointer.Position.Y) { pointer.Position = new Point(pointer.Position.X, Game1.ScreenHeight - 10); }

                if (pointer.PointerPlayerIndex != PlayerIndex.One)
                {
                    pointer.IsEnable = input.GamePadConnect(pointer.PointerPlayerIndex);
                }
            }
            
            //一つもコントローラが刺さってない時は1Pがマウスに追従します
            if (!input.GamePadConnect(PlayerIndex.One))
            {
                foreach (Pointer pointer in pointers)
                {
                    pointer.Position = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                }

            }

            foreach (Pointer pointer in pointers)
            {
                if(input.GamePadConnect(PlayerIndex.One)){
                foreach (CharactirSelectParts parts in partsList)
                {
                    if (parts.IsClick(pointer.Position) && input.PushABXY(pointer.PointerPlayerIndex))
                    {
						// パーツが選択されたらSEを再生
                        if (parts is OKButton)
                        {
                        }else selectSE.Play();
                        parts.Click(pointer.PointerPlayerIndex,data);
                    }
                }
                }else{
                foreach (CharactirSelectParts parts in partsList)
                    {
                        if (parts.IsClick(pointer.Position)&& input.PushMouseLeftButton())
                        {
							// パーツが選択されたらSEを再生
                            if (parts is OKButton)
                            {
                            }
                            else selectSE.Play();
                            parts.Click(pointer.PointerPlayerIndex, data);
                        }
                    }
                }
            }
            #endregion

            #region 全パーツの更新
            foreach (CharactirSelectParts parts in partsList)
            {
                parts.Update(gameTime,data);
            }
            #endregion

            //設定終了（OKボタンが押された）ならゲーム画面作成
            if (data.SettingEnd)
            {
                okSE.Play();
				// BGMの停止
				MediaPlayer.Stop();

                #region ゲーム画面の作成
                AttackManager attackManager = new AttackManager();

                // フィールドサイズを指定
                int fieldWidth = 8;
                int fieldHeight = 8;
                //// チェック柄のフィールドを作成
                //Model blockModelOdd = Content.Load<Model>("SampleBlock/blueBlock");
                //Model blockModelEven = Content.Load<Model>("SampleBlock/greenBlock");
                //Field field = new Field(fieldWidth, fieldHeight, blockModelOdd, blockModelEven);

                // 一枚絵のフィールドを作成
                // コンテンツのディープコピーができるコンテンツマネージャを作成
                MyContentManager content = new MyContentManager(this.content.ServiceProvider);
                content.RootDirectory = "Content";

                Model[,] blockModels = new Model[fieldWidth, fieldHeight];
                for (int z = 0; z < fieldHeight; z++)
                {
                    for (int x = 0; x < fieldWidth; x++)
                    {
                        blockModels[x, z] = content.Load<Model>("Block/block");
                    }
                }
				Texture2D topTexture = content.Load<Texture2D>("Block/sample01");
				Random rnd = new Random(gameTime.TotalGameTime.Milliseconds);
				switch (rnd.Next(3))
				{
					case 0:
						topTexture = content.Load<Texture2D>("Block/sample01");
						break;
					case 1:
						topTexture = content.Load<Texture2D>("Block/sample02");
						break;
					case 2:
						topTexture = content.Load<Texture2D>("Block/sample03");
						break;
					case 3:
						topTexture = content.Load<Texture2D>("Block/sample04");
						break;
				}
                Field field = new Field(fieldWidth, fieldHeight, blockModels, topTexture);

                //コントローラをまとめて作成
                Dictionary<PlayerIndex,Controller> controllers = new Dictionary<PlayerIndex,Controller>();
                foreach(PlayerIndex index in Enum.GetValues(typeof(PlayerIndex))){
                    if (data.getPlayerType(index) == PlayerType.HUMAN)
                    {
                        controllers[index] = new HumanController(index);
                    }
                    else
                    {
                        controllers[index] = new SimpleAI(index);
                    }
                }


                float x1 = 2;
                float z1 = 2;
                Model attackModel = content.Load<Model>("AttackWave/BWave");
                Controller controller1;
                controllers.TryGetValue(PlayerIndex.One, out controller1);
                Player player1 = new Player("1P", x1, z1, controller1, data.getModel(PlayerIndex.One), attackModel, Color.Red);
				player1.ModelInfo.Scale = ModelDB.GetScale(data.getModelType(PlayerIndex.One));

                Controller controller2;
                controllers.TryGetValue(PlayerIndex.Two, out controller2);
                float x2 = 5;
                float z2 = 2;
                Player player2 = new Player("2P", x2, z2, controller2, data.getModel(PlayerIndex.Two), attackModel, Color.Orange);
                //player2.ModelInfo.Radian = new Vector3(-MathHelper.PiOver2, 0, 0);
				player2.ModelInfo.Scale = ModelDB.GetScale(data.getModelType(PlayerIndex.Two));

                Controller controller3;
                controllers.TryGetValue(PlayerIndex.Three, out controller3);
                float x3 = 2;
                float z3 = 5;
                Player player3 = new Player("3P", x3, z3, controller3, data.getModel(PlayerIndex.Three), attackModel, Color.Lime);
				player3.ModelInfo.Scale = ModelDB.GetScale(data.getModelType(PlayerIndex.Three));

                Controller controller4;
                controllers.TryGetValue(PlayerIndex.Four, out controller4);
                float x4 = 5;
                float z4 = 5;
                Player player4 = new Player("4P", x4, z4, controller4, data.getModel(PlayerIndex.Four), attackModel, Color.Blue);
				player4.ModelInfo.Scale = ModelDB.GetScale(data.getModelType(PlayerIndex.Four));

                PlayerManager playerManager = new PlayerManager(player1, player2, player3, player4);
                ManagerSet managerSet = new ManagerSet(attackManager, field, playerManager);
                return new GameScene(content, managerSet);
                #endregion
            }
            return this;
        }

        /// <summary>
        /// 描画する
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            // TODO
			for (int i = 0; i <= Game1.ScreenWidth/back.Width; i++)
			{
				for (int j = 0; j <= Game1.ScreenHeight/back.Height; j++)
				{
					spriteBatch.Draw(back, new Rectangle(back.Width * i, back.Height * j, back.Width, back.Height), Color.White);
				}
			}

			foreach (CharactirSelectParts parts in partsList)
			{
				parts.Draw(spriteBatch);
			}
            foreach (Pointer pointer in pointers)
            {
                pointer.Draw(spriteBatch);
            }

        }
    }
}
