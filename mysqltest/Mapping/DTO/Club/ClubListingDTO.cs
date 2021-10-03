using mysqltest.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mysqltest.Mapping.DTO
{
    public class ClubListingDTO : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClubType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.TryParse(ClubType, true, out ClubType result))
            {
                yield return new ValidationResult("Invalid club type", new[] { nameof(ClubType) });
            }

            ClubType = result.ToString(); //normalize Type
        }
    }
}