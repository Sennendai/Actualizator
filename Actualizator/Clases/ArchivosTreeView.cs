using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actualizator
{
    public class ArchivosTreeView
    {
        private string dirName;
        private List<string> archivos = new List<string>();
        private List<ArchivosTreeView> subdir = new List<ArchivosTreeView>();

        public List<string> Archivos 
        { 
            get => archivos;
            set 
            { 
                if (archivos != value) 
                { 
                    archivos = value; 
                }                   
            }
        }
        public List<ArchivosTreeView> Subdir 
        { 
            get => subdir;
            set 
            { 
                if (subdir != value)
                {
                    subdir = value;
                }                 
            }
        }
        public string DirName { get => dirName; set => dirName = value; }
    }
}
