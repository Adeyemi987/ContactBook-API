using System;

namespace ContactBook.Utilities
{
    public class Pagination
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public Pagination()
        {
            PageSize = 3;
            CurrentPage = 1;
        }

        public Pagination(int PageSize, int CurrentPage)
        {
            this.PageSize = PageSize > 10 ? 10 : PageSize;
            this.CurrentPage = CurrentPage < 1 ? 1 : CurrentPage;
        }
    }


}
