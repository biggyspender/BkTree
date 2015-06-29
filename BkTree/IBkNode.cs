using System.Text;
using System.Threading.Tasks;

namespace BkTree
{
    public interface IBkNode<out T>
    {
        T Value
        {
            get;
        }
    }
}
