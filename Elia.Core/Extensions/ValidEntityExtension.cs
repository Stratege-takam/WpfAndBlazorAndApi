namespace Elia.Core.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// 
/// </summary>
public static class ValidEntityExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static IList<ValidationResult> CheckPropertyValidation(this object model)
    {
        var result = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);
        Validator.TryValidateObject(model, validationContext, result);
        if (model is IValidatableObject) (model as IValidatableObject)?.Validate(validationContext);
        return result;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static string GetErrors(this object model)
    {
        var errors = model.CheckPropertyValidation();
        return string.Join(";", errors.ToList().Select(e => e.ErrorMessage));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static List<string> GetMembers(this object model)
    {
        var errors = model.CheckPropertyValidation();
        return errors.ToList().Select(e => e.MemberNames?.FirstOrDefault()).ToList();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static bool IsValid(this object model)
    {
        var errors = model.CheckPropertyValidation();
        return errors.Count() == 0;
    }
}