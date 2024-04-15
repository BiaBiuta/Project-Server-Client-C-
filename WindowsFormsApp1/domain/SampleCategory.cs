using System;
using System.Linq;

namespace WindowsFormsApp1.domain

{
    public enum SampleCategory
    {
        [CategoryName("Desen")] DESEN,

        [CategoryName("Cautare comoara")] CAUTARE_COMOARA,

        [CategoryName("Poezie")] POEZIE
    }

    public class CategoryNameAttribute : Attribute
    {
        public string Name { get; }

        public CategoryNameAttribute(string name)
        {
            Name = name;
        }
    }

    public static class SampleCategoryExtensions
    {
        public static string GetCategoryName(this SampleCategory category)
        {
            var attribute = category.GetType()
                .GetField(category.ToString())
                .GetCustomAttributes(typeof(CategoryNameAttribute), false)
                .SingleOrDefault() as CategoryNameAttribute;
            return attribute?.Name ?? category.ToString();
        }

        public static SampleCategory FromString(string categoryName)
        {
            foreach (SampleCategory category in Enum.GetValues(typeof(SampleCategory)))
            {
                if (category.GetCategoryName().Equals(categoryName, StringComparison.OrdinalIgnoreCase))
                {
                    return category;
                }
            }

            throw new ArgumentException("Nu există o categorie de vârstă pentru: " + categoryName);
        }
    }
}