namespace Application.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int PendingOrders { get; set; }
        public int ActiveClients { get; set; }
        public List<ActivityByDateDto> ActivityByDate { get; set; } = new List<ActivityByDateDto>();
    }

    public class ActivityByDateDto
    {
        public string Date { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
