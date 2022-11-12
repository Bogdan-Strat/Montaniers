using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.BusinessLogic.Implementation.Warnings.Models;
using Montaniarzii.BusinessLogic.Implementation.Warnings.Validation;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.Common.Extensions;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation
{
    public class WarningService : BaseService
    {
        private readonly CreateWarningModelValidation CreateWarningModelValidation;
        public WarningService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            CreateWarningModelValidation = new CreateWarningModelValidation();
        }

        public async Task CreateWarning(CreateWarningModel model)
        {
            CreateWarningModelValidation.Validate(model).ThenThrow(model);

            var warning = Mapper.Map<CreateWarningModel, Warning>(model);

            warning.WarningId = Guid.NewGuid();
            warning.CreatedByUserId = CurrentUser.Id;
            warning.CreateDate = DateTime.Now;

            UnitOfWork.Warnings.Insert(warning);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<GetWarningModelAsUser>> GetActiveWarningsAsUser()
        {
            return await UnitOfWork.Warnings
                .Get()
                .Where(w => w.EndDate > DateTime.Now)
                .Select(w => new GetWarningModelAsUser()
                {
                    WarningMessage = w.WarningMessage,
                    CreateDate = w.CreateDate,
                    EndDate = w.EndDate
                })
                .ToListAsync();
        }

        public async Task<ListOfWarningAsAdminModel> GetWarningsAsAdmin(int count = 0)
        {
            if(count > 0)
                count--;
            var model = new ListOfWarningAsAdminModel()
            {
                NumberOfWarnings = await UnitOfWork.Warnings
                .Get()
                .CountAsync(),
                ActualPageNumber = count + 1
            };

            model.ListOfWarnings = await UnitOfWork.Warnings
                .Get()
                .OrderByDescending(w => w.EndDate)
                .Skip(count * 10)
                .Take(10)
                .Select(w => new GetWarningModelAsAdmin()
                {
                    WarningId = w.WarningId,
                    WarningMessage = w.WarningMessage,
                    CreateDate = w.CreateDate,
                    EndDate = w.EndDate,
                    UsernameAdmin = w.User.Username,
                    Status = w.EndDate > DateTime.Now
                })
                .ToListAsync();

            return model;
        }

        public async Task DeleteWarning(Guid warningId)
        {
            var warning = await UnitOfWork.Warnings
                .Get()
                .SingleOrDefaultAsync(w => w.WarningId == warningId);

            if(warning == null)
            {
                throw new NotFoundErrorException();
            }

            UnitOfWork.Warnings.HardDelete(warning);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<UpdateWarningModel> GetUpdateWarning(Guid warningId)
        {
            var warning = await GetWarningById(warningId);

            if(warning == null)
            {
                throw new NotFoundErrorException();
            }

            return new UpdateWarningModel()
            {
                WarningId = warningId,
                WarningMessage = warning.WarningMessage,
                EndDate = warning.EndDate
            };
        }
        public async Task UpdateWaning(UpdateWarningModel model)
        {
            var warning = await GetWarningById(model.WarningId);

            if (warning == null)
            {
                throw new NotFoundErrorException();
            }

            warning.WarningMessage = model.WarningMessage;
            warning.EndDate = model.EndDate;
            warning.CreatedByUserId = CurrentUser.Id;

            UnitOfWork.Warnings.Update(warning);
            await UnitOfWork.SaveChangesAsync();
        }

        private async Task<Warning> GetWarningById(Guid warningId)
        {
            var warning = await UnitOfWork.Warnings
                .Get()
                .SingleOrDefaultAsync(w => w.WarningId == warningId);

            if(warning == null)
            {
                throw new NotFoundErrorException();
            }

            return warning;
        }
    }
}
