using Contract.Models;

namespace Contract.ServiceCalls
{
    public interface IDocumentService
    {
        public Task<DocumentDto> GetDocumentById(Guid documentId);
    }
}
