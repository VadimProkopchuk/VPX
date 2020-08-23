using JML.ApiModels;
using JML.Domain;
using System;

namespace JML.BusinessLogic.Mappings.Lectures
{
    internal class LiteratureMap
    {
        public static LiteratureModel Map(Literature literature)
        {
            if (literature == null)
            {
                return null;
            }

            return new LiteratureModel
            {
                Content = literature?.Content ?? String.Empty,
                ModifiedAt = literature?.ModifiedAt ?? DateTime.Now,
            };
        }
    }
}
