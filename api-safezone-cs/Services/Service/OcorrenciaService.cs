using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Ocorrencia;
using api_safezone_cs.Mapper;
using api_safezone_cs.Repositories.Interfaces;
using api_safezone_cs.Services.Interfaces;
using MassTransit;

namespace api_safezone_cs.Services.Service;

public class OcorrenciaService(IOcorrenciaRepository ocorrenciaRepository, IBus bus) : IOcorrenciaService
{
    public async Task<List<Ocorrencia>> GetAllOcorrenciasAsync()
    {
        return await ocorrenciaRepository.GetAllOcorrenciasAsync();
    }

    public async Task<Ocorrencia?> GetOcorrenciaByIdAsync(int id)
    {
        return await ocorrenciaRepository.GetOcorrenciaByIdAsync(id);
    }

    public async Task<Ocorrencia> CreateOcorrenciaAsync(OcorrenciaRequest ocorrenciaRequest)
    {
        var ocorrencia = ocorrenciaRequest.ToOcorrenciaFromRequest();
        await ocorrenciaRepository.CreateOcorrenciaAsync(ocorrencia);
        await bus.Publish(new OcorrenciaResponse(
            Id: ocorrencia.Id,
            Localizacao: ocorrencia.Localizacao,
            Tipo: ocorrencia.Tipo,
            Status: ocorrencia.Status,
            Prioridade: ocorrencia.Prioridade,
            DataHora: ocorrencia.DataHora,
            Vitimas: ocorrencia.Vitimas.Select(vitima => vitima.ToVitimaResponse()).ToList())
        );
        return ocorrencia;
    }

    public async Task<Ocorrencia?> UpdateOcorrenciaByAsync(int id, OcorrenciaRequest ocorrenciaRequest)
    {
        return await ocorrenciaRepository.UpdateOcorrenciaByAsync(id, ocorrenciaRequest);
    }

    public async Task<bool> DeleteOcorrenciaByAsync(int id)
    {
        var ocorrencia = await ocorrenciaRepository.DeleteOcorrenciaByAsync(id);
        return ocorrencia != null;
    }
}