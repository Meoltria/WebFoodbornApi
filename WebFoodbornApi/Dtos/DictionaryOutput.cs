using System.ComponentModel.DataAnnotations;

namespace WebFoodbornApi.Dtos
{
    public class DictionaryOutput
    {
        public int Id { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string RemarkName { get; set; }
        public string RemarkValue { get; set; }
    }

    public class DictionarySelectOutput
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class DictionaryQueryInput : IPageAndSortInputDto
    {
        public DictionaryQueryInput()
        {
            base.SortBy = "TypeCode,Code Asc";
        }

        public string TypeCode { get; set; }
        public string TypeName { get; set; }
    }

    public class DictionaryCreateInput
    {
        [Required(ErrorMessage = "请输入类别编码")]
        public string TypeCode { get; set; }
        [Required(ErrorMessage = "请输入类别名称")]
        public string TypeName { get; set; }
        [Required(ErrorMessage = "请输入字典编码")]
        public string Code { get; set; }
        [Required(ErrorMessage = "请输入字典名称")]
        public string Name { get; set; }
        public string RemarkName { get; set; }
        public string RemarkValue { get; set; }
    }

    public class DictonaryUpdateInput
    {
        [Required(ErrorMessage = "请输入字典Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "请输入类别编码")]
        public string TypeCode { get; set; }
        [Required(ErrorMessage = "请输入类别名称")]
        public string TypeName { get; set; }
        [Required(ErrorMessage = "请输入字典编码")]
        public string Code { get; set; }
        [Required(ErrorMessage = "请输入字典名称")]
        public string Name { get; set; }
        public string RemarkName { get; set; }
        public string RemarkValue { get; set; }
    }
}
