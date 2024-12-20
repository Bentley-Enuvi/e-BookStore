using FluentValidation;
using eBookStoreAPI.Application.Products.Command.AddBook;

namespace eBookStoreAPI.Presentation.CommandValidators;


    public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {
        public AddToCartCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId is required.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("quantity must be greater than 0.");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("user id does not exist.");
        }
    }

