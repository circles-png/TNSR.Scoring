using System.Text.Json;

namespace TNSR.Scoring;
public static class TimeSaver
{
    public static string SavePath { get; set; } = "";
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
            .OrderBy(time => time.TimeTaken)
            .FirstOrDefault() ?? throw new Exception($"No times for level {levelNumber}");
    }
    public static void CleanTimes(int levelNumber, int recent = 5)
    {
        var times = new List<Time>() { GetBestTime(levelNumber) };
        times.AddRange(
            _timeData[levelNumber]
                .Times
                .OrderByDescending(time => time.TimeAchieved)
                .Take(recent)
        );
        _timeData[levelNumber].Times = times;
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
        if (file.Length == 0)
            _timeData = new();
        else
            _timeData = JsonSerializer
                .Deserialize<LevelTimeData>(file)
                ?? throw new Exception("Failed to read file");
        file.Dispose();
    }
    public static void ClearData()
    {
        _timeData = new();
    }
}
