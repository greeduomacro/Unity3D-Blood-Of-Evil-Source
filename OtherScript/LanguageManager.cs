using System.Collections.Generic;
using System.IO;
using System.Linq;
using LanguageType = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>;
using StringDictionary = System.Collections.Generic.Dictionary<string, string>;
using UnityEngine;

public enum e_language
{
	English,
	French,
	SIZE,
}

public enum e_languageExtension
{
	Ini,
	Xml,
	Bin,
	SIZE,
}

public interface ISerializalbleAndDeserializable
{
	void Save();
	void Load();
}

public class LanguageManager
{
	#region Attributes
	private StringDictionary defaultLanguageTexts;
	private StringDictionary currentLanguageTexts;
	
	private e_language defaultLanguage;
	private e_language currentLanguage;
	
	private string defaultPath;
	private string currentPath;
	
	private string repositoryPath;
	public string extension;
	private e_languageExtension extensionEnumeration;
	private LanguageType languageList = new LanguageType();
	#endregion
	#region Properties
	public StringDictionary DefaultLanguageTexts { get { return defaultLanguageTexts; } private set { defaultLanguageTexts = value; } }
	public StringDictionary CurrentLanguageTexts { get { return currentLanguageTexts; } private set { currentLanguageTexts = value; } }
	
	public e_language DefaultLanguage { get { return defaultLanguage; } private set { defaultLanguage = value; } }
	public e_language CurrentLanguage { get { return currentLanguage; } private set { currentLanguage = value; } }
	
	public string DefaultPath { get { return defaultPath; } private set { defaultPath = value; } }
	public string CurrentPath { get { return currentPath; } private set { currentPath = value; } }
	
	public string RepositoryPath { get { return repositoryPath; } private set { repositoryPath = value; } }

	public LanguageType LanguageList { get { return languageList; } private set { languageList = value; } }
	public void Initialize(e_language defaultLanguage = e_language.English, e_language currentLanguage = e_language.French, e_languageExtension extension = e_languageExtension.Ini)
	{
		this.defaultLanguageTexts = new StringDictionary();
		this.currentLanguageTexts = new StringDictionary();

		this.repositoryPath = DirectoryFunction.CombinePath(DirectoryFunction.CombinePath(DirectoryFunction.GetMyDocumentsPath(), "Blood Of Evil"), "Language");

		this.defaultLanguage = e_language.English;
		this.currentLanguage = e_language.French;

		this.languageList = new LanguageType();

		this.Extension = "." + extension.ToString().ToLower();
		this.extensionEnumeration = extension;

		this.CreateLanguageFilesIfDontExist();

		//Use those 2 lines for Generate Files and save
		//this.AddPredefinedLanguageNodes();
		//this.Save();

		//Use this line to load your files
		this.Load();
	}
	#region Load
	public void Load()
	{
		this.defaultLanguageTexts = this.LoadLanguage(this.defaultPath);

		if (this.defaultLanguage == this.currentLanguage)
		{
			this.currentLanguageTexts = this.defaultLanguageTexts;
			return;
		}

		this.currentLanguageTexts = this.LoadLanguage(this.currentPath);
	}

	public StringDictionary LoadIni(string path)
	{
		IniFile parser = new IniFile(path);
		return new StringDictionary(parser.KeysValues);
	}

	public StringDictionary LoadXmlOrBinary(string path)
	{
		StringDictionary languageTexts = new StringDictionary();

		this.languageList = SerializationTemplate.Load<LanguageType>(path, this.extensionEnumeration == e_languageExtension.Bin);
		this.languageList.ForEach(node => languageTexts.Add(node.Key, node.Value));

		return languageTexts;
	}
	#endregion

	private void ResetPath()
	{
		this.defaultPath = DirectoryFunction.CombinePath(this.repositoryPath, this.defaultLanguage.ToString()) + this.extension;
		this.currentPath = DirectoryFunction.CombinePath(this.repositoryPath, this.currentLanguage.ToString()) + this.extension;
	}
	#region Save
	public void Save()
	{
		this.SaveLanguage(this.defaultPath, this.defaultLanguageTexts);

		if (this.defaultLanguage == this.currentLanguage)
		{
			this.currentLanguageTexts = this.defaultLanguageTexts;
			return;
		}

		this.SaveLanguage(this.currentPath, this.currentLanguageTexts);
	}

	private void SaveIni(string path, StringDictionary languageTexts)
	{
		IniFile parser = new IniFile(path);
		parser.SaveFile(languageTexts);
	}

	private void SaveXmlOrBinary(string path, StringDictionary languageTexts)
	{
		this.languageList = languageTexts.ToList();
		bool isBinaryFile = this.extension == ".bin";

		SerializationTemplate.Save<LanguageType>(path, languageList, isBinaryFile);
	}
	#endregion
	public string Extension
	{
		get
		{
			return extension;
		}
		set
		{
			if (value == ".ini")
			{
				LoadLanguage = LoadIni;
				SaveLanguage = SaveIni;
			}
			else
			{
				LoadLanguage = LoadXmlOrBinary;
				SaveLanguage = SaveXmlOrBinary;
			}

			extension = value;

			ResetPath();
		}
	}
	public e_languageExtension ExtensionEnumeration { get { return extensionEnumeration; } private set { extensionEnumeration = value; } }
	#endregion
	#region Event Attributes
	public delegate void SaveEvent(string path, StringDictionary languageTexts);
	public delegate StringDictionary LoadEvent(string path);
	private System.Func<string, Dictionary<string, string>> LoadLanguage;
	private event SaveEvent SaveLanguage;
	#endregion
	#region Builder
	public LanguageManager()
	{
		this.Initialize(e_language.French, e_language.French, e_languageExtension.Ini);
		this.ChangeCurrentLanguage(e_language.English);
	}
	


	private void CreateLanguageFilesIfDontExist()
	{
		DirectoryFunction.CreateRepository(this.repositoryPath);

		for (short i =0; i < (int)e_language.SIZE; i++)
		{
			string filePath = DirectoryFunction.CombinePath(this.repositoryPath, ((e_language)i).ToString()) + this.extension;

			if (!(File.Exists(filePath)))
				using (File.Create(filePath));
		}
	}
	#endregion
	#region Override Functions
	#endregion
	#region Functions
	public string GetText(string textID)
	{
		if (this.currentLanguageTexts.ContainsKey(textID))
			return this.currentLanguageTexts[textID];
		else if (this.defaultLanguageTexts.ContainsKey(textID))
			return this.defaultLanguageTexts[textID];
		else Debug.LogError(textID + ", language : " + this.currentLanguage + ", path : " + this.defaultPath);
		return textID;
	}

	public void ChangeCurrentLanguage(e_language newLanguage)
	{
		if (newLanguage != this.currentLanguage)
		{
			this.currentLanguage = newLanguage;
			this.ResetPath();
			this.currentLanguageTexts = this.LoadLanguage(this.currentPath);
		}
	}

	//private void AddPredefinedLanguageNodes()
	//{
	//    this.currentLanguageTexts.Add("Awesome Bro", "Awesome Bro");
	//    this.currentLanguageTexts.Add("Thanks you", "Thanks you");
	//    this.currentLanguageTexts.Add("Seriously", "Seriously");
	//    this.currentLanguageTexts.Add("OMG", "OMG");

	//    this.defaultLanguageTexts.Add("Thanks you", "Merci");
	//    this.defaultLanguageTexts.Add("Seriously", "Sérieusement");
	//    this.defaultLanguageTexts.Add("OMG", "oh mon dieu");

	//    this.Save();
	//}

	


	#endregion
	#region Editor Functions
	public LanguageType ChangeEditorLanguageSelection(e_language newLanguage,  e_languageExtension newExtension)
	{
		string path = DirectoryFunction.CombinePath(this.repositoryPath, newLanguage.ToString()) + "." + newExtension.ToString().ToLower();

		if (newExtension == e_languageExtension.Ini)
		{
			IniFile parser = new IniFile(path);
			return new StringDictionary(parser.KeysValues).ToList(); 
		}
		else
		{
			LanguageType languageEditorList = new LanguageType();
			languageEditorList = SerializationTemplate.Load<LanguageType>(DirectoryFunction.CombinePath(this.repositoryPath, newLanguage.ToString()) + "." + newExtension.ToString().ToLower());
			return languageEditorList;
		}
	}
	public LanguageType LoadEditorLanguageData(e_language newLanguage, e_languageExtension newExtension)
	{
		string path = DirectoryFunction.CombinePath(this.repositoryPath, newLanguage.ToString()) + "." + newExtension.ToString().ToLower();
		//LanguageType languageData = new LanguageType();

		if (newExtension == e_languageExtension.Ini)
		{
			IniFile parser = new IniFile(path);
			return new StringDictionary(parser.KeysValues).ToList();
		}
		else
			return SerializationTemplate.Load<LanguageType>(path, newExtension == e_languageExtension.Bin);
	}
	public void SaveEditorLanguageData(e_language newLanguage, e_languageExtension newExtension, LanguageType languageData)
	{
		string path = DirectoryFunction.CombinePath(this.repositoryPath, newLanguage.ToString()) + "." + newExtension.ToString().ToLower();

		if (newExtension == e_languageExtension.Ini)
		{
			IniFile parser = new IniFile(path);
			StringDictionary languageTexts = new StringDictionary();

			languageData.ForEach(node => languageTexts.Add(node.Key, node.Value));
			parser.SaveFile(languageTexts);
		}
		else
			SerializationTemplate.Save<LanguageType>(path, languageData, newExtension == e_languageExtension.Bin);
	}
	#endregion
}
