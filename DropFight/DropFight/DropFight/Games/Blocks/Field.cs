using System.Collections.Generic;
using System.Linq;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace DropFight.Games.Blocks
{
    /// <summary>
    /// フィールド
    /// およびブロックのマネージャー
    /// </summary>
    public class Field
    {
        /// <summary>
        /// ブロックの2次元配列
        /// </summary>
        private Block[,] blocks;

        /// <summary>
        /// 幅(ブロック)
        /// </summary>
        public int Width
        {
            get
            {
                return blocks.GetLength(0);
            }
        }

        /// <summary>
        /// 高さ(ブロック)
        /// </summary>
        public int Height
        {
            get
            {
                return blocks.GetLength(1);
            }
        }

        /// <summary>
        /// フィールドの情報
        /// </summary>
        public FieldInfo Info
        {
            get
            {
                return new FieldInfo(this);
            }
        }

        /// <param name="width">幅(ブロック)</param>
        /// <param name="height">高さ(ブロック)</param>
        /// <param name="blockModelEven">奇数座標のブロックのモデル</param>
        /// <param name="blockModelOdd">偶数座標のブロックのモデル</param>
        public Field(int width, int height, Model blockModelOdd, Model blockModelEven)
        {
            blocks = new Block[width, height];
            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
					Block block;
                    if ((x + z) % 2 == 0)
                    {
						block = new Block(x, z, blockModelEven);
                    }
                    else
                    {
						block = new Block(x, z, blockModelOdd);
                    }
                    blocks[x, z] = block;
                }
            }
        }

		/// <summary>
		/// １枚絵のブロックから構成されるフィールドを作る.
		/// テクスチャの割り当てが変則的なため専用のモデルを使う必要がある.
		/// ---モデルの形式---
		/// ブロックの上面にのみテクスチャを設定する.
		/// ------------------
		/// </summary>
		/// <param name="width">幅(ブロック)</param>
		/// <param name="height">高さ(ブロック)</param>
		/// <param name="models">モデルの２次元配列</param>
		/// <param name="texture">フィールドの１枚絵</param>
		public Field(int width, int height, Model[,] models, Texture2D texture)
		{
			blocks = new Block[width, height];
			// 1枚絵を作成
			for (int z = 0; z < height; z++)
			{
				for (int x = 0; x < width; x++)
				{
					int blockWidth = (int)(texture.Width / width);
					int blockHeight = (int)(texture.Height / height);
					Rectangle sourceRect = new Rectangle(x * blockWidth, z * blockHeight, blockWidth, blockHeight);
					Texture2D cropTexture = new Texture2D(texture.GraphicsDevice, sourceRect.Width, sourceRect.Height);
					Color[] data = new Color[sourceRect.Width * sourceRect.Height];
					texture.GetData(0, sourceRect, data, 0, data.Length);
					cropTexture.SetData(data);

					models[x, z].Meshes[0].Effects[0].Parameters["Texture"].SetValue(cropTexture);
					blocks[x, z] = new Block(x, z, models[x, z]);
				}
			}
		}


        /// <summary>
        /// 指定された座標のブロックを返す
        /// </summary>
        /// <param name="x">ブロックのx座標</param>
        /// <param name="z">ブロックのz座標</param>
        public Block GetBlock(int x, int z)
        {
            return blocks[x, z];
        }

        /// <summary>
        /// 指定された座標のブロックを除去する
        /// 指定された座標が範囲外なら何もしない
        /// </summary>
        /// <param name="x">ブロックのx座標</param>
        /// <param name="z">ブロックのz座標</param>
        public void RemoveBlock(int x, int z)
        {
            blocks[x, z] = null;
        }

        /// <summary>
        /// 更新する
        /// </summary>
        /// <param name="gameInfo">ゲームの情報</param>
        public void Update(GameInfo gameInfo)
        {
            foreach (Block block in blocks)
            {
                if (block == null)
                {
                    continue;
                }

                block.Update(gameInfo);
            }
			//--- ブロックの時間経過による落下 ---//
			// 1分経過
			if (new TimeSpan(0, 1, 0) < gameInfo.CurrentTime && gameInfo.CurrentTime < new TimeSpan(0, 2, 0))
			{
				for (int x = 0; x < gameInfo.FieldInfo.Width; x++)
				{
					blocks[x, 0].KillBlock();
					blocks[x, gameInfo.FieldInfo.Height-1].KillBlock();
				}
				for (int z = 0; z < gameInfo.FieldInfo.Height; z++)
				{
					blocks[0, z].KillBlock();
					blocks[gameInfo.FieldInfo.Width-1, z].KillBlock();
				}
			}
			// 2分経過
			else if (new TimeSpan(0, 2, 0) < gameInfo.CurrentTime && gameInfo.CurrentTime < new TimeSpan(0, 3, 0))
			{
				for (int x = 1; x < gameInfo.FieldInfo.Width-1; x++)
				{
					blocks[x, 1].KillBlock();
					blocks[x, gameInfo.FieldInfo.Height - 2].KillBlock();
				}
				for (int z = 1; z < gameInfo.FieldInfo.Height-1; z++)
				{
					blocks[1, z].KillBlock();
					blocks[gameInfo.FieldInfo.Width - 2, z].KillBlock();
				}
			}
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="spriteBatch">スプライト描画用のオブジェクト</param>
        /// <param name="camera">モデル用のカメラ</param>
        public void Draw(Camera camera)
        {
            foreach (Block block in blocks)
            {
                if (block == null)
                {
                    continue;
                }

                block.Draw(camera);
            }
        }
    }
}
