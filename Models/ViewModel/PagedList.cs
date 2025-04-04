namespace NewsPortal.Models.ViewModel
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public PagedList(IEnumerable<T> items, int totalItems, int currentPage, int itemsPerPage)
        {
            Items = items.ToList();
            PagingInfo = new PagingInfo(totalItems, itemsPerPage, currentPage);
        }
    }
}