using CsvHelper;
using System.Globalization;

using (var reader = new StreamReader("c:\\Users\\steve\\Downloads\\PoisonMessages.csv"))
using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    var records = csvReader.GetRecords<PoisonMessage>().ToList();
    var groupedRecords = records.GroupBy(r => r.ConvertedDatetime);

    foreach (var groupedRecord in groupedRecords)
    {
        if (groupedRecord.Count() > 50)
        {
            Console.WriteLine($"Large Dataset Date: {groupedRecord.Key} | Count: {groupedRecord.Count()}");
            
            var groupedByFailure = groupedRecord.GroupBy(r => r.FailureCause);
            if (groupedByFailure.Count() > 10)
            {
                foreach (var failure in groupedByFailure)
                {
                    Console.WriteLine($"Failure: {failure.Key.Substring(0, 150)} | Count: {failure.Count()}");
                }
            }
        }
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