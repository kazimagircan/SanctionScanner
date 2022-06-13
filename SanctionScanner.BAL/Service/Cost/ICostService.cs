using System;
using System.Collections.Generic;
using System.Text;

namespace SanctionScanner.BAL.Service.Cost
{
    public interface ICostService
    {
        bool AddCost(CostDto data);
        bool UpdateCost(CostDto data);
        bool DeleteCost(int userId, int id);
        CostDto GetCost(int userId, int id);
        List<CostDto> GetCosts(int userId);
        List<CostDto> GetRejectedCosts(int userId);
        List<CostDto> GetAwaitingCosts(int managerId);
        List<CostDto> GetManagerCosts(int managerId);
        List<CostDto> GetWaitingPaymentCosts(int accountantId);
        List<CostDto> GetAccountantCosts(int accountantId);
    }
}
