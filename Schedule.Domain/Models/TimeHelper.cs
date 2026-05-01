using Schedule.Domain.Repository;

namespace Schedule.Domain.Models
{
    public static class TimeHelper
    {
        /// <summary>
        /// Calculates the end time by adding a specified duration, in minutes, to a given start time.
        /// </summary>
        /// <param name="startTime">The time at which the interval begins. Must be less than or equal to the resulting end time.</param>
        /// <param name="durationInMinutes">The duration to add to the start time, in minutes. Must be a positive value.</param>
        /// <returns>A TimeOnly value representing the end time after adding the specified duration to the start time.</returns>
        public static TimeOnly CalculateEndTime(TimeOnly startTime, int durationInMinutes)
        {
            // Validate that the duration is positive.
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(durationInMinutes, nameof(durationInMinutes));
            // Validate that the resulting end time does not wrap past midnight.
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startTime, startTime.AddMinutes(durationInMinutes));

            return startTime.AddMinutes(durationInMinutes);
        }

        /// <summary>
        /// Validates that the specified activity does not overlap with any activities in the provided list.
        /// </summary>
        /// <param name="newActivity">The activity to validate for overlaps. The activity's start and end times are compared against existing
        /// activities.</param>
        /// <param name="existingActivities">A list of existing activities to check for overlaps with the new activity. Cannot be null.</param>
        /// <exception cref="InvalidOperationException">Thrown if the new activity overlaps with any activity in the existing activities list.</exception>
        public static void ValidateNoOverlaps(IPlannableActivity newActivity, List<IPlannableActivity> existingActivities)
        {
            foreach (IPlannableActivity existingActivity in existingActivities)
            {
                if (newActivity.StartTime < existingActivity.EndTime && existingActivity.StartTime < newActivity.EndTime)
                {
                    throw new InvalidOperationException($"Overlap detected: [{newActivity}] overlaps with [{existingActivity}].");
                }
            }
        }

       /// <summary>
       /// Determines whether the specified activity fits entirely within the given schedule time range.
       /// </summary>
       /// <param name="activity">The activity to evaluate for inclusion within the schedule. The activity's start and end times are compared to the schedule boundaries.</param>
       /// <param name="scheduleStartTime">The start time of the schedule. Activities must not begin before this time to be considered fitting.</param>
       /// <param name="scheduleEndTime">The end time of the schedule. Activities must not end after this time to be considered fitting.</param>
       /// <returns>true if the activity's start time is greater than or equal to the schedule start time and its end time is less than or equal to the schedule end time; otherwise, false.</returns>
        public static bool FitsInSchedule(IPlannableActivity activity, TimeOnly scheduleStartTime, TimeOnly scheduleEndTime) => activity.StartTime >= scheduleStartTime && activity.EndTime <= scheduleEndTime;
    }
}
