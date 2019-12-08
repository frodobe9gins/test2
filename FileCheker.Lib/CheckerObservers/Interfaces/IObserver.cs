using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCheker.Lib
{
    interface IObserver
    {
        void Subscribe(IProvider provider);
        void UnSubscribe(IProvider provider);
    }

}
