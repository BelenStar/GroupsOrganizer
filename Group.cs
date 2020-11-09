using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace groupsorganizer
{
    class Group
    {
        private List<string> students;
        private List<string> topics;
        private int number;

        public Group(List<string> students, List<string> topics, int number){
            this.students = students;
            this.topics = topics;
            this.number = number;
        }

        public List<string> Students{
            get{return this.students;}
            set{this.students = value;}
        }

         public List<string> Topics{
            get{return this.topics;}
            set{this.topics = value;}
        }

         public int Number{
            get{return this.number;}
            set{this.number = value;}
        }

        public void Print(){
            Console.WriteLine("Grupo #{0}", Number);
            Console.Write("Tema(s): ");
            foreach (string top in Topics)
            {
                Console.Write(top + " ");
            }

            Console.WriteLine();
            Console.WriteLine("Estudiantes: ");

            int count = 1;
            foreach (string stud in Students)
            {
                Console.WriteLine("[{0}] - {1}", count, stud);
                count ++;
                
            }
        }
    }
}