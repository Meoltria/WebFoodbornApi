using System.ComponentModel.DataAnnotations;

namespace WebFoodbornApi.Dtos
{
    public class RoleOutput
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }

    public class RoleQueryInput : IPageAndSortInputDto
    {

    }

    public class RoleCreateInput
    {
        [Required(ErrorMessage = "请输入角色编码")]
        public string Code { get; set; }
        [Required(ErrorMessage = "请输入角色名称")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }

    public class RoleUpdateInput
    {
        [Required(ErrorMessage = "请输入角色Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "请输入角色编码")]
        public string Code { get; set; }
        [Required(ErrorMessage = "请输入角色名称")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
