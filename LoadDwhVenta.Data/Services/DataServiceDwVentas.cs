

using LoadDwhVenta.Data.Contexts.DwVentas;
using LoadDwhVenta.Data.Contexts.Nortwind;
using LoadDwhVenta.Data.Core;
using LoadDwhVenta.Data.Entities.DwVentas;
using LoadDWVentas.Data.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace LoadDWVentas.Data.Services
{
    public class DataServiceDwVentas : IDataServiceDwVentas
    {
        private readonly NorthwindContext _northwindContext;
        private readonly DwhVentasContext _dwhVentasContext;

        public DataServiceDwVentas(NorthwindContext northwindContext,
                                   DwhVentasContext dwhVentasContext)
        {
            _northwindContext = northwindContext;
            _dwhVentasContext = dwhVentasContext;
        }

        public async Task<OperactionResult> LoadDHW()
        {
            OperactionResult result = new OperactionResult();
            try
            {
                await LoadCategory();
                await LoadCUstomer();
                await LoadEmployee();
                await LoadProduct();
                await LoadShipper();
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = $"Error cargando el DWH Ventas..... {ex.Message}";
            }

            return result;
        }

        private async Task<OperactionResult> LoadCategory()
        {
            OperactionResult operation = new OperactionResult();

            try
            {
                var categories = await _northwindContext.Categories.Select(cat => new DimCategory()
                {
                    CategoryName = cat.CategoryName,
                    CategoryId = cat.CategoryId
                }).AsNoTracking()
                .ToListAsync();

                await _dwhVentasContext.DimCategories.AddRangeAsync(categories);
                await _dwhVentasContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                operation.Success = false;
                operation.Message = $"Hubo un error cargando dimension de categoria...";
            }
            return operation;
        }

        private async Task<OperactionResult> LoadCUstomer()
        {
            OperactionResult operation = new OperactionResult();
            try
            {
                //Obtener clientes de northwind
                var customers = await _northwindContext.Customers.Select(cust => new DimCustomer()
                {
                    CustomerName = cust.ContactName,
                    CustomerId = cust.CustomerId
                }).AsNoTracking()
                    .ToListAsync();

                // Carga  dimension de cliente

                await _dwhVentasContext.AddRangeAsync(customers);
                await _dwhVentasContext.SaveChangesAsync();


            }
            catch (Exception)
            {
                operation.Success = false;
                operation.Message = $"Error cargando dimension de customer....";
            }

            return operation;
        }

        private async Task<OperactionResult> LoadEmployee()
        {
            OperactionResult operation = new OperactionResult();
            try
            {
                // Obtener los empleados de la base de datos de norwind.
                var employees = await _northwindContext.Employees.Select(emp => new DimEmployee()
                {
                    EmployeeId = emp.EmployeeId,
                    EmployeeName = string.Concat(emp.FirstName," ",emp.LastName)
                }).AsNoTracking()
                  .ToListAsync();

                // Carga la dimension de empleado
                await _dwhVentasContext.AddRangeAsync(employees);
                await _dwhVentasContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                operation.Success = false;
                operation.Message = $"Error cargando dimension de employees....";
            }
            return operation;
        }

        private async Task<OperactionResult> LoadProduct()
        {
            OperactionResult operation = new OperactionResult();
            try
            {
                //Obtener Productos de northwind
                var products = await _northwindContext.Products.Select(prod => new DimProduct()
                {
                    ProductName = prod.ProductName,
                    ProductId = prod.ProductId
                }).AsNoTracking()
                  .ToListAsync();

                //carga dimension de producto
                await _dwhVentasContext.AddRangeAsync(products);
                await _dwhVentasContext.SaveChangesAsync();

            }
            catch (Exception)
            {
                operation.Success = false;
                operation.Message = $"Error cargando dimension de products...";
            }
            return operation;
        }

        private async Task<OperactionResult> LoadShipper()
        {
            OperactionResult operation = new OperactionResult();
            try
            {
                //Obtener los shipper de northwind
                var shippers = await _northwindContext.Shippers.Select(ship => new DimShipper()
                {
                    ShipperId = ship.ShipperId,
                    ShipperName = ship.CompanyName
                }).ToListAsync();

                //carga de dimension shipper
                await _dwhVentasContext.AddRangeAsync(shippers);
                await _dwhVentasContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                operation.Success = false;
                operation.Message = $"Error cargando dimension de shipper.....";
            }
            return operation;
        }
    }
}