using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DropFight.GameResult
{
	/// <summary>
	/// ランキング順位テクスチャ管理クラス
	/// </summary>
	class RankNo
	{
		private Texture2D texture;
		private Vector2 position;
		private int rank;

		public RankNo(Texture2D texture, Vector2 pos)
		{
			this.texture = texture;
			this.position = pos;
		}

		public RankNo(Texture2D texture, Vector2 pos, int rank)
		{
			this.texture = texture;
			this.position = pos;
			this.rank = rank;
		}

		public int Rank
		{
			get { return this.rank; }
		}

		public void Update()
		{
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, position, Color.White);
		}
	}
}
