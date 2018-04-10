using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_VS_Code_test.Models.TagsViewModels
{
    public class AddNewTagViewModel
    {
        public Tag Tag {get; set;}
        public IList<string> AllCategories {get; set;}
    }
}