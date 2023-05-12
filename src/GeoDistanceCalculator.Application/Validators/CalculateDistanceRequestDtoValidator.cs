using FluentValidation;
using GeoDistanceCalculator.Application.Dtos;

namespace GeoDistanceCalculator.Application.Validators;

public class CalculateDistanceRequestDtoValidator : AbstractValidator<CalculateDistanceRequestDto>
{
    public CalculateDistanceRequestDtoValidator()
    {
        RuleFor(x => x.FirstCoordinate)
            .NotNull()
            .WithMessage(GetErrorMessage(nameof(CalculateDistanceRequestDto.FirstCoordinate)));

        RuleFor(x => x.SecondCoordinate)
            .NotNull()
            .WithMessage(GetErrorMessage(nameof(CalculateDistanceRequestDto.SecondCoordinate)));
    }

    private static string GetErrorMessage(string propertyName) => $"A valid value for {propertyName} must be provided";
}