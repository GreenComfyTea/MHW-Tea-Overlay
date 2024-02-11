using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;

namespace MHWTeaOverlay;

public static class JsonManager
{

	public static JObject ParseJson(string json)
	{
		return JObject.Parse(json);
	}

	public static JObject ParseJsonFromFile(string filePathName)
	{
		var json = File.ReadAllText(filePathName);
		return ParseJson(json);
	}

	public static string ReadJsonFromFile(string filePathName)
	{
		return File.ReadAllText(filePathName);
	}

	public static void SaveJson(string filePathName, string json)
	{
		File.WriteAllText(filePathName, json);
	}

	public static void SaveJson(string filePathName, JObject data)
	{
		var json = data.ToString();
		SaveJson(filePathName, json);
	}
}
