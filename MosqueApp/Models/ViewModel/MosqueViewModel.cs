using MosqueApp.Models.Model;

namespace MosqueApp.Models.ViewModel
{
    public class MosqueViewModel 
    {
        public Photos Photo { get; set; }
        public Mosque Mosque { get; set; }
        public List<Admin> Admins { get; set; }
        public List<Mosque> Mosques { get; set; }
        public List<City> Cities { get; set; }
        public List<Town> Towns { get; set; }
        public List<Photos> Photos { get; set; }
        public int SelectedCityId { get; set; }
        public int SelectedTownId { get; set; }
    }
}
