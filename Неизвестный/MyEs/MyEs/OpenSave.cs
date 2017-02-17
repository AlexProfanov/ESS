using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace MyEs
{
	internal class OpenSave
	{
		public string FileName;

		public void SaveKB(KnowledgeBase bass)
		{
			Stream stream = File.Open(this.FileName, 2);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, bass);
			stream.Close();
		}

		public KnowledgeBase OpenKB()
		{
			KnowledgeBase result;
			try
			{
				Stream stream = File.Open(this.FileName, 3);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				KnowledgeBase knowledgeBase = (KnowledgeBase)binaryFormatter.Deserialize(stream);
				stream.Close();
				result = knowledgeBase;
			}
			catch
			{
				MessageBox.Show("Произошла ошибка при загрузке файла");
				result = null;
			}
			return result;
		}
	}
}
