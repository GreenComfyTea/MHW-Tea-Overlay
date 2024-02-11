using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace MHWTeaOverlay;

public static class JsonManager
{

	public static readonly JsonSerializerOptions JsonSerializerOptionsInstance = new() { WriteIndented = true, AllowTrailingCommas = true };


	public static string Serialize(object obj)
	{
		return JsonSerializer.Serialize(obj, JsonSerializerOptionsInstance).Replace("  ", "\t");
	}

	public static async Task SerializeToFile(string filePathName, string json)
	{
		//File.WriteAllText(filePathName, json);
;
		var file = File.Open(filePathName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
		var streamWriter = new StreamWriter(file);
		streamWriter.AutoFlush = true;
		file.SetLength(0);
		await streamWriter.WriteLineAsync(json);

		streamWriter.Close();

	}

	public static async Task SearializeToFile(string filePathName, object obj)
	{
		await SerializeToFile(filePathName, Serialize(obj));
	}

	public static async Task<string> ReadFromFile(string filePathName)
	{
		//return File.ReadAllText(filePathName);

		var file = File.Open(filePathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		var streamReader = new StreamReader(file);
		var content = await streamReader.ReadToEndAsync();

		streamReader.Close();

		return content;
	}
}
