using Abp.Application.Services.Dto;
using System;

namespace Factora.Products.Dto
{
    public class PagedProductResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
