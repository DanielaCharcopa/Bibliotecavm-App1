using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class PurchaseRequestLog
    {
        PurchaseRequestDat objPur = new PurchaseRequestDat();

        // Método para mostrar todas las solicitudes de compra
        public DataSet showPurchaseRequest()
        {
            return objPur.showPurchaseRequest();
        }

        // Método para mostrar solicitudes de compra en formato DDL
        public DataSet showPurchaseRequestDDL()
        {
            return objPur.showPurchaseRequestDDL();
        }

        // Método para guardar una nueva solicitud de compra
        public bool savePurchaseRequest(string ticket, DateTime fecha, int userId, int cantidad, int matId)
        {
            return objPur.savePurchaseRequest(ticket, fecha, userId, cantidad, matId); // Llamamos al método correcto en la capa de datos
        }

        // Método para actualizar una solicitud de compra
        public bool updatePurchaseRequest(int requestId, string ticket, DateTime fecha, int userId, int cantidad, int matId)
        {
            return objPur.updatePurchaseRequest(requestId, ticket, fecha, userId, cantidad, matId); // Llamamos al método correcto en la capa de datos
        }

        // Método para eliminar una solicitud de compra
        public bool deletePurchaseRequest(int requestId)
        {
            return objPur.deletePurchaseRequest(requestId); // Llamamos al método correcto en la capa de datos
        }

        // Método para contar las solicitudes de compra
        public int countPurchaseRequests()
        {
            return objPur.countPurchaseRequests();
        }

        // Método para mostrar solicitudes de compra de un usuario específico
        public DataSet showPurchaseRequestsByUser(int userId)
        {
            return objPur.showPurchaseRequestsByUser(userId);
        }
    }
}