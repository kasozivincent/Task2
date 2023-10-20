using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Task2;

public static class Validation
{
    public static List<Validator> Validators = new List<Validator>
    {
        ValidateCurrentDate, ValidateAppointmentDates, ValidateRecurringAppointment,
        ValidateOnceAppointment, ValidateDailyTimeIntervals, ValidateDailyRepetitions
    };
    public static Validator ConfigurationValidator(IEnumerable<Validator> validators)
        => t => validators.Aggregate(FinSucc(t), (acc, validator) => acc.Bind(_ => validator(t)));

    public static Fin<Configuration> ValidateCurrentDate(Configuration configuration)
    {
        if (configuration.CurrentDate != DateTime.Now.Date)
            return Error.New("Current date must be today's date");
        return configuration;
    }

    public static Fin<Configuration> ValidateAppointmentDates(Configuration configuration)
    {
        if (configuration.StartDate > configuration.EndDate)
            return Error.New("Start date must be earlier than end date");
        return configuration;
    }

    public static Fin<Configuration> ValidateRecurringAppointment(Configuration configuration)
    {
        if (configuration.Type == AppointmentType.Recurring && configuration.OccursOnceAt != null)
            return Error.New("If an appointment is repetitive, OccursOnceAt must be undefined");
        return configuration;
    }

    public static Fin<Configuration> ValidateOnceAppointment(Configuration configuration)
    {
        if (configuration.Type == AppointmentType.Once && configuration.OccursOnceAt == null)
            return Error.New("If an appointment occurs once, OccursOnceAt must be defined");
        return configuration;
    }

    public static Fin<Configuration> ValidateDailyTimeIntervals(Configuration configuration)
    {
        if (configuration.StartTime >= configuration.EndTime)
            return Error.New("Start time must be earlier than end time for appointments that occur multiple times a day");
        return configuration;
    }

    public static Fin<Configuration> ValidateDailyRepetitions(Configuration configuration)
    {
        if (configuration.Count <= 0 || configuration.Count > 10)
            return Error.New("Numbers of repetitions per day must not be 0 and can't exceed 10");
        return configuration;
    }

}
