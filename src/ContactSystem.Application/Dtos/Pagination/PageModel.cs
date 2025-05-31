namespace ContactSystem.Application.Dtos.Pagination;

public class PageModel
{
    public int Skip { get; set; } = 1;
    public int Take { get; set; } = 10;
}
