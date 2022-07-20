using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EUSignTestCS.Verificators
{
	class TestData
	{
		private static Random rnd = new Random();
		public static int BigFileSize = 60000000;
		public static int SignKeys = 3;
		public static int EnvelopRecipientsKeys = 2;

		public static string GetString(int size = 100)
		{
			char[] characters = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
				'A', 'B', 'C', 'D', 'E', 'F', 'a', 'b', 'c', 'd', 'e', 'f'};
			StringBuilder strBuilder = new StringBuilder(size);
			for (int i = 0; i < size; i++)
			{
				strBuilder.Append(
					characters[rnd.Next() % characters.Length]);
			}

			return strBuilder.ToString();
		}

		public static byte[] GetByteArray(int minSize, int maxSize)
		{
			int count = rnd.Next(minSize, maxSize);

			byte[] data = new byte[count];
			rnd.NextBytes(data);
			
			return data;
		}

		public static byte[] GetByteArray(int size = 5000000)
		{
			byte[] data = new byte[size];

			rnd.NextBytes(data);

			return data;
		}

		public static bool CreateTestFile(string filePath, int size = 5000000)
		{
			const int maxBufferSize = 5000000;

			byte[] data = GetByteArray(
				size < maxBufferSize ?
					size : maxBufferSize);

			try
			{
				FileStream fileStream = new FileStream(
					filePath, FileMode.Create, FileAccess.Write);

				int parts = size / maxBufferSize;
				int lastPart = size % maxBufferSize;
				
				for (int i = 0; i < parts; i++)
					fileStream.Write(data, 0, data.Length);

				if (lastPart != 0)
					fileStream.Write(data, parts * maxBufferSize, lastPart);

				fileStream.Close();
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		public static void ReplaceFile(string sourceFilePath,
			string targetFilePath)
		{
			try
			{
				if (File.Exists(targetFilePath))
					File.Delete(targetFilePath);
				File.Move(sourceFilePath, targetFilePath);
			}
			catch (Exception)
			{
			
			}
		}

		public static void CreateDirectory(string path)
		{
			try
			{
				Directory.CreateDirectory(path);
			}
			catch (Exception)
			{
			}
		}

		public static void DeleteDirectory(string path)
		{
			try
			{
				Directory.Delete(path, true);
			}
			catch (Exception)
			{
			}
		}
	}
}
