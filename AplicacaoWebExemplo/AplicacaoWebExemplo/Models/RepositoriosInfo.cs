using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacaoWebExemplo.Models
{
    public class RepositoriosInfo
    {
        public long ID{ get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public String UltimaData { get; set; }
        public String CriadorReposit { get; set; } 
        public String Linguagem { get; set; } 
        public String[] Contributors { get; set; }

        public string toString()
        {
            string msg = Nome + "   -------      " + Descricao;
            return msg;
        }
    }
}
