using System;

namespace PhoneStore.ViewModels
{
    public class PageViewModel
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < TotalPages;

        public PageViewModel(int page, int count, int pageSize)
        {
            Page = page;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}