using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.Category
{
    public class ListCategoriesResult
    {
        public IEnumerable<string> Categories { get; set; } = [];
    }
}
