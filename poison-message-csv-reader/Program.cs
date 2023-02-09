using CsvHelper;
using System.Globalization;

Console.WriteLine("Hello, World!");

using (var reader = new StreamReader("c:\\Users\\steve\\Downloads\\PoisonMessages.csv"))
using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    var records = csvReader.GetRecords<PoisonMessage>().ToList();
    Console.WriteLine($"Record count: {records.Count()}");

    Console.WriteLine("iterating over records");
    var groupedRecords = records.GroupBy(r => r.ConvertedDatetime);

    foreach (var groupedRecord in groupedRecords)
    {
        Console.WriteLine($"Grouped Record DateTime: {groupedRecord.Key} | Group Count: {groupedRecord.Count()}");
    }
}

public class PoisonMessage
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTime Timestamp{ get; set; }
    public string FailureCause { get; set; }
    public string MessageContent { get; set; }
    public string ConvertedDatetime => Timestamp.ToString("yyyy:MM:dd");
}