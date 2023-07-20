using MosqueApp.Models.Model;

namespace MosqueApp.Models.ViewModel
{
    public class MosqueViewModel
    {
        public List<Admin> Admins { get; set; }
        public List<Mosque> Mosques { get; set; }
        public List<City> Cities { get; set; }
        public List<Town> Towns { get; set; }
        public int SelectedCityId { get; set; }
        public int SelectedTownId { get; set; }
    }
}
