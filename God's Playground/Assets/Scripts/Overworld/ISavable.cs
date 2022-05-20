using System.Collections.Generic;

internal interface ISavable
{
    string GetData();

    void LoadData(string data);

    int ID { get; set; }
}