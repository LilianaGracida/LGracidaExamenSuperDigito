﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class Historial
{
    public int IdHistorial { get; set; }

    public int? IdUsuario { get; set; }

    public int Numero { get; set; }

    public int Resultado { get; set; }

    public DateTime? FechaHora { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
