namespace WebShopApp.ViewModel
{
    public class PageViewModel
    {
        public int PageNumber { get; set; }
        public int Total { get; set; }
        public string Query { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int CountryId { get; set; }
        public PageViewModel(int count, int pageNumber, int pageSize, string search, int brandId, int categoryId, int countryId)
        {
            PageNumber = pageNumber;
            Total = count / pageSize;
            Query = search;
            BrandId = brandId;
            CategoryId = categoryId;
            CountryId = countryId;
        }
        public bool HasPreviousPage { get => PageNumber > 1; }
        public bool HasNextPage { get => PageNumber < Total; }
    }
}
