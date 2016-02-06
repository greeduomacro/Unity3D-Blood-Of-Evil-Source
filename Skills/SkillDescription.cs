using UnityEngine;
using System.Collections;

public class SkillDescription	{
    private string description;
    public string Description { get { return description; } set { if (value != "") description = value; } }
    private string levelInformation;
    public string LevelInformation { get { return levelInformation; } set { if (value != "") levelInformation = value; } }


	public SkillDescription() {}
}
