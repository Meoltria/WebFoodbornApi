using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFoodbornApi.Dtos
{
    public class IPageAndSortInputDto
    {
        public IPageAndSortInputDto()
        {
            Page = 1;
            per_Page = 10;
            SortBy = "Id Asc";
        }
        /// <summary>
        /// 默认为1，查询第一页
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 默认每页10条，最大每页100条
        /// </summary>
        private int per_Page;
        public int Per_Page
        {
            get { return per_Page; }
            set
            {
                per_Page = value > 100 ? 100 : value; ;
            }
        }
        /// <summary>
        /// 默认为Id Asc,多个排序字段用逗号分隔
        /// </summary>
        public string SortBy { get; set; }

    }
}
