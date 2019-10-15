using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WS_SigloXXI
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void GenerarBoleta(int IdReserva);
    }
}
