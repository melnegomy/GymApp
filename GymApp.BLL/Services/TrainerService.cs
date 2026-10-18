using GymApp.BLL.ViewModels.Trainer;
using GymApp.DAL.Contracts;
using GymApp.DAL.Models.Enums;
using GymApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.BLL.Services;

public class TrainerService : ITrainerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public TrainerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TrainerViewModel>> GetTrainersAsync(CancellationToken cancellationToken)
    {
        var _trainerRepository = _unitOfWork.GetRepository<Trainer>();
        var trainers = await _trainerRepository.GetAllAsync(cancellationToken : cancellationToken);
        return _mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerViewModel>>(trainers);
    }
    public async Task<bool> CreateTrainerAsync(TrainerCreateViewModel createTrainerViewModel, CancellationToken cancellationToken)
    {
        var _trainerRepository = _unitOfWork.GetRepository<Trainer>();
        var emailExists = await _trainerRepository.AnyAsync(m => m.Email == createTrainerViewModel.Email, cancellationToken);
        var phoneExists = await _trainerRepository.AnyAsync(m => m.Phone == createTrainerViewModel.Phone, cancellationToken);
        if (emailExists || phoneExists)
            return false;


        var trainer = _mapper.Map<TrainerCreateViewModel, Trainer>(createTrainerViewModel);
        _trainerRepository.Add(trainer, cancellationToken: cancellationToken);
        return (await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)) > 0;
    }

    public async Task<TrainerDetailsViewModel> GetTrainerDetailsAsync(int id, CancellationToken cancellationToken)
    {
       var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(id, cancellationToken: cancellationToken);
        if (trainer == null)
            return null;
        var trainerDetailsViewModel = _mapper.Map<Trainer, TrainerDetailsViewModel>(trainer);
        return trainerDetailsViewModel;
    }

    public async Task<TrainerEditViewModel?> GetTrainerDetailsForEditAsync(int id,  CancellationToken cancellationToken)
    {
       var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(id, cancellationToken: cancellationToken);
        if (trainer == null)
            return null;

        return _mapper.Map<Trainer, TrainerEditViewModel>(trainer);
    }

    public async Task<bool> UpdateTrainerAsync(int id, TrainerEditViewModel trainerEditViewModel, CancellationToken cancellationToken)
    {
        var _trainerRepository = _unitOfWork.GetRepository<Trainer>();
        var trainer = await _trainerRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (trainer == null)
            return false;

        var emailExists = await _trainerRepository.AnyAsync(m => m.Email == trainerEditViewModel.Email && m.Id != id, cancellationToken);
        var phoneExists = await _trainerRepository.AnyAsync(m => m.Phone == trainerEditViewModel.Phone && m.Id != id, cancellationToken);
        if (emailExists || phoneExists)
            return false;

        _mapper.Map(trainerEditViewModel, trainer);

        _trainerRepository.Update(trainer, cancellationToken: cancellationToken);
        return (await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)) > 0;
    }

    public async Task<bool> DeleteTrainerAsync(int id, CancellationToken cancellationToken)
    {
        var _trainerRepository = _unitOfWork.GetRepository<Trainer>();
        var trainer = await _trainerRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (trainer == null)
            return false;
        _trainerRepository.Delete(trainer, cancellationToken: cancellationToken);
        return (await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken)) > 0;

    }
}

