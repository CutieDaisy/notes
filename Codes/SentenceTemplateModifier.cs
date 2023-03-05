// See https://aka.ms/new-console-template for more information

using rna.Base.Extensions;
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
    @"{title} <strong>{jsonjoin~value~</strong> <em>a.k.a.</em> <strong>=>oldName}</strong>, {initsmcase=>a=>s=>occupation} with service number {registrationNo} of {organisation}, 
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

    for (int i = 0; i < modifiers.Length; i++) {
        var modifierExpression = modifiers[i].Low();

        //var modifierWithConditions = modifierExpression.Split("~value.");



        var modifierWithConditions = modifierExpression.Split("~value~");
        var modifierString = modifierWithConditions[0];
        TemplateModifier modifier = modifierString.GetEnumValue<TemplateModifier>();

        var conditionalField = modifierWithConditions.Length > 1 ? modifierWithConditions[1] : null;


        if (conditionalField is { } f && formDiction.Where(d => d.Key.ToLower() == f.ToLower()).FirstOrDefault() is { } d) {
            //var hello = JsonSerializer.Deserialize<string[]>(d.Value);
        }

        value = modifier switch {
            TemplateModifier.s => value.Split(" ")[0].Low() is { } a && (a == "a" || a == "an") ? value : value.MakeWordPlural(),
            TemplateModifier.a => value.IsPlural() ? value : ("aeiou".Contains(value.Low().Substring(0, 1)) ? $"an" : "a") + $" {value}",
            TemplateModifier.initupcase => value.ToPascalCase()!,
            TemplateModifier.initsmcase => value.ToCamelCase()!,
            TemplateModifier.upcase => value.Up(),
            TemplateModifier.smcase => value.Low(),
            TemplateModifier.jsonjoin => string.Join(conditionalField, JsonSerializer.Deserialize<string[]>(value)!),
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
    upcase, /// <summary>
            /// All Upper Casing
            /// </summary>
            /// 
    smcase, /// <summary>
            /// All Small or lower Casing
            /// </summary>
            /// 
    initupcase, /// <summary>
                /// Initial letter Upper Casing
                /// </summary>
                /// 
    initsmcase, /// <summary>
                /// initial letter Lower Casing
                /// </summary>
                /// 
    s, /// <summary>
       /// Make word plural
       /// </summary>
       /// 
    jsonjoin,
}

public enum TemplateModifierArgType {
    field, value
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