using Excel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TMP_Text ChatBox;
    public TMP_Text SpeakerBox;
    public RawImage Speaker1;
    public RawImage Speaker2;
    public GameObject Mask1;
    public GameObject Mask2;
    public string filePath;
    public string sheetName;
    public Texture2D imageTexture1; 
    public Texture2D imageTexture2; 
    private Dictionary<int, Dictionary<string, string>> unitDictionary;
    public int sequence;
    private bool isImage1Visible = true; 
    // Start is called before the first frame update
    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "Text/Text.xlsx");
        sheetName = "Sheet1";
        unitDictionary = new Dictionary<int, Dictionary<string, string>>();
        ReadDataFromExcel(filePath,sheetName,unitDictionary);
        print(GetUnitData(1,"Text"));
        sequence = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AddData(Dictionary<int, Dictionary<string, string>> dictionaryName,int id, string key, string value)
    {
        if (!dictionaryName.ContainsKey(id))
        {
            dictionaryName[id] = new Dictionary<string, string>();
        }

        dictionaryName[id][key] = value;
    }

    private void ReadDataFromExcel(string filePath, string sheetName, Dictionary<int, Dictionary<string, string>> dictionaryName)
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();
        DataTable table = result.Tables[sheetName];


        DataRow headerRow = table.Rows[0];
        for (int i = 0; i < table.Columns.Count; i++)
        {
            table.Columns[i].ColumnName = headerRow[i].ToString();
        }
        table.Rows.RemoveAt(0);


        foreach (DataRow row in table.Rows)
        {
            for (int i =  1; i < table.Columns.Count; i++)
            {
                int rowID = int.Parse(row["ID"].ToString());
                string key = table.Columns[i].ColumnName.ToString();
                AddData(unitDictionary, rowID, key, row[key].ToString());
            }
        }

        excelReader.Close();
        stream.Close();
    }

    public string GetUnitData(int id, string key)
    {
        if (unitDictionary.ContainsKey(id) && unitDictionary[id].ContainsKey(key))
        {
            return unitDictionary[id][key];
        }

        return "0";
    }
    public void changeText(){
        ChatBox.text = GetUnitData(sequence,"Text");
        SpeakerBox.text = GetUnitData(sequence,"Text");
        Speaker1.texture = imageTexture1;
        Speaker2.texture = imageTexture2;
        isImage1Visible = !isImage1Visible;
        Mask1.SetActive(isImage1Visible);
        Mask2.SetActive(!isImage1Visible);
        sequence+=1;
    }

}
