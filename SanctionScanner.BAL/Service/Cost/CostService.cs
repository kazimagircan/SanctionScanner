using SanctionScanner.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanctionScanner.BAL.Service.Cost
{
    public class CostService : ICostService
    {
        private UnitOfWork uow;

        public CostService()
        {
            uow = new UnitOfWork();
        }
        public bool AddCost(CostDto data)
        {
            bool result = false;

            try
            {
                DAL.Model.Entity.Cost model = new DAL.Model.Entity.Cost
                {
                    Id = data.Id,
                    CreatedDate = DateTime.UtcNow,
                    Description = data.Description,
                    Date = data.Date,
                    Quantity = data.Quantity,
                    UnitPrice = data.UnitPrice,
                    Total = data.Total,
                    Status = 1,
                    UserId = data.UserId,
                    IsActive = true
                };
                uow.Repository<DAL.Model.Entity.Cost>().Add(model);
                uow.SaveChanges();

                result = true;
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public bool DeleteCost(int userId, int id)
        {
            var user = uow.Repository<DAL.Model.Entity.Cost>().GetBy(x => x.Id == id && x.UserId == userId).FirstOrDefault();

            if (user == null)
                return false;

            uow.Repository<DAL.Model.Entity.Cost>().Delete(id);
            uow.SaveChanges();
            return true;
        }

        public CostDto GetCost(int userId, int id)
        {
            return uow.Repository<DAL.Model.Entity.Cost>().GetBy(x => x.Id == id && x.UserId == userId && x.IsActive == true).Select(x => new CostDto
            {
                Id = x.Id,
                CreatedDate = (DateTime)x.CreatedDate,
                UpdatedDate = (DateTime)x.UpdatedDate,
                Description = x.Description,
                Date = x.Date,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                Total = x.Total,
                Status = x.Status,
                RejectReason = x.RejectReason

            }).FirstOrDefault();
        }

        public List<CostDto> GetCosts(int userId)
        {
            return uow.Repository<DAL.Model.Entity.Cost>()
                .GetBy(x => x.UserId == userId)
                .Select(x => new CostDto()
                {
                    Id = x.Id,
                    CreatedDate = (DateTime)x.CreatedDate,
                    UpdatedDate = (DateTime)x.UpdatedDate,
                    Date = (DateTime)x.Date,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Total = x.Total,
                    Status = x.Status,
                    RejectReason = x.RejectReason,
                    UserId = x.UserId
                }).ToList();
        }

        public List<CostDto> GetRejectedCosts(int userId)
        {
            return uow.Repository<DAL.Model.Entity.Cost>()
                .GetBy(x => x.UserId == userId && x.Status==3)
                .Select(x => new CostDto()
                {
                    Id = x.Id,
                    CreatedDate = (DateTime)x.CreatedDate,
                    UpdatedDate = (DateTime)x.UpdatedDate,
                    Date = (DateTime)x.Date,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Total = x.Total,
                    Status = x.Status,
                    RejectReason = x.RejectReason,
                    UserId=x.UserId
                }).ToList();
        }

        public List<CostDto> GetAwaitingCosts(int managerId)
        {
            return uow.Repository<DAL.Model.Entity.Cost>()
                .GetBy(x => x.User.ManagerId == managerId && x.Status == 1)
                .Select(x => new CostDto()
                {
                    Id = x.Id,
                    CreatedDate = (DateTime)x.CreatedDate,
                    UpdatedDate = (DateTime)x.UpdatedDate,
                    Date = (DateTime)x.Date,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Total = x.Total,
                    Status = x.Status,
                    RejectReason = x.RejectReason,
                    UserId = x.UserId
                }).ToList();
        }

        public List<CostDto> GetManagerCosts(int managerId)
        {
            return uow.Repository<DAL.Model.Entity.Cost>()
                .GetBy(x => x.User.ManagerId == managerId)
                .Select(x => new CostDto()
                {
                    Id = x.Id,
                    CreatedDate = (DateTime)x.CreatedDate,
                    UpdatedDate = (DateTime)x.UpdatedDate,
                    Date = (DateTime)x.Date,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Total = x.Total,
                    Status = x.Status,
                    RejectReason = x.RejectReason,
                    UserId = x.UserId
                }).ToList();
        }

        public bool UpdateCost(CostDto data)
        {
            bool result = false;

            try
            {
                DAL.Model.Entity.Cost cost = uow.Repository<DAL.Model.Entity.Cost>().GetBy(x => x.Id == data.Id && x.UserId==data.UserId).FirstOrDefault();

                if (cost != null)
                {

                    cost.Description = data.Description;
                    cost.UpdatedDate = DateTime.UtcNow;
                    cost.Quantity = data.Quantity;
                    cost.UnitPrice = data.UnitPrice;
                    cost.Total = data.Total;
                    cost.Status = data.Status;
                    cost.RejectReason = data.RejectReason;

                    uow.Repository<DAL.Model.Entity.Cost>().Edit(cost);
                    uow.SaveChanges();

                    result = true;
                }


            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public List<CostDto> GetWaitingPaymentCosts(int accountantId)
        {
            return uow.Repository<DAL.Model.Entity.Cost>()
                .GetBy(x => x.Status == 4)
                .Select(x => new CostDto()
                {
                    Id = x.Id,
                    CreatedDate = (DateTime)x.CreatedDate,
                    UpdatedDate = (DateTime)x.UpdatedDate,
                    Date = (DateTime)x.Date,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Total = x.Total,
                    Status = x.Status,
                    RejectReason = x.RejectReason,
                    UserId = x.UserId
                }).ToList();
        }

        public List<CostDto> GetAccountantCosts(int accountantId)
        {
            return uow.Repository<DAL.Model.Entity.Cost>()
                .GetBy(x => x.Status==5)
                .Select(x => new CostDto()
                {
                    Id = x.Id,
                    CreatedDate = (DateTime)x.CreatedDate,
                    UpdatedDate = (DateTime)x.UpdatedDate,
                    Date = (DateTime)x.Date,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Total = x.Total,
                    Status = x.Status,
                    RejectReason = x.RejectReason,
                    UserId = x.UserId
                }).ToList();
        }
    }
}
