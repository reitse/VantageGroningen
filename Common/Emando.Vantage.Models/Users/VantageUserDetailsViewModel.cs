namespace Emando.Vantage.Models.Users
{
    public class VantageUserDetailsViewModel : VantageUserViewModel
    {
        public string[] Roles { get; set; }

        public VantageUserCompetitionRightViewModel[] CompetitionRights { get; set; }
    }
}