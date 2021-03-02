using EPiServer.Core;

namespace AlloyAdvance.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
