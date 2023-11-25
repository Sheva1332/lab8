using System;

public interface IDataFormat
{
    IDataFormat Clone();
    void LoadData(string data);
    string SaveData();
}

public class CSVData : IDataFormat
{
    private string _data;

    public IDataFormat Clone() => new CSVData();

    public void LoadData(string data) => _data = data;

    public string SaveData() => _data;
}

public class XMLData : IDataFormat
{
    private string _data;

    public IDataFormat Clone() => new XMLData();

    public void LoadData(string data) => _data = data;

    public string SaveData() => _data;
}

public class JSONData : IDataFormat
{
    private string _data;

    public IDataFormat Clone() => new JSONData();

    public void LoadData(string data) => _data = data;

    public string SaveData() => _data;
}

public class DataFormatAdapter
{
    private IDataFormat _sourceFormat;

    public DataFormatAdapter(IDataFormat sourceFormat)
    {
        _sourceFormat = sourceFormat;
    }

    public void Convert(IDataFormat targetFormat)
    {
        string sourceData = _sourceFormat.SaveData();
        targetFormat.LoadData(sourceData);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Виберіть формат вхідних даних (csv, xml, json):");
        string inputFormat = Console.ReadLine().ToLower();

        Console.WriteLine("Виберіть формат вихідних даних (csv, xml, json):");
        string outputFormat = Console.ReadLine().ToLower();

        IDataFormat inputData = GetFormat(inputFormat);
        IDataFormat outputData = GetFormat(outputFormat);

        DataFormatAdapter adapter = new DataFormatAdapter(inputData);
        adapter.Convert(outputData);

        Console.WriteLine($"Дані у форматі {outputFormat.ToUpper()}:\n{outputData.SaveData()}");
    }

    static IDataFormat GetFormat(string format)
    {
        switch (format)
        {
            case "csv":
                return new CSVData();
            case "xml":
                return new XMLData();
            case "json":
                return new JSONData();
            default:
                throw new ArgumentException("Непідтримуваний формат");
        }
    }
}