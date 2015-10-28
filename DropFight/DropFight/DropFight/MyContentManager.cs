using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace DropFight
{
	/// <summary>
	/// 同一コンテンツをディープコピーできるようにコンテンツマネージャをオーバーロード
	/// </summary>
	class MyContentManager : ContentManager
	{
		public MyContentManager(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{ }

		public override T Load<T>(string assetName)
		{
			return ReadAsset<T>(assetName, IgnoreDisposableAsset);
		}

		void IgnoreDisposableAsset(IDisposable disposable)
		{
		}
	}
}
