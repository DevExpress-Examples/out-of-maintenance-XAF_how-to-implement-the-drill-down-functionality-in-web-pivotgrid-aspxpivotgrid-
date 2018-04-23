using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace PivotGridDrillDown.Module.BusinessObjects {
    [DefaultClassOptions]
    public class DomainObject1 : BaseObject {
        public DomainObject1(Session session)
            : base(session) { }
        private string _Name;
        public string Name {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }
        private int _Value1;
        public int Value1 {
            get { return _Value1; }
            set { SetPropertyValue("Value1", ref _Value1, value); }
        }
        private int _Value2;
        public int Value2 {
            get { return _Value2; }
            set { SetPropertyValue("Value2", ref _Value2, value); }
        }
        private int _Value3;
        public int Value3 {
            get { return _Value3; }
            set { SetPropertyValue("Value3", ref _Value3, value); }
        }
    }

}
