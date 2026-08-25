using GymApp.BLL.ViewModels.Session;
using GymApp.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.BLL.Services;

public class SessionService : ISessionService
{
    private readonly IUnitOfWork _unitOfWork;

    public SessionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken cancellationToken = default)
    {
        var sessions = await _unitOfWork.Session
            .GetAllSessionsWithCategoryAndTrainerAsync(cancellationToken: cancellationToken);
        var result = sessions.Select(s => new SessionViewModel
        {
            Id = s.Id,
            Capacity = s.Capacity,
            TrainerName = s.Trainer.Name,
            CategoryName = s.Category.Name,
            Description = s.Description,
            StartDate = s.StartDate,
            EndDate = s.EndDate,
            
        });
        foreach (var item in result)
        {
                item.AvailableSlots =item.Capacity - await _unitOfWork.Session.CountOfBookedSlotsAsync(item.Id, cancellationToken);
        }
        return result;
    }
}
