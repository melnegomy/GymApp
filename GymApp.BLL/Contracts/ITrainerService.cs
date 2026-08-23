using GymApp.BLL.ViewModels.Trainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.BLL.Contracts;

public interface ITrainerService
{
    Task<bool> CreateTrainerAsync(TrainerCreateViewModel trainerViewModel, CancellationToken cancellationToken);
    Task<bool> DeleteTrainerAsync(int id, CancellationToken cancellationToken);
    Task<TrainerDetailsViewModel> GetTrainerDetailsAsync(int id, CancellationToken cancellationToken);
    Task<TrainerEditViewModel?> GetTrainerDetailsForEditAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TrainerViewModel>> GetTrainersAsync(CancellationToken cancellationToken);
    Task<bool> UpdateTrainerAsync(int id, TrainerEditViewModel trainerEditViewModel, CancellationToken cancellationToken);
}
