using System.Text.Json;

namespace TNSR.Scoring;
public static class TimeSaver
{
    public static string SavePath = "";
    static LevelTimeData _timeData = new();

    public static void SaveTime(int levelNumber, float timeTaken, DateTime? customTime = null)
    {
        if (_timeData.All(level => level.LevelNumber != levelNumber))
            _timeData.Add(new Level() { LevelNumber = levelNumber });
        _timeData[levelNumber]
            .Times
            .Add(new Time(timeTaken, customTime ?? DateTime.Now));
    }
    public static Time GetBestTime(int levelNumber)
    {
        return _timeData[levelNumber]
            .Times
            .OrderByDescending(time => time.TimeTaken)
            .FirstOrDefault() ?? throw new Exception($"No times for level {levelNumber}");
    }
    public static void CleanTimes()
    {
        throw new NotImplementedException();
    }
    public static void WriteToFile()
    {
        if (SavePath == "")
            throw new Exception("SavePath not set");
        File.WriteAllText(
            SavePath,
            JsonSerializer.Serialize(
                _timeData,
                new JsonSerializerOptions()
                {
                    AllowTrailingCommas = true,
                    WriteIndented = true
                }
            )
        );
    }
    public static void ReadFromFile()
    {
        if (SavePath == "")
            throw new Exception("SavePath not set");
        var file = new FileStream(SavePath, FileMode.Open);
        _timeData = JsonSerializer
            .Deserialize<LevelTimeData>(file)
            ?? throw new Exception("Failed to read file");
        file.Close();
    }

}
