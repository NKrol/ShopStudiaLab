using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Shop.Web.Dtos.Validators
{
    public class ProductQueryValidator : AbstractValidator<ProductQuery>
    {
        private int[] allowedPageSize = new[]
        {
            5, 10, 15
        };

        //private string[] allowedSortByColumnName =
        //    {nameof(Produkt.Name), nameof(Produkt.Description), nameof(Produkt.Category)};
        public ProductQueryValidator()
        {

            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSize.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPageSize)}]");
                }
            });

            //RuleFor(r => r.SortBy)
            //    .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnName.Contains(value))
            //    .WithMessage($"SortBy is optional, or must be in [{string.Join(",", allowedSortByColumnName)}]");

        }

    }
}
