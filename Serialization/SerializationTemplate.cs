using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

static public class SerializationTemplate {

	public static void Save<T>(string path, T whatYouWantToSave, bool binarySave = false) where T : class
	{
		BinaryFormatter		serializerBinary	= new BinaryFormatter();
		XmlSerializer		serializerXML		= new XmlSerializer(typeof(T));

		try
		{
			using (var stream = new FileStream(path, FileMode.Create))
			{	
				if (binarySave)		serializerBinary.Serialize(stream, whatYouWantToSave);
				else				serializerXML.Serialize(stream, whatYouWantToSave);
			}
		}
		catch (Exception e)			{ Debug.Log(e.Message); }
	}

	public static T Load<T>(string path, bool binarySave = false) where T : class
	{
		BinaryFormatter		serializerBinary	= new BinaryFormatter();
		XmlSerializer		serializerXML		= new XmlSerializer(typeof(T));

		try
		{
			using (var stream = new FileStream(path, FileMode.Open))
			{
				if (binarySave)		return serializerBinary.Deserialize(stream) as T;
				else				return serializerXML.Deserialize(stream) as T;
			}
		}
		catch (Exception e)				{ Debug.Log(e.Message); return null; }
	}
}
