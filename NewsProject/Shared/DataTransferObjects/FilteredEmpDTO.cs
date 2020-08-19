using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Shared.DataTransferObjects
{
    public class FilteredEmpDTO
    {
        public int Page { get; set; } = 1;
        public int RecordsPerPage { get; set; } = 4;
        public PaginationDTO Pagination
        {
            get
            {
                return new PaginationDTO()
                {
                    Page = Page,
                    RecordsPerPage = RecordsPerPage
                };
            }
        }

        public string Title { get; set; }
        public bool InWork { get; set; }
    }
}
