using LanguageExt;
using static LanguageExt.Prelude;

namespace Task2;
public enum AppointmentType
{
    Once,
    Recurring
}

public enum Frequency
{
   Daily,
   Weekly
}

public enum Unit
{
   Seconds,
   Minutes,
   Hours
}

[Flags]
public enum AppointmentDays
{
    None = 1,
    Monday = 2,
    Tuesday = 3,
    Wednesday = 4,
    Thursday = 5,
    Friday = 6,
    Saturday = 7,
    Sunday = 8
}

public record AppointmentDetails(DateTime NextDate, string Description);
    
public class Configuration
{
    public DateTime CurrentDate { get; set; }

    #region Configuration
    public AppointmentType Type { get; set; }
    public TimeSpan? OccursOnceAt { get; set; }
    public bool Enabled { get; set; } = true;
    public Frequency Occurs { get; set; }
    #endregion

    #region WeeklyConfigurationSection
     public int NumOfWeeks { get; set; }
     public AppointmentDays WeekDays { get; set; }
     #endregion

     #region DailyFrequencySection
     public TimeSpan OccursDailyAt { get; set; }
     public int Count { get; set; }
     public Unit Unit { get; set; }
     public TimeSpan StartTime { get; set; }
     public TimeSpan EndTime { get; set; }
     #endregion

     #region Limits
     public DateTime StartDate { get; set; }
     public DateTime EndDate { get; set; }
     #endregion
}

public delegate Fin<Configuration> Validator(Configuration configuration);
    

public class Scheduler
{
    public Fin<AppointmentDetails> CalculateNextDate(Configuration configuration)
    {
        Validator validator = Validation.ConfigurationValidator(Validation.Validators);
        //return validator(configuration)
        //.Bind(config => FinSucc(new AppointmentDetails(DateTime.Now, "Kasozi")));
        throw new NotImplementedException();
    }
}


