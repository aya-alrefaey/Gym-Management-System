namespace Gym.BusinessLogic.ViewModels.Home
{
    public class HomeStatsViewModel
    {
        public int TotalMembers { get; set; }
        public int ActiveMembers { get; set; }
        public int TrainersCount { get; set; }

        public int UpcomingSessions { get; set; }
        public int OngoingSessions { get; set; }
        public int CompletedSessions { get; set; }
    }
}
