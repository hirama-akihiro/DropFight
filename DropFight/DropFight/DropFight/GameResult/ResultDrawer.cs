using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework;
using DropFight.Games.Players;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DropFight.GameResult
{
	class ResultDrawer
	{
		private List<RankNo> ranks = new List<RankNo>();
		private List<Player> players = new List<Player>();
		private Model stage;

		public ResultDrawer(PlayerManager playerManager,ContentManager content)
		{
			// 各プレイヤーモデルの位置調整
			foreach (var player in playerManager.Players)
			{
				switch (players.Count)
				{
					case 0:
						player.ModelInfo.Position = new Vector3(-1.8f, -1.0f, 0.0f);
						break;
					case 1:
						player.ModelInfo.Position = new Vector3(-0.6f, -1.0f, 0.0f);
						break;
					case 2:
						player.ModelInfo.Position = new Vector3(0.6f, -1.0f, 0.0f);
						break;
					case 3:
						player.ModelInfo.Position = new Vector3(1.8f, -1.0f, 0.0f);
						break;
				}
				player.ModelInfo.Radian = Vector3.Zero;
				players.Add(player);
			}

			// 各プレイヤーのランキング順位テクスチャ設定
			foreach (var player in playerManager.Players)
			{
				Vector2 pos;
				Texture2D texture;
				switch (ranks.Count)
				{
					case 0:
						pos = new Vector2(80f, 170f);
						texture = GetRankTexture(players, player.Index, content);
						ranks.Add(new RankNo(texture, pos, GetRank(players,player.Index)));
						break;
					case 1:
						pos = new Vector2(260f, 170f);
						texture = GetRankTexture(players, player.Index, content);
						ranks.Add(new RankNo(texture, pos, GetRank(players, player.Index)));
						break;
					case 2:
						pos = new Vector2(440f, 170f);
						texture = GetRankTexture(players, player.Index, content);
						ranks.Add(new RankNo(texture, pos, GetRank(players, player.Index)));
						break;
					case 3:
						pos = new Vector2(620f, 170f);
						texture = GetRankTexture(players, player.Index, content);
						ranks.Add(new RankNo(texture, pos, GetRank(players, player.Index)));
						break;
				}
			}

			stage = content.Load<Model>("Scene/Result/Stage");
		}


		/// <summary>
		/// リザルト描画メソッド
		/// </summary>
		/// <param name="camera">モデル用のカメラ</param>
		/// <param name="spriteBatch">テクスチャ用のスプライトバッチ</param>>
		public void Draw(Camera camera, SpriteBatch spriteBatch)
		{
			foreach (var player in players)
			{
				//player.Draw(camera);
				// player.Draw()では死んだプレイヤーが表示されない
				player.Model.Draw(camera, player.ModelInfo);
			}

			foreach (var rank in ranks)
			{
				rank.Draw(spriteBatch);
			}
			foreach (ModelMesh mesh in stage.Meshes)
			{
				foreach (BasicEffect effect in mesh.Effects)
				{
					effect.EnableDefaultLighting();
					effect.DirectionalLight0.Direction = new Vector3(10.0f, 10.0f, 10.0f);
					effect.World = Matrix.CreateScale(Vector3.One) * Matrix.CreateTranslation(new Vector3(0.0f,-1.0f,0.0f));
					effect.View = camera.View;
					effect.Projection = camera.Projection;
				}
				mesh.Draw();
			}
		}

		/// <summary>
		/// リザルト更新処理
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			for (int array = 0; array < players.Count; array++)
			{
				if (ranks[array].Rank == 1)
				{
					players[array].Model.ChangeMotion(PlayerModel.ModelAnimation.ATTACK);
					players[array].Model.Update(gameTime);
				}
				else
				{
					players[array].Model.ChangeMotion(PlayerModel.ModelAnimation.WALK);
					players[array].Model.Update(gameTime);
				}
			}
		}

		/// <summary>
		/// ランキング順位に対応したテクスチャを取得するメソッド
		/// </summary>
		/// <param name="players"></param>
		/// <param name="index"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		private Texture2D GetRankTexture(List<Player> players, PlayerIndex index, ContentManager content)
		{
			int rank = GetRank(players, index);
			switch (rank)
			{
				case 1:
					return content.Load<Texture2D>("Scene/Result/One");
				case 2:
					return content.Load<Texture2D>("Scene/Result/Two");
				case 3:
					return content.Load<Texture2D>("Scene/Result/Three");
				case 4:
					return content.Load<Texture2D>("Scene/Result/Four");
				default:
					return content.Load<Texture2D>("Scene/Result/Four");
			}
		}

		/// <summary>
		/// プレイヤー番号に対応したランキング順位を取得するメソッド
		/// </summary>
		/// <param name="players"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		private int GetRank(List<Player> players, PlayerIndex index)
		{
			List<uint> scores = new List<uint>();
			List<TimeSpan> deadTimes = new List<TimeSpan>();
			// 各プレイヤーの残基数取得
			foreach (var player in players)
			{
				scores.Add(player.Stock);
				deadTimes.Add(player.DeadTime);
			}
			scores.Sort();
			deadTimes.Sort();

			// indexのプレイヤーが死んでいた場合は死んだ時間
			// 生きている場合残基数で順位決定
			int rank = 0;
			int count = 0;
			if(players[(int)index].Stock == 0)
			{
				for (int loop = 0; loop < deadTimes.Count; loop++)
				{
					if (deadTimes[loop] == TimeSpan.Zero)
						continue;
					if (deadTimes[loop] == players[(int)index].DeadTime)
					{
						break;
					}
					count++;
				}
			}
			else
			{
				// プレイヤー番号のランキングを探索
				for (int loop = scores.Count-1; loop >= 0; loop--)
				{
					count = loop;
					if (scores[loop] == players[(int)index].Stock)
						break;
				}
			}
			rank = scores.Count - count;
			return rank;
		}
	}
}
