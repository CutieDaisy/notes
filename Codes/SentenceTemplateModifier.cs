// See https://aka.ms/new-console-template for more information

using rna.Base.Extensions;
using rna.Exceptions.Extensions;
using System.Text.Json;
using System.Text.RegularExpressions;


//Gender gender = Gender.Male;

//string pronoun = gender == Gender.Male ? "his" : "her";

//Title title = Title.Dr;

//string oldName = "[\"Araba Opoku-Agyeman\",\"Araba Opoku Agyeman\"]";

//string newName = "James Johnson";

//string occupation = "Engineer";
//string organisation = "Ghana Traders Association";
//string registrationNo = "GH89384098QJ";
//string address = "3851 plantation St";
//string effectiveDate = "3rd February, 2023";

var form = new Form();

var nums = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };

var json = JsonSerializer.Serialize(form); //.ToDictionary(c => c.ToString(), c => c);

var diction = JsonSerializer.Deserialize<Dictionary<string, string>>(json)!;

string inputs =
    @"{title} <strong>{JsonJoin~value-></strong> <em>a.k.a.</em> <strong>~=>oldName}</strong>, {InitSmCase~field->occupation->length->isEquals->8~=>a=>s=>occupation} with service number {registrationNo} of {organisation}, 
      and of {address}, wishes to be known and called {title} {newName} with effect from {effectiveDate}.
      All documents bearing {pronoun} former names are still valid.";



//Console.WriteLine(string.Join(",", nums[..^1]));

InsertIntoTemplete(ref inputs, diction);


Console.WriteLine(inputs);



/// ArgsType(Value, Field).FieldName('if field is used').Condition( IsGreaterThan, IsLessThan, IsLengthGreaterThan, IsLengthLessThan, IsJsonLengthGreaterThan, IsJsonLengthLessThan).ValueToCompare
/// Example : jsonjoin~value.</strong> <em>a.k.a.</em> <strong>.Is



void InsertIntoTemplete(ref string template, Dictionary<string, string> formDiction) {
    var fields = GetFields(template).Distinct().ToArray();

    for (int i = 0; i < fields.Length; i++) {
        var field = fields[i];

        var prefixedFields = field.Split("=>");


        var properFieldName = prefixedFields.Length > 1 ? prefixedFields[^1] : field;

        var modifiers = prefixedFields.Length > 1 ? prefixedFields[..^1] : Array.Empty<string>();

        if (formDiction.Where(d => d.Key.ToLower() == properFieldName.ToLower()).FirstOrDefault() is { } d) {
            if (d.Key is { } k) {
                string pattern = "{" + field.ToLower() + "}";

                Console.WriteLine(d.Value);

                var value = ModifyValue(modifiers, d.Value, formDiction);

                template = Regex.Replace(template, pattern, value, RegexOptions.IgnoreCase);
            }
            //Console.WriteLine(d.Key ?? "This One is Null!!!!");
        }
    }
}

IEnumerable<string> GetFields(string template) {
    var holders = template.Split("{");

    for (int i = 0; i < holders.Length; i++) {
        var item = holders[i];
        if (!string.IsNullOrEmpty(item)) {
            var holderEndIndex = item.IndexOf("}", StringComparison.OrdinalIgnoreCase);
            if (holderEndIndex != -1) {
                item = item.Substring(0, holderEndIndex);

                yield return item;
            }
        }
    }
}


string ModifyValue(string[] modifiers, string value, Dictionary<string, string> formDiction) {
    var map = "->";
    var tilde = '~';
    var valueIdentifier = $"{tilde}value{map}";
    var fieldIdentifier = $"{tilde}field{map}";
    for (int i = 0; i < modifiers.Length; i++) {
        var modifierExpression = modifiers[i].Low();

        //var modifierWithConditions = modifierExpression.Split("~value.");

        string[]? modifierWithExpression;
        string modifierString = string.Empty;
        string? modifierCondition = null;
        string? modifierValue = null;
        bool hasCondition = false;
        bool isConditionMet = false;

        if (modifierExpression.Contains(valueIdentifier)) {
            modifierWithExpression = modifierExpression.Split(valueIdentifier);
            modifierString = modifierWithExpression[0];
            modifierValue = modifierWithExpression.Length > 1 ? modifierWithExpression[1]?.Trim() : null;

            if (modifierValue is null) value.ThrowException($"No value was provide for the modifier: '{modifierString}");

            if (!modifierValue.EndsWith(tilde)) value.ThrowException($"The modifier: '{modifierString} must end with '{tilde}'");

            modifierValue = modifierValue.Trim(tilde);
        }
        else if (modifierExpression.Contains(fieldIdentifier)) {
            modifierWithExpression = modifierExpression.Split(fieldIdentifier);
            // jsonjoin~field->fieldName->valueProperty->isGreaterThan->value~
            modifierString = modifierWithExpression[0];
            var modifierArgs = modifierWithExpression.Length > 1 ? modifierWithExpression[1]?.Trim() : null;

            if (modifierArgs is null) value.ThrowException($"No field arguments were provided for the modifier: '{modifierString}");

            var args = modifierArgs.Split(map);

            if (args.Length != 4) value.ThrowException($"The field option for the modifier: '{modifierString}' must have 3 arguments or mappers");

            var fieldName = args[0];
            var valueProperty = args[1]; // Length, value
            modifierCondition = args[2];
            modifierValue = args[3];

            if (modifierValue is null) value.ThrowException($"No value was provide for the modifier: '{modifierString}");

            if (!modifierValue.EndsWith(tilde)) value.ThrowException($"The modifier: '{modifierString} must end with '{tilde}'");

            modifierValue = modifierValue.Trim(tilde);


            var modifierConditionEnum = modifierCondition?.GetEnumValue<TemplateModifierConditions>();

            if (modifierConditionEnum != null) hasCondition = true;


            if (formDiction.Where(d => d.Key.ToLower() == fieldName.ToLower()).FirstOrDefault() is { } item) {
                var valuePropertyEnum = valueProperty.GetEnumValue<TemplateModifierValueProperty>();

                isConditionMet = modifierConditionEnum switch {
                    TemplateModifierConditions.IsGreaterThan => valuePropertyEnum switch {
                        TemplateModifierValueProperty.value => item.Value.ToDecimal() > modifierValue.ToDecimal(),
                        TemplateModifierValueProperty.length => item.Value.Length > modifierValue.ToInt(),
                        _ => false
                    },
                    TemplateModifierConditions.IsGreaterThanOrEquals => valuePropertyEnum switch {
                        TemplateModifierValueProperty.value => item.Value.ToDecimal() >= modifierValue.ToDecimal(),
                        TemplateModifierValueProperty.length => item.Value.Length >= modifierValue.ToInt(),
                        _ => false
                    },
                    TemplateModifierConditions.IsLessThan => valuePropertyEnum switch {
                        TemplateModifierValueProperty.value => item.Value.ToDecimal() < modifierValue.ToDecimal(),
                        TemplateModifierValueProperty.length => item.Value.Length < modifierValue.ToInt(),
                        _ => false
                    },
                    TemplateModifierConditions.IsLessThanOrEquals => valuePropertyEnum switch {
                        TemplateModifierValueProperty.value => item.Value.ToDecimal() <= modifierValue.ToDecimal(),
                        TemplateModifierValueProperty.length => item.Value.Length <= modifierValue.ToInt(),
                        _ => false
                    },
                    TemplateModifierConditions.IsEquals => valuePropertyEnum switch {
                        TemplateModifierValueProperty.value => item.Value == modifierValue,
                        TemplateModifierValueProperty.length => item.Value.Length == modifierValue.ToInt(),
                        _ => false
                    },
                    TemplateModifierConditions.Contains => valuePropertyEnum switch {
                        TemplateModifierValueProperty.value => item.Value.Contains(modifierValue),
                        TemplateModifierValueProperty.length => throw item.ThrowException($"The property 'length' of the modifier: '{modifierString}' cannot use 'Contain' as a condition"),
                        _ => false
                    },
                    TemplateModifierConditions.Like => valuePropertyEnum switch {
                        TemplateModifierValueProperty.value => item.Value.Low().Contains(modifierValue.Low()),
                        TemplateModifierValueProperty.length => throw item.ThrowException($"The property 'length' of the modifier: '{modifierString}' cannot use 'Like' as a condition"),
                        _ => false
                    },
                    _ => false
                };

                //if (valuePropertyEnum == TemplateModifierValueProperty.value)
                //    item.Value.Length
            }


        }
        else modifierString = modifierExpression;






        TemplateModifier modifier = modifierString.GetEnumValue<TemplateModifier>();

        var applyModifier = !hasCondition || (hasCondition && isConditionMet);

        value = modifier switch {
            TemplateModifier.s => applyModifier ? value.Split(" ")[0].Low() is { } a && (a == "a" || a == "an") ? value : value.MakeWordPlural() : value,
            TemplateModifier.a => applyModifier ? value.IsPlural() ? value : ("aeiou".Contains(value.Low().Substring(0, 1)) ? $"an" : "a") + $" {value}" : value,
            TemplateModifier.InitUpCase => applyModifier ? value.ToPascalCase()! : value,
            TemplateModifier.InitSmCase => applyModifier ? value.ToCamelCase()! : value,
            TemplateModifier.UpCase => applyModifier ? value.Up() : value,
            TemplateModifier.SmCase => applyModifier ? value.Low() : value,
            TemplateModifier.JsonJoin => applyModifier ? string.Join(modifierValue, JsonSerializer.Deserialize<string[]>(value)!) : value,
            _ => value
        };
    }

    return value;
}


ModifierModel GetModifier(string modifierExpression, Dictionary<string, string> formDiction) {
    var argTypes = TemplateModifierArgType.GetNames<TemplateModifierArgType>()!;

    for (var i = 0; i < argTypes.Length; i++) {
        var argType = argTypes[i];
        var argTypeEnum = argType.GetEnumValue<TemplateModifierArgType>();

        var argTypeOperator = $"~{argType}.";

        //if (!modifierExpression.Contains(argTypeOperator)) return new() { ArgsValue = null, Name = modifierExpression };

        if (modifierExpression.Contains(argTypeOperator)) {
            var modifierWithArgsValue = modifierExpression.Split(argTypeOperator);
            var modifierString = modifierWithArgsValue[0];
            var modifierArgsValue = modifierWithArgsValue[1];

            TemplateModifier modifierEnum = modifierString.GetEnumValue<TemplateModifier>();



            if (argTypeEnum == TemplateModifierArgType.field && formDiction.Where(d => d.Key.ToLower() == argType.ToLower()).FirstOrDefault() is { } d) {
                //var hello = JsonSerializer.Deserialize<string[]>(d.Value);
            }

            ModifierModel modifier = argTypeEnum switch {
                var v => new() { },
                //_ => new()
            };


            return new() { ArgsValue = null, Name = modifierExpression };
        }


    }
    return new();
}

class ModifierModel {
    public string Name { get; set; }
    public string? ArgsValue { get; set; }
    public bool ConditionValid { get; set; }
    public bool IsConditionalModifier { get; set; }
}




public enum TemplateModifier {
    a, /// <summary>
       /// The article a or an. 
       /// </summary>
       /// 
    UpCase, /// <summary>
            /// All Upper Casing
            /// </summary>
            /// 
    SmCase, /// <summary>
            /// All Small or lower Casing
            /// </summary>
            /// 
    InitUpCase, /// <summary>
                /// Initial letter Upper Casing
                /// </summary>
                /// 
    InitSmCase, /// <summary>
                /// initial letter Lower Casing
                /// </summary>
                /// 
    s, /// <summary>
       /// Make word plural
       /// </summary>
       /// 
    JsonJoin,
}

public enum TemplateModifierArgType {
    field, value
}
public enum TemplateModifierValueProperty {
    length, value
}
public enum TemplateModifierConditions {
    IsGreaterThan, IsGreaterThanOrEquals, IsLessThan, IsLessThanOrEquals, IsEquals, Contains, Like
}

public enum GenderEnum { Male, Female }
public enum TitleEnum { Dr, Mr, Mrs, Miss, Ms }

public class Form {

    public string Title => TitleEnum.Dr.ToString();
    public string Gender => GenderEnum.Male.ToString();
    public string Pronoun => Gender == GenderEnum.Male.ToString() ? "his" : "her";

    public string OldName => "[\"Araba Opoku-Agyeman\",\"Araba Opoku Agyeman\"]";

    public string NewName => "James Johnson";

    public string Occupation => "Engineer";
    public string Organisation => "Ghana Traders Association";
    public string RegistrationNo => "GH89384098QJ";
    public string Address => "3851 plantation St";
    public string EffectiveDate => "3rd February, 2023";
}