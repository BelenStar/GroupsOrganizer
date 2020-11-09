using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace groupsorganizer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variables
            int studentsQuantity = 0;
            int topicsQuantity = 0;
            List<string> studentsList = new List<string>();
            List<string> topicsList = new List<string>();


            //Read files and Add data to lists

            Console.Write("Ingresar la ruta o el nombre del archivo de estudiantes: ");
            var studentsDir = Console.ReadLine();
            using (StreamReader studentsReader = new StreamReader(studentsDir)){
                string line;
                while ((line = studentsReader.ReadLine()) != null)
                {
                    studentsList.Add(line);
                    
                }
            }
            Console.Write("Ingresar la ruta o el nombre del archivo de temas: ");
            var topicsDir = Console.ReadLine();
            using (StreamReader tReader = new StreamReader(topicsDir)){
                string line;
                while ((line = tReader.ReadLine()) != null)
                {
                    topicsList.Add(line);
                    
                }
            }

            studentsQuantity = studentsList.Count();
            topicsQuantity = topicsList.Count();

            Console.Write("Ingresar la cantidad de estudiantes por grupo: ");
            int studentsPerGroup = int.Parse(Console.ReadLine());

            int leftStudents = studentsQuantity % studentsPerGroup;
            
            studentsQuantity = studentsQuantity - leftStudents;

            bool goodStudents = studentsQuantity >= studentsPerGroup;
            if(!goodStudents){
                Console.Clear();
                Console.WriteLine("Lo sentimos, no hay suficientes estudiantes para crear grupos, da 'Enter' y ejecuta el programa nuevamente.");
                Console.ReadLine();
                return;

            }
            
            int groupsQuantity = studentsQuantity / studentsPerGroup;
            bool goodTopics = topicsQuantity >=  groupsQuantity;
            if(!goodTopics){
                Console.Clear();
                Console.WriteLine("Lo sentimos, no hay suficientes temas para crear grupos, da 'Enter' y ejecuta el programa nuevamente.");
                Console.ReadLine();
                return;

            }
            
            int topicsLeft = topicsQuantity% groupsQuantity;
            topicsQuantity = topicsQuantity - topicsLeft;
            int topicsPerGroup = topicsQuantity/groupsQuantity;

            List<Group> groupList = new List<Group>();
            Random random = new Random();

            for (int i = 0; i < groupsQuantity; i++)
            {
                List<string> groupMembers = new List<string>();
                List<string> groupTopics = new List<string>();

                for (int j = 0; j < studentsPerGroup; j++)
                {
                    int index_student = random.Next(studentsList.Count());
                    groupMembers.Add(studentsList.ElementAt(index_student));
                    studentsList.RemoveAt(index_student);
                }

                 for (int k = 0; k < topicsPerGroup; k++)
                {
                    int index_topic = random.Next(topicsList.Count());
                    groupTopics.Add(topicsList.ElementAt(index_topic));
                    topicsList.RemoveAt(index_topic);
                }

                groupList.Add(new Group(groupMembers,groupTopics, i+1));

            }

            while(leftStudents > 0){
                List<Group> minValueStudentGroup = new List<Group>();
                int st_min = groupList.First().Students.Count();

                foreach (Group group in groupList)
                {
                    int students_quantity = group.Students.Count();
                    if (students_quantity < st_min)
                    {
                        st_min = students_quantity;
                    }

                    if (students_quantity == st_min)
                    {
                        minValueStudentGroup.Add(group);

                    } 
                }

                for (int i = 0; i < leftStudents; i++)
                {
                    int index_stud_group = random.Next(minValueStudentGroup.Count());
                    Group student_group = groupList.ElementAt(index_stud_group);
                    
                    for (int j = 0; j < leftStudents; j++)
                    {
                        int index_student = random.Next(studentsList.Count());
                        student_group.Students.Add(studentsList.ElementAt(index_student));
                        studentsList.RemoveAt(index_student);
                        leftStudents--;
                    }
                }
            }

             while(topicsLeft > 0){
                List<Group> minValueTopicsGroup = new List<Group>();
                int top_min = groupList.First().Topics.Count();

                foreach (Group group in groupList)
                {
                    int topics_quantity = group.Topics.Count();
                    if (topics_quantity < top_min)
                    {
                        top_min = topics_quantity;
                    }

                    if (topics_quantity == top_min)
                    {
                        minValueTopicsGroup.Add(group);

                    } 
                }

                for (int i = 0; i < topicsLeft; i++)
                {
                    int index_topic_group = random.Next(minValueTopicsGroup.Count());
                    Group topic_group = groupList.ElementAt(index_topic_group);
                    
                    for (int j = 0; j < topicsLeft; j++)
                    {
                        int index_topic = random.Next(topicsList.Count());
                        topic_group.Topics.Add(topicsList.ElementAt(index_topic));
                        topicsList.RemoveAt(index_topic);
                        topicsLeft--;
                    }
                }
            }

            foreach (Group group in groupList)
            {
                group.Print();
            }

            Console.ReadLine();
        }
    }
}
