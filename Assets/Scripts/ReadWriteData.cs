using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ReadWriteData : MonoBehaviour
{

    private List<string> names;
    private string fileNameLvl1;
    private string fileNameLvl2;

    private string recordList;

    private string highestScoreLvl1;
    private string highestScoreLvl2;


    // Start is called before the first frame update
    void Start()
    {
        
        fileNameLvl1 = "LevelOne.txt";
        fileNameLvl2 = "LevelTwo.txt";

        highestScoreLvl1 = "00m00s";
        highestScoreLvl2 = "00m00s";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Reading the txt file and extracting the top times (scores) and names
    private void readTextFile(int level)
    {

        string path = "";

        if (level == 1)
        {
            path = Application.persistentDataPath + "/" + fileNameLvl1;
        }
        else
        {
            path = Application.persistentDataPath + "/" + fileNameLvl2;
        }

        string content = "";


        if (System.IO.File.Exists(path))
        {
            content = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
        }
        else
        {

            if (level == 1)
            {
                TextAsset myFileData = Resources.Load("LevelOne") as TextAsset;
                content = myFileData.text;
              
            }
            else
            {
                TextAsset myFileData = Resources.Load("LevelTwo") as TextAsset;
                content = myFileData.text;
               
            }

        }

        string[] lines = content.Split('\n');
        Dictionary<string, string> topScores = new Dictionary<string, string>();

        for (int i = 0; i < lines.Length; i++)
        {
            string[] line = lines[i].Split(": ");
            if (line.Length == 2)
            {
                topScores.Add(line[0], line[1]);

            }
        }

        var list = topScores.ToList();
        list.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

        if (level == 1)
        {
            highestScoreLvl1 = list[0].Value;
 
        }
        else
        {
            highestScoreLvl2 = list[0].Value;
        }

        
        recordList = "";

        for (int i = 0; i < 5; i++)
        {
            if (i < list.Count)
            {
                recordList += (i + 1) + ". " + list[i].Key + " " + list[i].Value + "\n";
            }
        }


       names = list.Select(pair => pair.Key).ToList();

    }


    // Appending a new text line of the saved information to the txt file
    private void AppendInTextFile(int level, string str)
   {

        string filePath = "";

        if (level == 1)
        {

            filePath = Application.persistentDataPath + "/" + fileNameLvl1;

        }
        else
        {
            filePath = Application.persistentDataPath + "/" + fileNameLvl2;

        }


        if (!System.IO.File.Exists(filePath))
       {

            TextAsset myFileData;

            if (level == 1)
            {
                myFileData = Resources.Load("LevelOne") as TextAsset;


            }
            else
            {
                myFileData = Resources.Load("LevelTwo") as TextAsset;

            }


           if (myFileData != null)
           {
               System.IO.File.WriteAllText(filePath, myFileData.text);
           }
           else
           {

                return;
           }
       }

       System.IO.File.AppendAllText(filePath, "\n" + str);


    }

    // Calling the AppendInTextFile method to write the new information to the txt file
    public void Write(int level, string line)
    {
        if (level == 1)
        {

            AppendInTextFile(1, line);
            readTextFile(1);
        }
        else
        {
            
            AppendInTextFile(2,line);
            readTextFile(2);
        }
    }


    // calling the readTextFile method to get the top scores
    public string GetRecordList(int level)
    {
        if (level == 1)
        {
            readTextFile(1);
            return recordList;
        }
        else
        {
            readTextFile(2);
            return recordList;
        }
    }

    // calling the readTextFile method to get the highest score
    public string GetHighestScore(int level)
    {
        if (level == 1)
        {
            readTextFile(1);
            //Debug.Log("Highest Score for level 1: " + highestScoreLvl1);
            return highestScoreLvl1;
        }
        else
        {
            readTextFile(2);
            //Debug.Log("Highest Score: " + highestScoreLvl2);
            return highestScoreLvl2;
        }

    }

    // calling the readTextFile method to get the names list
    public List<string> GetNames(int level)
    {
        if (level == 1)
        {
            readTextFile(1);
        }
        else
        {
            readTextFile(2);
        }
        return names;
    }

    }
