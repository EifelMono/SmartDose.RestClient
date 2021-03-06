﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmartDose.RestDummy.Generators.V0
{
    public class Department
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class Departments
    {
        public List<Department> Items { get; set; }
    }

    public static class DepartmentGenerator
    {
        private static Departments s_departments = null;
        private static Random s_random = new Random();
        public static Departments Departments
        {
            get => s_departments ?? (s_departments = JsonConvert.DeserializeObject<Departments>(RestDummyGlobals.ReadFromResource($"{typeof(DepartmentGenerator).Namespace}.Departments.json")));
        }

        public static Department Random()
            => Departments.Items[s_random.Next(Departments.Items.Count)];
    }
}
