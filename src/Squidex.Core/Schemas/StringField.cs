﻿// ==========================================================================
//  StringField.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Squidex.Core.Schemas.Validators;

namespace Squidex.Core.Schemas
{
    public sealed class StringField : Field<StringFieldProperties>
    {
        public StringField(long id, string name, StringFieldProperties properties) 
            : base(id, name, properties)
        {
        }

        protected override IEnumerable<IValidator> CreateValidators()
        {
            if (Properties.IsRequired)
            {
                yield return new RequiredStringValidator();
            }

            if (Properties.MinLength.HasValue || Properties.MaxLength.HasValue)
            {
                yield return new StringLengthValidator(Properties.MinLength, Properties.MaxLength);
            }

            if (!string.IsNullOrWhiteSpace(Properties.Pattern))
            {
                yield return new PatternValidator(Properties.Pattern, Properties.PatternMessage);
            }

            if (Properties.AllowedValues != null)
            {
                yield return new AllowedValuesValidator<string>(Properties.AllowedValues.ToArray());
            }
        }

        protected override object ConvertValue(JToken value)
        {
            return value.ToString();
        }
    }
}
