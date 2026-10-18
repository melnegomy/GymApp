using GymApp.BLL.Common;
using GymApp.BLL.ViewModels.Session;
using GymApp.DAL.Contracts;
using GymApp.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.BLL.Services;

public class SessionService : ISessionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken cancellationToken = default)
    {
        var sessions = await _unitOfWork.Session
            .GetAllSessionsWithCategoryAndTrainerAsync(cancellationToken: cancellationToken);
        var result = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);
        foreach (var item in result)
        {
                item.AvailableSlots =item.Capacity - await _unitOfWork.Session.CountOfBookedSlotsAsync(item.Id, cancellationToken);
        }
        return result;
    }
    public  async Task<Result> CreateSessionAsync(CreateSessionViewModel model, CancellationToken cancellationToken = default)
    {
        if (model.EndDate < model.StartDate)
            return Result.ValidatonError("End date must be greater than start date");
        if (model.StartDate <= DateTime.Now)
            return Result.ValidatonError("Start date must be in the future");

        var trainer = await _unitOfWork.GetRepository<Trainer>()
            .GetByIdAsync(model.TrainerId, cancellationToken: cancellationToken);
        if (trainer == null) return Result.NotFound($"Trainer {model.TrainerId} not found");

        //var isTrainerAvailable = await _unitOfWork.GetRepository<Trainer>()
        //    .AnyAsync(t=>t.Sessions.Any(s=>s.StartDate < model.EndDate && s.EndDate > model.StartDate));//SQL    
        //if (!isTrainerAvailable)
        //    return Result.ValidatonError($"Trainer {model.TrainerId} is not available for the selected time period");

        var category = await _unitOfWork.GetRepository<Category>()
            .GetByIdAsync(model.CategoryId, cancellationToken: cancellationToken);
        if (category == null) return Result.NotFound($"Category {model.CategoryId} not found");

        var isValidSpecialty = Enum.TryParse<Specialties>(category.Name, true, out _);
        if(!isValidSpecialty)
            return Result.ValidatonError("Invalid specialty for the selected category");

        var session = _mapper.Map<CreateSessionViewModel, Session>(model);
        _unitOfWork.Session.Add(session);
        var result = (await _unitOfWork.SaveChangesAsync(cancellationToken))>0;
        return result ? Result.Ok() : Result.ValidatonError("Failed to create session");
    }

    public async Task<IEnumerable<CategorySelectItemViewModel>> GetCategorySelectItemAsync(CancellationToken cancellationToken = default)
    {
        var categories =await  _unitOfWork.GetRepository<Category>().GetAllAsync(cancellationToken: cancellationToken);
        return _mapper.Map<IEnumerable<CategorySelectItemViewModel>>(categories);
    }

    public async Task<Result<SessionViewModel>> GetSessionByIdAsync(int sessionId, CancellationToken cancellationToken = default)
    {
       // Get Session
       var session = await _unitOfWork.Session.GetWithCategoryAndTrainerByIdAsync(sessionId, cancellationToken);
       if (session is null)
           return (Result<SessionViewModel>)Result<SessionViewModel>.NotFound($"Session with ID {sessionId} not found");

       // Map 
       var sessionViewModel = _mapper.Map<SessionViewModel>(session);

       // Return  Result 
       return Result<SessionViewModel>.Ok(sessionViewModel);
    }

    public async Task<Result<UpdateSessionViewModel>> GetSessionToUpdateAsync(int sessionId, CancellationToken cancellationToken = default)
    {
        var session = await _unitOfWork.Session.GetByIdAsync(sessionId, cancellationToken: cancellationToken);
        var updateSessionViewModel = _mapper.Map<UpdateSessionViewModel>(session);
        return Result<UpdateSessionViewModel>.Ok(updateSessionViewModel);
    }

    public  async Task<Result> UpdateSessionAsync(int sessionId, UpdateSessionViewModel model, CancellationToken cancellationToken = default)
    {
        var session = await _unitOfWork.Session.GetWithCategoryAndTrainerByIdAsync(sessionId, cancellationToken);

        if (session is null)
            return Result.NotFound($"Session with ID {sessionId} not found");


        if (session.StartDate <= DateTime.Now)
            return Result.ValidatonError("Cannot update a session that has already started");


        if (model.EndDate < model.StartDate)
            return Result.ValidatonError("End date must be greater than start date");
        if (model.StartDate <= DateTime.Now)
            return Result.ValidatonError("Start date must be in the future");

        var trainer = await _unitOfWork.GetRepository<Trainer>()
           .GetByIdAsync(model.TrainerId, cancellationToken: cancellationToken);
        if (trainer == null) return Result.NotFound($"Trainer {model.TrainerId} not found");

        var isValidSpecialty = Enum.TryParse<Specialties>(session.Category.Name, true, out _);
        if (!isValidSpecialty)
            return Result.ValidatonError("Invalid specialty for the selected category");

        //var isTrainerAvailable = await _unitOfWork.GetRepository<Trainer>()
        //    .AnyAsync(t => t.Sessions
        //    .Any(s => s.StartDate < model.EndDate && s.EndDate > model.StartDate && s.Id != sessionId), cancellationToken);//SQL    
        //if (!isTrainerAvailable)
        //    return Result.ValidatonError($"Trainer {model.TrainerId} is not available for the selected time period");


        var UpdatedSession = _mapper.Map(model, session);
        _unitOfWork.Session.Update(UpdatedSession);
        var isUpdated = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > 0;
        return isUpdated ? Result.Ok() : Result.ValidatonError("Failed to update session");

    }

    public  async Task<Result> DeleteSessionAsync(int id, CancellationToken cancellationToken)
    {
        var session = await _unitOfWork.Session.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (session is null)
            return Result.NotFound($"Session with ID {id} not found");

        if (session.StartDate <= DateTime.Now&& session.EndDate >= DateTime.Now )
            return Result.ValidatonError("Cannot delete a session that has already started");
        _unitOfWork.Session.Delete(session);
        var isDeleted = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > 0;
        return isDeleted ? Result.Ok() : Result.ValidatonError("Failed to delete session");
    }
}



