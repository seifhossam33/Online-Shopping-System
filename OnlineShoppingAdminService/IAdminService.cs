using OnlineShoppingAdminService.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OnlineShoppingAdminService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminService" in both code and config file together.
    [ServiceContract]
    public interface IAdminService
    {
        [OperationContract(IsOneWay = true)]
        void AddItem(Classes.Item item);

        [OperationContract(IsOneWay = true)]
        void DeleteItem(string id);

        [OperationContract(IsOneWay = false)]
        List<String> RetrieveID();

        [OperationContract(IsOneWay = false)]
        Classes.Item RetriveItemToEdit(string id);

        [OperationContract(IsOneWay = true)]
        void EditItem(Classes.Item tmp);

        [OperationContract(IsOneWay = false)]
        List<Item> ViewItems();
    }
}
