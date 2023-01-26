using StavkiWebApi.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace StavkiWebApi.Models.Entites
{
    public class Request : RequestDomain
    {
        public Client Client { get; set; }
    }
}
