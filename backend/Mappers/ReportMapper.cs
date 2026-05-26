namespace BlogApp.Mappers;

using BlogApp.DTOs;
using BlogApp.Entities;

public class ReportMapper
{
    /// <summary>
    ///     Map Report Entity to Report DTO
    /// </summary>
    /// <param name="report">Report Entity Object</param>
    /// <returns>Report DTO Object</returns>
    public static ReportDTO ToReportDTO(Report report)
    {
        return new ReportDTO
        {
            Id = report.Id,
            TargetType = report.TargetType,
            TargetId = report.TargetId,
            Content = report.Content,
            ReporterId = report.ReporterId,
            CreatedAt = report.CreatedAt
        };
    }

    /// <summary>
    ///     Map Report DTO to Report Entity
    /// </summary>
    /// <param name="reportDTO"></param>
    /// <returns></returns>
    public static Report ToReport(ReportDTO reportDTO)
    {
        return new Report
        {
            Id = reportDTO.Id,
            TargetType = reportDTO.TargetType,
            TargetId = reportDTO.TargetId,
            Content = reportDTO.Content,
            ReporterId = reportDTO.ReporterId
        };
    }

    /// <summary>
    ///    Map List of Report Entity to List of Report DTO
    /// </summary>
    /// <param name="reports"></param>
    /// <returns></returns>
    public static List<ReportDTO> ToReportDTOList(List<Report> reports)
    {
        return reports.Select(r => ToReportDTO(r)).ToList();
    }
}