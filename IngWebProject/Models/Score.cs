//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IngWebProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Score
    {
        public int ScoresID { get; set; }
        public int UsersID { get; set; }
        public int Combo { get; set; }
        public double Points { get; set; }
        public string MODE { get; set; }
    
        public virtual User User { get; set; }
    }
}
