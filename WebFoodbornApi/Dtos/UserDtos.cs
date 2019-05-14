using System.ComponentModel.DataAnnotations;

namespace WebFoodbornApi.Dtos
{
    public class UserOutput
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string UserTypeCode { get; set; }
        public string UserTypeName { get; set; }
        public int OrganazitionId { get; set; }
        public string OrganazitionCode { get; set; }
        public string OrganazitionName { get; set; }
        public int RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string GenderCode { get; set; }
        public string GenderName { get; set; }
        public string UserRankCode { get; set; }
        public string UserRankName { get; set; }
        public string Tel { get; set; }
        public string Info { get; set; }
        public string Status { get; set; }
    }

    public class UserSelectOutput
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class UserQueryInput : IPageAndSortInputDto
    {
        public string Name { get; set; }
    }

    public class PassWordInput
    {
        public string OldPassWord { get; set; }
        public string NewPassWord { get; set; }
    }

    public class UserCreateInput
    {
        [Required(ErrorMessage = "请输入用户编码")]
        public string Code { get; set; }
        [Required(ErrorMessage = "请输入姓名")]
        public string Name { get; set; }
        [Required(ErrorMessage = "请输入密码")]
        public string PassWord { get; set; }
        public string UserTypeCode { get; set; }
        public string UserTypeName { get; set; }
        public int OrganazitionId { get; set; }
        public int RoleId { get; set; }
        public string GenderCode { get; set; }
        public string GenderName { get; set; }
        public string UserRankCode { get; set; }
        public string UserRankName { get; set; }
        public string Tel { get; set; }
        public string Info { get; set; }
        public string Status { get; set; }
    }

    public class UserUpdateInput
    {
        [Required(ErrorMessage = "Id不能为空")]
        public int Id { get; set; }
        [Required(ErrorMessage = "请输入用户编码")]
        public string Code { get; set; }
        [Required(ErrorMessage = "请输入姓名")]
        public string Name { get; set; }
        public string UserTypeCode { get; set; }
        public string UserTypeName { get; set; }
        public int OrganazitionId { get; set; }
        public int RoleId { get; set; }
        public string GenderCode { get; set; }
        public string GenderName { get; set; }
        public string UserRankCode { get; set; }
        public string UserRankName { get; set; }
        public string Tel { get; set; }
        public string Info { get; set; }
        public string Status { get; set; }
    }
}
