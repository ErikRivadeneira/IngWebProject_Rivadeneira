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
    
    public partial class Option
    {
        public int OptionsID { get; set; }
        public int QuestionsID { get; set; }
        public string OptionText { get; set; }
        public bool isCorrect { get; set; }
    
        public virtual Question Question { get; set; }
    }
}
