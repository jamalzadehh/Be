namespace Be.ViewModels.TeamVM
{
    public class TeamCreateVM
    {
        public string FullName { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile? Image { get; set; }    
    }

}
