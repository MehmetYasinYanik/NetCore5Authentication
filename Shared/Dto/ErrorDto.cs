using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dto
{
    public class ErrorDto
    {
        public List<String> Errors { get; set; }

        public bool IsShow { get; set; }

        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public ErrorDto(string error, bool isShow)
        {
            Errors.Add(error);
            IsShow = isShow;
        }
        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
