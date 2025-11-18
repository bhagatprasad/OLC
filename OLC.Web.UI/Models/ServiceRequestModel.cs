namespace OLC.Web.UI.Models
{
    public class ServiceRequestModel : ServiceRequest
    {
        public ServiceRequestModel()
        {
            
        }
        public List<Category> categories { get; set; }
        public List<Priority> priorities { get; set; }
    }


    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    //public class Priority
    //{
    //    public long id { get; set; }
    //    public string name { get; set; }
    //}
}
