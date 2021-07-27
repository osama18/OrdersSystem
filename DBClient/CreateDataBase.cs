﻿
using OrderManagementSystem.Common.Settings;


namespace DBTester
{
    public class CreateDataBase : ICommad
    {
        private readonly ISettingProvider settings;
        public CreateDataBase(ISettingProvider settings)
        {
            this.settings = settings;
        }
        public string Name { get => "Create Databases"; }
        public string Key { get => "2"; }

        public void Execute()
        {
            
        }
    }
}
