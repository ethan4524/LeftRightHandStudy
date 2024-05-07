using System.IO;
using UnityEngine;

public class CSVWriter : MonoBehaviour
{
    void Start()
    {
        Debug.Log("datapath: " + Application.persistentDataPath);
    }

    public void AppendCSV(string[] data)
    {

        string filePath = Path.Combine(Application.persistentDataPath, "data.txt");

        // Create or append to the file
        StreamWriter writer = new StreamWriter(filePath, true);

        // Write each string in the array to its own line
        foreach (string line in data)
        {
            writer.WriteLine(line);
        }

        writer.Close();
        Debug.Log("Data appended to text file.");
    }
}