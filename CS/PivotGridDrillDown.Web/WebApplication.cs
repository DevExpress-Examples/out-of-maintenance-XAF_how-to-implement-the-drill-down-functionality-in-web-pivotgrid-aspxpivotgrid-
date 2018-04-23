using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;

namespace PivotGridDrillDown.Web {
    public partial class PivotGridDrillDownAspNetApplication : WebApplication {
        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule module2;
        private PivotGridDrillDown.Module.PivotGridDrillDownModule module3;
        private PivotGridDrillDown.Module.Web.PivotGridDrillDownAspNetModule module4;
        private DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule pivotGridAspNetModule1;
        private DevExpress.ExpressApp.PivotGrid.PivotGridModule pivotGridModule1;
        private System.Data.SqlClient.SqlConnection sqlConnection1;

        public PivotGridDrillDownAspNetApplication() {
            InitializeComponent();
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProvider = new XPObjectSpaceProviderThreadSafe(args.ConnectionString, args.Connection);
        }

        private void PivotGridDrillDownAspNetApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
            e.Updater.Update();
            e.Handled = true;
        }

        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule();
            this.module3 = new PivotGridDrillDown.Module.PivotGridDrillDownModule();
            this.module4 = new PivotGridDrillDown.Module.Web.PivotGridDrillDownAspNetModule();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.pivotGridAspNetModule1 = new DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule();
            this.pivotGridModule1 = new DevExpress.ExpressApp.PivotGrid.PivotGridModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Integrated Security=SSPI;Pooling=false;Data Source=.\\SQLEXPRESS;Initial Catalog=P" +
    "ivotGridDrillDown";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // PivotGridDrillDownAspNetApplication
            // 
            this.ApplicationName = "PivotGridDrillDown";
            this.Connection = this.sqlConnection1;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.module4);
            this.Modules.Add(this.pivotGridModule1);
            this.Modules.Add(this.pivotGridAspNetModule1);
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.PivotGridDrillDownAspNetApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
