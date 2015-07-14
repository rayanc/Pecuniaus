
namespace Pecuniaus.Models.User
{
    public class GroupPermission
    {
        public int ModuleId { get; set; }
        public int GroupID { get; set; }
        public int PageId { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Edit { get; set; }
        public bool IsFormDisabled { get; set; }
    }
}
