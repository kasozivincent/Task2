using LanguageExt;
using static LanguageExt.Prelude;

namespace Task2.Tests;
    
[TestFixture]
public class Tests
{
    [Test]
    public void TestCurrentDateSuccess()
    {
        var configuration = new Configuration
        {
           CurrentDate = DateTime.Now.Date,
        };

        var result = Validation.ValidateCurrentDate(configuration);
        Assert.That(result.IsSucc, Is.True);
    }

    [Test]
    public void TestCurrentDateFailure()
    {
        var configuration = new Configuration
        {
            CurrentDate = new DateTime(2020, 2, 4),
        };

        var result = Validation.ValidateCurrentDate(configuration);
        Assert.That(result.IsSucc, Is.False);
    }

    [Test]
    public void ValidateAppointmentDatesSuccess()
    {
        var configuration = new Configuration
        {
            StartDate = new DateTime(2023, 12, 1),
            EndDate = new DateTime(2023, 12, 23)
        };

        var result = Validation.ValidateAppointmentDates(configuration);
        Assert.That(result.IsSucc, Is.True);
    }

    [Test]
    public void ValidateAppointmentDatesFailure()
    {
        var configuration = new Configuration
        {
            StartDate = new DateTime(2023, 12, 1),
            EndDate = new DateTime(2023, 11, 23)
        };

        var result = Validation.ValidateAppointmentDates(configuration);
        Assert.That(result.IsSucc, Is.False);
    }

    [Test]
    public void ValidateRecurringAppointmentSuccess()
    {
        var configuration = new Configuration
        {
            Type = AppointmentType.Recurring,
            OccursOnceAt = null  // null not applicable
        };

        var result = Validation.ValidateRecurringAppointment(configuration);
        Assert.That(result.IsSucc, Is.True);
    }

    [Test]
    public void ValidateRecurringAppointmentFailure()
    {
        var configuration = new Configuration
        {
            Type = AppointmentType.Recurring,
            OccursOnceAt = new TimeSpan(4, 7, 0)
        };

        var result = Validation.ValidateRecurringAppointment(configuration);
        Assert.That(result.IsSucc, Is.False);
    }

    [Test]
    public void ValidateOnceAppointmentSuccess()
    {
        var configuration = new Configuration
        {
            Type = AppointmentType.Once,
            OccursOnceAt = new TimeSpan(4, 7, 0)
        };

        var result = Validation.ValidateOnceAppointment(configuration);
        Assert.That(result.IsSucc, Is.True);
    }

    [Test]
    public void ValidateOnceAppointmentFailure()
    {
        var configuration = new Configuration
        {
            Type = AppointmentType.Once,
            OccursOnceAt = null
        };

        var result = Validation.ValidateOnceAppointment(configuration);
        Assert.That(result.IsSucc, Is.False);
    }

    [Test]
    public void ValidateDailyTimeIntervalsSuccess()
    {
        var configuration = new Configuration
        {
            StartTime = new TimeSpan(2, 9, 0),
            EndTime = new TimeSpan(4, 0, 0)
        };

        var result = Validation.ValidateDailyTimeIntervals(configuration);
        Assert.That(result.IsSucc, Is.True);
    }

    [Test]
    public void ValidateDailyTimeIntervalsFailure()
    {
        var configuration = new Configuration
        {
            StartTime = new TimeSpan(2, 9, 0),
            EndTime = new TimeSpan(1, 0, 0)
        };

        var result = Validation.ValidateDailyTimeIntervals(configuration);
        Assert.That(result.IsSucc, Is.False);
    }

    [Test]
    public void ValidateDailyRepetitionsSuccess()
    {
        var configuration = new Configuration
        {
            Count = 1
        };

        var result = Validation.ValidateDailyRepetitions(configuration);
        Assert.That(result.IsSucc, Is.True);
    }

    [Test]
    public void ValidateDailyRepetitionsFailure()
    {
        var configuration = new Configuration
        {
            Count = 0
        };

        var result = Validation.ValidateDailyRepetitions(configuration);
        Assert.That(result.IsSucc, Is.False);
    }

    [Test]
    public void ValidateConfigurationSuccess()
    {
        var configuration = new Configuration
        {
            StartDate = new DateTime(2020, 1, 5),
            EndDate = new DateTime(2020, 4, 10),
            CurrentDate = DateTime.Now.Date,
            Type = AppointmentType.Recurring,
            OccursOnceAt = null,
            Occurs = Frequency.Weekly,
            OccursDailyAt = new TimeSpan(4, 7, 0),
            Count = 2,
            Unit = Unit.Hours,
            StartTime = new TimeSpan(2, 9, 0),
            EndTime = new TimeSpan(6, 0, 0)
        };

        var validator = Validation.ConfigurationValidator(Validation.Validators);
        var result = validator(configuration);
        Assert.That(result.IsSucc, Is.True);
    }

    [Test]
    public void ValidateConfigurationWithWrongEndTimeFailure()
    {
        var configuration = new Configuration
        {
            StartDate = new DateTime(2020, 1, 5),
            EndDate = new DateTime(2020, 4, 10),
            CurrentDate = DateTime.Now.Date,
            Type = AppointmentType.Recurring,
            OccursOnceAt = null,
            Occurs = Frequency.Weekly,
            OccursDailyAt = new TimeSpan(4, 7, 0),
            Count = 2,
            Unit = Unit.Hours,
            StartTime = new TimeSpan(2, 9, 0),
            EndTime = new TimeSpan(1, 0, 0)
        };

        var validator = Validation.ConfigurationValidator(Validation.Validators);
        var result = validator(configuration);
        Assert.That(result.IsSucc, Is.False);
    }

    [Test]
    public void ValidateConfigurationWithWrongEndDateFailure()
    {
        var configuration = new Configuration
        {
            StartDate = new DateTime(2020, 1, 5),
            EndDate = new DateTime(2019, 4, 10),
            CurrentDate = DateTime.Now.Date,
            Type = AppointmentType.Recurring,
            OccursOnceAt = null,
            Occurs = Frequency.Weekly,
            OccursDailyAt = new TimeSpan(4, 7, 0),
            Count = 2,
            Unit = Unit.Hours,
            StartTime = new TimeSpan(2, 9, 0),
            EndTime = new TimeSpan(1, 0, 0)
        };

        var validator = Validation.ConfigurationValidator(Validation.Validators);
        var result = validator(configuration);
        Assert.That(result.IsSucc, Is.False);
    }

    [Test]
    public void ValidateConfigurationWithWrongOccursOnceAtFailure()
    {
        var configuration = new Configuration
        {
            StartDate = new DateTime(2020, 1, 5),
            EndDate = new DateTime(2019, 4, 10),
            CurrentDate = DateTime.Now.Date,
            Type = AppointmentType.Recurring,
            OccursOnceAt = new TimeSpan(4, 7, 0),
            Occurs = Frequency.Weekly,
            OccursDailyAt = new TimeSpan(4, 7, 0),
            Count = 2,
            Unit = Unit.Hours,
            StartTime = new TimeSpan(2, 9, 0),
            EndTime = new TimeSpan(1, 0, 0)
        };

        var validator = Validation.ConfigurationValidator(Validation.Validators);
        var result = validator(configuration);
        Assert.That(result.IsSucc, Is.False);
    }
}
