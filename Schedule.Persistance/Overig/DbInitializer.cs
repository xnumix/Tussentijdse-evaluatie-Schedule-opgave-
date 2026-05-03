using Microsoft.Data.SqlClient;

namespace Schedule.Persistance.Overig
{
    public static class DbInitializer
    {
        private const string CreateTablesSql = @"
IF OBJECT_ID('dbo.Days', 'U') IS NULL
    CREATE TABLE Days (
        Id        INT IDENTITY(1,1) PRIMARY KEY,
        Date      DATE      NOT NULL UNIQUE,
        StartTime TIME(0)   NOT NULL,
        EndTime   TIME(0)   NOT NULL
    );

IF OBJECT_ID('dbo.Activities', 'U') IS NULL
    CREATE TABLE Activities (
        Id                  INT IDENTITY(1,1) PRIMARY KEY,
        DayId               INT           NOT NULL,
        Type                NVARCHAR(20)  NOT NULL,
        StartTime           TIME(0)       NOT NULL,
        Name                NVARCHAR(100) NULL,
        StudentCount        INT           NULL,
        DurationInMinutes   INT           NULL,
        TravelTimeInMinutes INT           NULL,
        CONSTRAINT FK_Activities_Days FOREIGN KEY (DayId)
            REFERENCES Days(Id) ON DELETE CASCADE
    );
";

        public static void Initialize(string connectionString)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            using SqlCommand command = new(CreateTablesSql, connection);
            command.ExecuteNonQuery();
        }
    }
}
