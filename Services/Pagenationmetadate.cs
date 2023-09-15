namespace CityInfo.Services
{
    public class Pagenationmetadate
    {
        public int TotalItemcount { get; set; }
        public int TotalPagecount { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
        public Pagenationmetadate(int totalItemcount,int pageSize,int currentPage)
        {
            totalItemcount = TotalItemcount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPagecount = (int)Math.Ceiling(totalItemcount / (double)pageSize);

        }

    }
}
