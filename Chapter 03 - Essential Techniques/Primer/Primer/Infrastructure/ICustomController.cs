using System.Threading;
using System.Threading.Tasks;

namespace Primer.Infrastructure {
    public interface ICustomController {

        Task<long> GetPageSize(CancellationToken cToken);

        Task PostUrl(string newUrl, CancellationToken cToken);
    }
}
