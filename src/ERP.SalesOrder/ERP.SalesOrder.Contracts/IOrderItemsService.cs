using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.SalesOrder.Contracts;
public interface IOrderItemsService
{
    Task<IEnumerable<OrderItemDto>> GetAllAsync();
}
