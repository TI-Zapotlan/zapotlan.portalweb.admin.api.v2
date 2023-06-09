﻿using Microsoft.Extensions.Options;
using Zapotlan.PortalWeb.Admin.Core.CustomEntities;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;
using Zapotlan.PortalWeb.Admin.Core.Exceptions;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Core.QueryFilters;

namespace Zapotlan.PortalWeb.Admin.Core.Services
{
    public class AdministracionService : IAdministracionService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly PaginationOptions _paginationOptions;

        // CONSTRUCTOR

        public AdministracionService(IUnitOfWork unitOfWork) //, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            //_paginationOptions = options.Value;
        }

        // METHODS

        public PagedList<Administracion> Gets(AdministracionQueryFilter filters)
        {
            var items = _unitOfWork.AdministracionRepository.Gets();

            // Filters

            if (!string.IsNullOrEmpty(filters.Periodo)) 
            { 
                items = items.Where(i => i.Periodo != null && i.Periodo.ToLower().Contains(filters.Periodo.ToLower()));
            }

            if (filters.FechaInicio != null) 
            {   
                items = items.Where(i => i.FechaInicio >= filters.FechaInicio);
            }

            if (filters.FechaTermino != null) 
            {
                items = items.Where(i => i.FechaTermino <= filters.FechaTermino);
            }

            if (filters.Estatus != null && filters.Estatus != EstatusType.Ninguno)
            {
                items = items.Where(i => i.Estatus == filters.Estatus);
            }
            else 
            {
                filters.IncludeEliminados ??= false;
                items = (bool)filters.IncludeEliminados
                    ? items.Where(i => i.Estatus != EstatusType.Ninguno)
                    : items.Where(i => i.Estatus != EstatusType.Ninguno && i.Estatus != EstatusType.Eliminado);
            }

            // Order

            switch (filters.Orden)
            {
                case AdministracionOrderFilterType.Periodo:
                    items = items.OrderBy(i => i.Periodo); 
                    break;
                case AdministracionOrderFilterType.FechaInicio:
                    items = items.OrderBy(i => i.FechaInicio); 
                    break;
                case AdministracionOrderFilterType.PeriodoDesc:
                    items = items.OrderByDescending(i => i.Periodo);
                    break;
                case AdministracionOrderFilterType.FechaInicioDesc:
                    items = items.OrderByDescending(i => i.FechaInicio);
                    break;
                default:
                    items = items.OrderBy(i => i.Periodo);
                    break;
            }

            // Misc
            var pagedItems = PagedList<Administracion>.Create(items, filters.PageNumber, filters.PageSize);
            return pagedItems;
        }

        public async Task<Administracion?> GetAsync(Guid id)
        {
            return await _unitOfWork.AdministracionRepository.GetAsync(id);
        }

        public async Task<Administracion> AddAsync(Administracion item)
        {
            if (string.IsNullOrEmpty(item.UsuarioActualizacion)) {
                throw new BusinessException("Faltó especificar el usuario que ejecuta la aplicación.");
            }

            // TODO: Validar permiso del usuario y si el usuario existe

            item.ID = Guid.NewGuid();
            item.Estatus = EstatusType.Ninguno;
            item.FechaActualizacion = DateTime.Now;

            await _unitOfWork.AdministracionRepository.DeleteTmpByUser(item.UsuarioActualizacion);
            await _unitOfWork.AdministracionRepository.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return item;
        }

        public async Task<Administracion> UpdateAsync(Administracion item)
        {
            var adminFound = await _unitOfWork.AdministracionRepository
                .GetByDateRangeWithException(item.FechaInicio, item.FechaTermino, item.ID);
            if (adminFound.Any()) {
                throw new BusinessException("Ya existe un registro dentro del rango de fechas establecido");
            }

            // Validar si esta cambiando el registro como activo
            if (item.Estatus == EstatusType.Activo) 
            {
                await _unitOfWork.AdministracionRepository.DisableActiveAdmin(item.ID);
            }

            if (string.IsNullOrEmpty(item.UsuarioActualizacion))
            {
                throw new BusinessException("Faltó especificar el usuario que ejecuta la aplicación.");
            }

            // TODO: Validar permiso del usuario

            if (item.Estatus == EstatusType.Ninguno) item.Estatus = EstatusType.Activo;
            item.FechaActualizacion = DateTime.Now;

            await _unitOfWork.AdministracionRepository.UpdateAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteAsync(Administracion item)
        {
            var currItem = await _unitOfWork.AdministracionRepository.GetAsync(item.ID) ?? throw new BusinessException("No se encontró el registro.");

            if (currItem.Estatus == EstatusType.Eliminado)
            {
                //var ayuntamientoFound = await _unitOfWork.AyuntamientoIntegranteRepository.GetByAdministracionID(id);
                //if (ayuntamientoFound.Any())
                //{
                //    throw new BusinessException("No se puede eliminar la administración pues hay registros de integrantes del ayuntamiento asociados.");
                //}
                // Verificar validaciones a datos enlazados
                await _unitOfWork.AdministracionRepository.DeleteAsync(currItem.ID);
            }
            else
            {
                currItem.Estatus = currItem.Estatus == EstatusType.Activo ? EstatusType.Baja : EstatusType.Eliminado;
                currItem.UsuarioActualizacion = item.UsuarioActualizacion;
                currItem.FechaActualizacion = DateTime.Now;
                await _unitOfWork.AdministracionRepository.UpdateAsync(currItem);
            }

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
