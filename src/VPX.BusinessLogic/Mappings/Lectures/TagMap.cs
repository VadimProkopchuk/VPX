using VPX.ApiModels;
using VPX.Domain;

namespace VPX.BusinessLogic.Mappings.Lectures
{
    internal class TagMap
    {
        public static TagModel Map(Tag tag)
        {
            if (tag == null)
            {
                return null;
            }

            return new TagModel
            {
                Display = tag.Name,
                Value = tag.Id
            };
        }

        public static Tag Map(TagModel tag)
        {
            if (tag == null)
            {
                return null;
            }

            return new Tag(tag.Value.Value)
            {
                Name = tag.Display,
            };
        }
    }
}
