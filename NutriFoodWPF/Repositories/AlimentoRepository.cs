using NutriFoodWPF.Data;
using NutriFoodWPF.Models;
using NutriFoodWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFoodWPF.Repositories
{
    public class AlimentoRepository
    {
        private readonly AlimentoService _service;

        public AlimentoRepository()
        {
            _service = new AlimentoService();
        }
    }
}
