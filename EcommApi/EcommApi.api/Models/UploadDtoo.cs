namespace EcommApi.api.Models
{
    public class UploadDtoo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }


        public IFormFile Image { get; set; }

        public int Quantity { get; set; }
    }
}
