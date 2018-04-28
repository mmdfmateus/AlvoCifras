﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlvoCifras.Models
{
    public class BaseModel
    {
        [Required]
        public int Id { get; set; }

        //TODO: Inserir lógica de criar e pegar o DateTime.Now

        [DisplayName("Criado em")]
        [Required]
        public DateTime CreatedAt { get; set; }

    }
}
