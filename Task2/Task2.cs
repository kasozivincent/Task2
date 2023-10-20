namespace Task2
{
    using System;
    using System.Diagnostics.Contracts;

    namespace Task2
    {
        enum AppointmentType
        {
            Once,
            Recurring
        }

        enum Frequency
        {
            Daily,
            Weekly
        }

        enum Unit
        {
            Seconds,
            Minutes,
            Hours
        }

        [Flags]
        enum AppointmentDays
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

        record AppointmentDetails(DateTime NextDate, string Description);
        class Configuration
        {
            public DateTime CurrentDate { get; set; }

            #region Configuration
            public AppointmentType Type { get; set; }
            public DateTime? OccursOnceAt { get; set; }
            public bool Enabled { get; set; } = true;
            public Frequency Occurs { get; set; }
            #endregion

            #region WeeklyConfigurationSection
            public int NumOfWeeks { get; set; }
            public AppointmentDays WeekDays { get; set; }
            #endregion

            #region DailyFrequencySection
            public DateTime OccursDailyAt { get; set; }
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

        class Scheduler
        {
            public AppointmentDetails CalculateNextDate(Configuration configuration)
            {
                Contract.Requires(configuration.CurrentDate == DateTime.Now, "Current date must be today's date");

                Contract.Requires(configuration.StartDate < configuration.EndDate,
                    "Start date must be earlier than end date");

                Contract.Requires(configuration.Type == AppointmentType.Recurring
                    && configuration.OccursOnceAt != null, "If an appointment is repetitive, OccursOnceAt must be undefined");

                Contract.Requires(configuration.Type == AppointmentType.Once
                    && configuration.OccursOnceAt == null, "If an appointment occurs once, OccursOnceAt must be defined");

                Contract.Requires(configuration.Count > 0 && configuration.Count < 10,
                    "Numbers of repetitions per day must not be 0 and can't exceed 10");

                Contract.Requires(configuration.StartTime < configuration.EndTime,
                    "Start time must be earlier than end time for appointments that occur multiple times a day");

                throw new NotImplementedException();
            }
        }
    }

}