using System;

namespace Domain.domain
{

public enum AgeCategory
{
    ANI_6_8,
    ANI_9_11,
    ANI_12_15
}

public static class AgeCategoryExtensions
{
    public static string GetCategoryName(this AgeCategory category)
    {
        switch (category)
        {
            case AgeCategory.ANI_6_8:
                return "6-8 ani";
            case AgeCategory.ANI_9_11:
                return "9-11 ani";
            case AgeCategory.ANI_12_15:
                return "12-15 ani";
            default:
                throw new ArgumentException("Nu există o categorie de vârstă pentru: " + category);
        }
    }

    public static AgeCategory FromString(string categoryName)
    {
        foreach (AgeCategory category in Enum.GetValues(typeof(AgeCategory)))
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