using System.Collections;
using ClientService.Domain.Entities;

namespace ClientService.Infrastructure.Data;

public static class MajorSeeding
{
    public static IList<Major> Majors = new List<Major>()
    {
        new Major()
        {
            Name = "Công nghệ thông tin",
            Code = "cong-nghe-thong-tin"
        },
        new Major()
        {
            Name = "Logistic",
            Code = "logistic"
        },
        new Major()
        {
            Name = "Kinh doanh",
            Code = "kinh-doanh"
        }
    };
}