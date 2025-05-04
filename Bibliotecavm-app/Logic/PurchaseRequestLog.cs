using Data;
using System;
using System.Data;

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
        public bool savePurchaseRequest(string ticket, DateTime fecha, int userId, int cantidad, int matId, out string errorMessage)
        {
            return objPur.savePurchaseRequest(ticket, fecha, userId, cantidad, matId, out errorMessage);
        }

        // Método para actualizar una solicitud de compra
        public bool updatePurchaseRequest(int requestId, string ticket, DateTime fecha, int userId, int cantidad, int matId)
        {
            return objPur.updatePurchaseRequest(requestId, ticket, fecha, userId, cantidad, matId);
        }

        // Método para eliminar una solicitud de compra
        public bool deletePurchaseRequest(int requestId)
        {
            return objPur.deletePurchaseRequest(requestId);
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


        // Método para obtener la lista de materiales educativos con su precio
        public DataSet showGetMaterials()
        {
            return objPur.showGetMaterials();
        }
        public DataSet ListarMaterialesEducativos()
        {
            return objPur.ListarMaterialesEducativos();
        }

        // Método para alternar el estado de completado
        public bool ToggleCompletada(int solicId)
        {
            try
            {
                return objPur.ToggleCompletada(solicId);
            }
            catch (Exception ex)
            {
                // Puedes registrar el error o manejarlo según tus necesidades
                Console.WriteLine($"Error en PurchaseRequestLog.ToggleCompletada: {ex.Message}");
                return false;
            }
        }

        // Método para obtener un material por su ID
        public DataTable GetMaterialById(int matId)
        {
            return objPur.GetMaterialById(matId);
        }

    }
}
