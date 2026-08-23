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
    private readonly IGenericRepository<Trainer> _trainerRepository;

    public TrainerService(IGenericRepository<Trainer> trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }


    public async Task<IEnumerable<TrainerViewModel>> GetTrainersAsync(CancellationToken cancellationToken)
    {
       var trainers = await _trainerRepository.GetAllAsync(cancellationToken : cancellationToken);
        var result =  trainers.Select(t => new TrainerViewModel
        {
            Id = t.Id,
            Name = t.Name,
            Email = t.Email,
            Phone = t.Phone,
            Specialty = t.Specialty.ToString(),
        }).ToList();       
        return result;
    }
    public async Task<bool> CreateTrainerAsync(TrainerCreateViewModel createTrainerViewModel, CancellationToken cancellationToken)
    {
        var emailExists = await _trainerRepository.AnyAsync(m => m.Email == createTrainerViewModel.Email, cancellationToken);
        var phoneExists = await _trainerRepository.AnyAsync(m => m.Phone == createTrainerViewModel.Phone, cancellationToken);
        if (emailExists || phoneExists)
            return false;

        var trainer = new Trainer()
        {
            Name = createTrainerViewModel.Name,
            Email = createTrainerViewModel.Email,
            Phone = createTrainerViewModel.Phone,
            DateOfBirth = createTrainerViewModel.DateOfBirth,
            Gender = createTrainerViewModel.Gender,
            Address = new Address
            {
                BuildingNumber = createTrainerViewModel.BuildingNumber,
                Street = createTrainerViewModel.Street,
                City = createTrainerViewModel.City
            },
            Specialty = Enum.TryParse<Specialties>(createTrainerViewModel.Specialties, out var specialty) ? specialty : Specialties.GeneralFitness
        };


        return (await _trainerRepository.AddAsync(trainer, cancellationToken: cancellationToken)) > 0;
    }

    public async Task<TrainerDetailsViewModel> GetTrainerDetailsAsync(int id, CancellationToken cancellationToken)
    {
       var trainer = await _trainerRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (trainer == null)
            return null;
        var trainerDetailsViewModel = new TrainerDetailsViewModel
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Email = trainer.Email,
            Phone = trainer.Phone,
            DateOfBirth = trainer.DateOfBirth,
            Specialty = trainer.Specialty.ToString(),
            Address = $"{trainer.Address.BuildingNumber}, {trainer.Address.Street}, {trainer.Address.City}"
        };
         
        return trainerDetailsViewModel;
    }

    public async Task<TrainerEditViewModel?> GetTrainerDetailsForEditAsync(int id,  CancellationToken cancellationToken)
    {
       var trainer = await _trainerRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (trainer == null)
            return null;
        return new TrainerEditViewModel
        {
            Name = trainer.Name,
            Email = trainer.Email,
            Phone = trainer.Phone,
            BuildingNumber = trainer.Address.BuildingNumber,
            Street = trainer.Address.Street,
            City = trainer.Address.City,
            Specialties = trainer.Specialty.ToString()
        };
        
    }

    public async Task<bool> UpdateTrainerAsync(int id, TrainerEditViewModel trainerEditViewModel, CancellationToken cancellationToken)
    {
        var trainer = await _trainerRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (trainer == null)
            return false;

        var emailExists = await _trainerRepository.AnyAsync(m => m.Email == trainerEditViewModel.Email && m.Id != id, cancellationToken);
        var phoneExists = await _trainerRepository.AnyAsync(m => m.Phone == trainerEditViewModel.Phone && m.Id != id, cancellationToken);
        if (emailExists || phoneExists)
            return false;

        trainer.Email = trainerEditViewModel.Email;
        trainer.Phone = trainerEditViewModel.Phone;
        trainer.Address= new Address
        {
            BuildingNumber = trainerEditViewModel.BuildingNumber,
            Street = trainerEditViewModel.Street,
            City = trainerEditViewModel.City
        };
        trainer.Specialty = Enum.TryParse<Specialties>(trainerEditViewModel.Specialties, out var specialty) ? specialty : Specialties.GeneralFitness;

        return (await _trainerRepository.UpdateAsync(trainer, cancellationToken: cancellationToken) > 0);
    }

    public async Task<bool> DeleteTrainerAsync(int id, CancellationToken cancellationToken)
    {
        var trainer = await _trainerRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (trainer == null)
            return false;
        var result = (await _trainerRepository.DeleteAsync(trainer, cancellationToken: cancellationToken)) > 0;
        return result;

    }
}

