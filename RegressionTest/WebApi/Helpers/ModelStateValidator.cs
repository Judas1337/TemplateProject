using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TemplateProject.RegressionTest.WebApi.Helpers
{
    internal static class ModelStateValidator
    {
        public static void AssertModelIsValid(object model)
        {
            var validationResult = Validate(model);

            Assert.IsFalse(validationResult.Any(), string.Join(", ", validationResult.Select(x => x.ErrorMessage).ToArray()));
        }

        public static void AssertNumberOfModelFaultsEqualTo(object model, int expectedNumberOfFaults)
        {
            var validationResult = Validate(model);
            var actualNumberOfFaults = validationResult.Count();
            Assert.AreEqual(expectedNumberOfFaults, actualNumberOfFaults, $"Expected {expectedNumberOfFaults} faults, contained {actualNumberOfFaults}");
        }


        public static void AssertFieldIsInvalid(object model, string fieldname)
        {
            var validationResult = Validate(model);
            Assert.IsTrue(validationResult.Any(), $"{fieldname} wasn't invalid");
            bool fieldIsInvalid = false;
            foreach (var result in validationResult)
            {
                fieldIsInvalid = result.MemberNames.Contains(fieldname);
                if (fieldIsInvalid) break;
            }
            Assert.IsTrue(fieldIsInvalid, $"{fieldname} wasn't invalid");
        }

        private static IList<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);

            Validator.TryValidateObject(model, validationContext, results, true);
            return results;
        }
    }
}
