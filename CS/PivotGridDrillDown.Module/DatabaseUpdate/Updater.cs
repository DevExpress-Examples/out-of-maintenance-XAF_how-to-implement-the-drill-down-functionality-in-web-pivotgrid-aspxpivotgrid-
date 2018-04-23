using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using PivotGridDrillDown.Module.BusinessObjects;

namespace PivotGridDrillDown.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            CreateDomainObject1("Obj 1", 1, 1, 2);
            CreateDomainObject1("Obj 2", 1, 0, 1);
            CreateDomainObject1("Obj 1", 2, 1, 0);
            CreateDomainObject1("Obj 2", 2, 2, 1);
            CreateDomainObject1("Obj 1", 2, 1, 1);
        }
        private void CreateDomainObject1(String name, int value1, int value2, int value3) {
            DomainObject1 obj = ObjectSpace.CreateObject<DomainObject1>();
            obj.Name = name;
            obj.Value1 = value1;
            obj.Value2 = value2;
            obj.Value3 = value3;
        }
    }
}
