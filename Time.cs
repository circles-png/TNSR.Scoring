namespace TNSR.Scoring;

/// <summary>
///     A time achieved on a level in TNSR.
/// </summary>
/// <param name="TimeTaken">Time taken to complete the level.</param>
/// <param name="TimeAchieved">Time when the level was completed.</param>
public record Time(float TimeTaken, DateTime TimeAchieved);
