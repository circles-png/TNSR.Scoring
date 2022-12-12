using System.Text.Json;

namespace TNSR.Scoring;
public static class TimeSaver
{
    public static string SavePath = "";
    static LevelTimeData _timeData = new();

    public static void SaveTime(int levelNumber, float timeTaken, DateTime? customTime)
    {
        _timeData[levelNumber]
            .Add(new Time(timeTaken, customTime ?? DateTime.Now));
    }
    public static Time GetBestTime(int levelNumber)
    {
        return _timeData[levelNumber]
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
            JsonSerializer.Serialize(_timeData)
        );
    }
    public static void ReadFromFile()
    {

    }
}

public record Time(float TimeTaken, DateTime TimeAchieved);
