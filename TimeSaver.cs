using System.Text.Json;

namespace TNSR.Scoring;

/// <summary>
///     Utility class for saving and loading level finishing times for the game TNSR.
/// </summary>
public static class TimeSaver
{
    /// <summary>
    ///     The path to the file where the times are saved.
    /// </summary>
    public static string SavePath { get; set; } = "";
    static LevelTimeData _timeData = new();

    /// <summary>
    ///     Save the time taken to complete a level.
    /// </summary>
    /// <param name="levelNumber">Number of the level to save the score to.</param>
    /// <param name="timeTaken">Time taken to complete the level.</param>
    /// <param name="customTime">Optional custom time to use instead of the current time.</param>
    public static void SaveTime(int levelNumber, float timeTaken, DateTime? customTime = null)
    {
        if (_timeData.All(level => level.LevelNumber != levelNumber))
            _timeData.Add(new Level() { LevelNumber = levelNumber });
        _timeData[levelNumber]
            .Times
            .Add(new Time(timeTaken, customTime ?? DateTime.Now));
    }
    /// <summary>
    ///     Get the best time for a level.
    /// </summary>
    /// <param name="levelNumber">Number of the level to get the best time for.</param>
    /// <returns>The best time for the level specified.</returns>
    /// <exception cref="Exception">There are no times saved on the level.</exception>
    public static Time GetBestTime(int levelNumber)
    {
        return _timeData[levelNumber]
            .Times
            .OrderBy(time => time.TimeTaken)
            .FirstOrDefault() ?? throw new Exception($"No times for level {levelNumber}");
    }
    /// <summary>
    ///     Remove all times for a level except the best time and the most recent times.
    /// </summary>
    /// <param name="levelNumber">Number of the level for cleaning times.</param>
    /// <param name="recent">Number of recent scores to save. Defaults to 5.</param>
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
    /// <summary>
    ///     Write the time data to the file specified in <see cref="SavePath"/>.
    /// </summary>
    /// <exception cref="Exception">The path for saving times is not set.</exception>
    public static void WriteToFile()
    {
        if (SavePath == "")
            throw new Exception("SavePath not set");
        File.WriteAllText(
            SavePath,
            JsonSerializer.Serialize(
                _timeData,
                new JsonSerializerOptions() { AllowTrailingCommas = true }
            )
        );
    }
    /// <summary>
    ///    Load the time data from the file specified in <see cref="SavePath"/>.
    /// </summary>
    /// <exception cref="Exception">The path for saving times is not set.</exception>
    public static void ReadFromFile()
    {
        if (SavePath == "")
            throw new Exception("SavePath not set");
        var file = File.ReadAllText(SavePath);
        if (file.Length == 0)
            _timeData = new();
        else
            _timeData = JsonSerializer
                .Deserialize<LevelTimeData>(file)
                ?? throw new Exception("Failed to read file");
    }
    /// <summary>
    ///     Clear all time data in memory.
    /// </summary>
    public static void ClearData()
    {
        _timeData = new();
    }
}
