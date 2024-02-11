using SharpPluginLoader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHWTeaOverlay
{
	public static class TeaLog
	{

		public static void Info(string text)
		{

			Log.Info($"MHW Tea Overlay: {text}");
		}
	}
}
