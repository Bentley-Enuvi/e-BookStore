using FluentValidation;
using eBookStoreAPI.Application.Products.Command.AddBook;

namespace eBookStoreAPI.Presentation.CommandValidators;


    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Genre).NotEmpty().WithMessage("Genre is required.");
            RuleFor(x => x.ISBN).NotEmpty().WithMessage("ISBN is required.");
            RuleFor(x => x.AuthorName).NotEmpty().WithMessage("Author name is required.");
            RuleFor(x => x.PublishedYear).GreaterThan(0).WithMessage("Published year must be greater than 0.");
        }
    }

